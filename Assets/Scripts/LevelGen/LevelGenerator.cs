using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public enum ConnectionSide{
    Top,
    Right,
    Left,
    Bottom
}
[System.Serializable]
public class GenerationStep{

    public GenerationStep(int i, List<GameObject> rooms){
        index = i;
        stepAttempts = 0;
        roomOptions = rooms;
    }

    public GameObject NextRoom(){
        if (roomOptions.Count > 0){ 
            GameObject g = roomOptions[Random.Range(0, roomOptions.Count)];
            roomOptions.Remove(g);
            return g;
        }
        return null;
    }

    public int index;
    public Room room;
    public List<GameObject> roomOptions;
    public int stepAttempts;

}

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private Room start;
    [SerializeField]
    private List<GameObject> roomTestPool;
    [SerializeField]
    private List<Room> allRooms = new List<Room>();
    [SerializeField]
    private List<GameObject> startRooms;
    [SerializeField]
    private List<GameObject> finishRooms;
    [SerializeField]
    private List<GameObject> rewardRooms;
    [SerializeField]
    private List<GameObject> obstacleRooms;
    [SerializeField]
    private List<GameObject> combatRooms;
    [SerializeField]
    private List<GameObject> trapRooms;
    [SerializeField]
    private List<GameObject> npcRooms;
    [SerializeField]
    private List<GameObject> bossRooms;
    [SerializeField]
    private List<GameObject> artifactRooms;
    [SerializeField]
    private List<GameObject> hallways;
    [SerializeField]
    private string generationTemplate;
    private Vector3 currentDir;
    [SerializeField]
    private List<GenerationStep> stepHistory = new List<GenerationStep>();
    private Stack<GenerationStep> roomPath = new Stack<GenerationStep>();
    private LevelGrammarGenerator grammarGenerator;
    private int index = 0;
    private int roomHistoryIndex = 0;
    private int maxRoomsSinceTurn;
    [SerializeField]
    private int generationAttempts = 0;
    private int roomAttempts = 0;
    private bool roomsGenerated;
    [SerializeField]
    private GameEvent SceneReady;

    // Start is called before the first frame update
    void Start()
    {
        
        grammarGenerator = GetComponent<LevelGrammarGenerator>();
        generationTemplate = grammarGenerator.GetGenerationTemplate();
        generationTemplate = grammarGenerator.FillTemplate(generationTemplate);
        maxRoomsSinceTurn = 4;
    }


    public void FixedUpdate()
    {
        if (index < generationTemplate.Length)
        {
            CreateLevel();
        }
        else if (!roomsGenerated)
        {
            GetComponent<NavMeshSurface>().BuildNavMesh();
            foreach (Room r in allRooms)
            {
                Debug.Log("Generating Rooms");
                r.Generate();
            }
            roomsGenerated = true;
            SceneReady.Raise();
        }
        
    }

    
    private Vector3 GetVector(ConnectionSide side){
        switch(side){
            case ConnectionSide.Top:
                return new Vector3(0,0,1);
            case ConnectionSide.Right:
                return new Vector3(1,0,0);
            case ConnectionSide.Bottom:
                return new Vector3(0,0,-1);
            case ConnectionSide.Left:
                return new Vector3(-1,0,0);
            default:
                return new Vector3();
        }
    }

    private void CreateLevel()
    {
        //Debug.Log(index);
        if (index == 0)
        {
            Room room = Instantiate(startRooms[Random.Range(0,startRooms.Count)], transform).GetComponent<Room>();
            GenerationStep step = new GenerationStep(-1, new List<GameObject>());
            step.room = room;
            roomPath.Push(step);
            
            allRooms.Add(room);
            start = room;
            start.SetText("Start");
            //Debug.Log("creating start");
        }
        if (!Step(generationTemplate[index]))
        {
            Debug.LogError("Generation Failed");
            generationAttempts++;
            if (generationAttempts > 5)
            {
                generationAttempts = 0;
                generationTemplate = grammarGenerator.GetGenerationTemplate();
                generationTemplate = grammarGenerator.FillTemplate(generationTemplate);
            }
            Destroy(start.gameObject);
            roomPath.Clear();
            allRooms.Clear();
            stepHistory.Clear();
            start = null;
            index = 0;
        }
        else{
            index++;
        }
        
    }

    private List<GameObject> GetRooms(List<ConnectionSide> sides, int count, char c) {
        List<GameObject> rooms = new List<GameObject>();
        List<GameObject> pool = roomTestPool;
        switch(c){
            case 'e':
                pool = combatRooms;
                break;
            case 't':
                pool = trapRooms;
                break;
            case 'o':
                //pool = obstacleRooms;
                break;
            case 'h':
                //pool = hallways;
                break;
            case 'r':
                //pool = rewardRooms;
                break;
            case 'n':
                pool = npcRooms;
                break;
            case 'b':
                pool = bossRooms;
                break;
            case 'a':
                pool = artifactRooms;
                break;
            case 'f':
                //pool = finishRooms;
                break;
            case '-':
                //pool = hallways;
                break;
        }
        
        foreach (GameObject go in pool){
            Room r = go.GetComponent<Room>();
            if (r.HasSides(sides)){
                if (r.Passages.Count == count) {
                    rooms.Add(go);
                }
                
            }
        }
        return rooms;
    }

    private int GetConnectionCount() {
        
        int branchDepth = 0;
        int count = 1;
        for (int i = index+1; i < generationTemplate.Length; i++) {
            if (branchDepth == 0) {
                //Debug.Log(generationTemplate[i]);
                count++;
                if (generationTemplate[i] != ')' && generationTemplate[i] != '('){
                    
                    return count;
                }
            }
            if (generationTemplate[i] == ')'){
                branchDepth -= 1;
                continue;
            }
            else if(generationTemplate[i] == '(') {
                branchDepth += 1;
                continue;
            }
            else if (branchDepth < 0){
                return count-1;
            }
        }
        return count;
    }

    public bool Step(char c)
    {
        if (c == '(')
        {
            roomPath.Push(roomPath.Peek());
        }
        else if (c == ')')
        {
            roomPath.Pop();
        }
        else
        {
            if (roomPath.Count <= 0) return false;
            Room previousRoom = roomPath.Pop().room;
            if (previousRoom.Passages.Count <= 0){
                Debug.LogError("previous room has no passages");
                return false;
            }
            Passage passage = previousRoom.GetUnconnectedPassage();
            if (passage == null){
                Debug.LogError("no unconnected passages on " + previousRoom.name);
                foreach (GameObject g in roomTestPool){
                    if (previousRoom.gameObject == g){
                        Debug.LogError(g.name);
                    }
                }
                return false;
            }
            currentDir = GetVector(passage.side);
            if (stepHistory.Count <= index){
                CreateGenerationStep(previousRoom, c);
            }
            if (stepHistory[roomHistoryIndex].roomOptions == null){
                if (index == 0){
                    return false;
                }
                Rewind(previousRoom);
            }
            else if (stepHistory[roomHistoryIndex].roomOptions.Count <= 0){
                Debug.LogError("No alternatives");
                return false;
            }
            else {
                Room currentRoom = Instantiate(stepHistory[roomHistoryIndex].NextRoom(), previousRoom.transform).GetComponent<Room>();
                CreateRoom(currentRoom, previousRoom, c);
            }
            roomHistoryIndex++;
        }
        return true;
    }

    private void Rewind(Room previousRoom){
        Debug.LogWarning("Rewinding");
        GenerationStep step = GetStepByRoom(previousRoom.transform.parent.GetComponent<Room>());
        step.room.DisconnectPassages(step.room);
        index = step.index;
        step.stepAttempts++;
        roomHistoryIndex = stepHistory.IndexOf(step);
        if (roomPath.Count > 0) roomPath.Pop();
        roomPath.Push(step);
        allRooms.Remove(previousRoom);
        Destroy(previousRoom.gameObject);
    }

    private void CreateGenerationStep(Room previousRoom, char c) {
        

        int connectionCount = GetConnectionCount();
        List<ConnectionSide> sides = new List<ConnectionSide>();
        for (int i = 0; i < connectionCount; i++)
        {
            sides.Add((ConnectionSide)Random.Range(0,4));
        }
        //Debug.Log("next room must have " +  GetConnectionSide(-currentDir) + " and " + connectionCount + " connections");
        if (!sides.Contains(GetConnectionSide(-currentDir)))
        {
            
            sides[0] = GetConnectionSide(-currentDir);
        }

        List<GameObject> potentialRooms = GetRooms(new List<ConnectionSide>(){GetConnectionSide(-currentDir)}, connectionCount, c);
        //Debug.Log("Finding valid room in direction: " + currentDir);
        List<GameObject> validRooms = FindValidRooms(potentialRooms, previousRoom);

        stepHistory.Add(new GenerationStep(index, validRooms));
        roomHistoryIndex = stepHistory.Count-1;
        stepHistory[roomHistoryIndex].roomOptions = validRooms;
        
    }
    private void CreateRoom(Room currentRoom, Room previousRoom, char c) {
        previousRoom.ReservePassage(GetConnectionSide(currentDir), currentRoom); 
        currentRoom.ReservePassage(GetConnectionSide(-currentDir), previousRoom);

        currentRoom.CenterRoom(previousRoom);
        currentRoom.transform.position += currentRoom.Offset(currentDir) + previousRoom.Offset(currentDir);

        currentRoom.transform.position -= currentRoom.PassageOffset(GetConnectionSide(-currentDir)) - previousRoom.PassageOffset(GetConnectionSide(currentDir));
        currentRoom.SetText(c.ToString());
        GenerationStep s = GetStep();
        if (s != null){
            s.room = currentRoom;
        }
        else{
            Debug.Log("we fucked up lol");
        }
        roomPath.Push(s);
        allRooms.Add(currentRoom);
    }

    public GenerationStep GetStepByRoom(Room room) {
        foreach (GenerationStep s in stepHistory){
            if (s.room == room){
                return s;
            }
        }
        return null;
    }   

    public GenerationStep GetStep() {
        foreach (GenerationStep g in stepHistory){
            if (g.index == index){
                return g;
            }
        }
        return null;
    }

    private List<GameObject> FindValidRooms(List<GameObject> potentialRooms, Room previousRoom) {
        List<GameObject> validRooms = new List<GameObject>(); 
        Room currentRoom = null;
        foreach (GameObject room in potentialRooms){
                currentRoom = Instantiate(room, previousRoom.transform).GetComponent<Room>();
                currentRoom.CenterRoom(previousRoom);
                
                if (!CheckForOverlap(previousRoom, currentRoom, currentDir)) {
                    validRooms.Add(room);
                }
                DestroyImmediate(currentRoom.gameObject);
            }
        if (validRooms.Count <= 0) {
            Debug.LogError("No Valid Rooms");
            return null;
        }
        return validRooms;
    }

    public ConnectionSide GetConnectionSide(Vector3 side) {
        if (side.x < 0){
            return ConnectionSide.Left;
        }
        else if (side.x > 0){
            return ConnectionSide.Right;
        }
        else if (side.z > 0){
            return ConnectionSide.Top;
        }
        else{
            return ConnectionSide.Bottom;
        }
    }

    private bool CheckForOverlap(Room previousRoom, Room currentRoom, Vector3 direction)
    {
        Vector3 newPosition = previousRoom.GetCenter() + currentRoom.Offset(direction) + previousRoom.Offset(direction) - (currentRoom.PassageOffset(GetConnectionSide(-currentDir)) - previousRoom.PassageOffset(GetConnectionSide(currentDir)));
        foreach (Room r in allRooms)
        {
            Vector3 min = currentRoom.HalfExtent + r.HalfExtent;
            if (min.x > Mathf.Abs(Mathf.Round(newPosition.x - r.GetCenter().x)) &&
                min.z > Mathf.Abs(Mathf.Round(newPosition.z - r.GetCenter().z)))
            {
                //Debug.LogError("overlapping with " + r.name);
                return true;

            }
            else if (Mathf.Abs(Mathf.Round(newPosition.x - r.GetCenter().x)) == 0 && 
                Mathf.Abs(Mathf.Round(newPosition.z - r.GetCenter().z)) == 0)
            {
                //Debug.LogError("overlapping with " + r.name);
                return true;
            }
        }
        return false;
    }
}

