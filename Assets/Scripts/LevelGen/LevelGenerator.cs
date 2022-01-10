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

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private Room start;

    [SerializeField]
    private List<GameObject> singleConnection;
    [SerializeField]
    private List<GameObject> doubleConnection;
    [SerializeField]
    private List<GameObject> tripleConnection;
    [SerializeField]
    private List<GameObject> hub;

    [SerializeField]
    private List<Room> allRooms;

    [SerializeField]
    private List<GameObject> rooms;
    [SerializeField]
    private List<GameObject> chestRooms;
    [SerializeField]
    private List<GameObject> obstacleRooms;
    [SerializeField]
    private List<GameObject> combatRooms;
    [SerializeField]
    private List<GameObject> trapRooms;
    [SerializeField]
    private GameObject hall;
    [SerializeField]
    private List<Vector3> directions = new List<Vector3>();
    [SerializeField]
    private List<Vector3> corners = new List<Vector3>();
    [SerializeField]
    private string generationTemplate;
    private Vector3 currentDir;
    private Stack<Room> roomPath = new Stack<Room>();
    private LevelGrammarGenerator grammarGenerator;
    private int index = 0;
    private int roomsSinceTurn = 0;
    private int maxRoomsSinceTurn;
    [SerializeField]
    private int generationAttempts = 0;
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
            foreach (Room r in start.GetComponentsInChildren<Room>())
            {
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
        Debug.Log(index);
        if (index == 0)
        {
            Room room = Instantiate(singleConnection[Random.Range(0,singleConnection.Count)], transform).GetComponent<Room>();
            roomPath.Push(room);
            allRooms.Add(room);
            start = room;
            start.SetText("Start");
            Debug.Log("creating start");
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
            start = null;
            index = 0;
        }
        else{
            index++;
        }
        
    }

    private GameObject GetRoom(List<ConnectionSide> sides) {
        List<GameObject> pool = null;
        switch(sides.Count){
            case 1:
                pool = singleConnection;
                break;
            case 2:
                pool = doubleConnection;
                break;
            case 3:
                pool = tripleConnection;
                break;
            case 4:
                pool = hub;
                break;
        }
        foreach (GameObject go in pool){
            if (go.GetComponent<Room>().HasSides(sides)){
                return go;
            }
        }
        return null;
    }

    private List<GameObject> GetRooms(List<ConnectionSide> sides, int count) {
        List<GameObject> rooms = new List<GameObject>();
        List<GameObject> pool = null;
        switch(count){
            case 1:
                pool = singleConnection;
                break;
            case 2:
                pool = doubleConnection;
                break;
            case 3:
                pool = tripleConnection;
                break;
            case 4:
                pool = hub;
                break;
        }
        foreach (GameObject go in pool){
            if (go.GetComponent<Room>().HasSides(sides)){

                rooms.Add(go);
            }
        }
        return rooms;
    }

    private int GetConnectionCount() {
        
        int branchDepth = 0;
        int count = 1;
        for (int i = index+1; i < generationTemplate.Length; i++) {
            if (branchDepth == 0) {
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
            Debug.Log("Spawning " + c);
            Room previousRoom = roomPath.Pop();
            if (previousRoom.Passages.Count <= 0) return false;
            Passage passage = previousRoom.GetUnconnectedPassage();
            if (passage == null) return false;
            currentDir = GetVector(passage.side);

            int connectionCount = GetConnectionCount();
            List<ConnectionSide> sides = new List<ConnectionSide>();
            for (int i = 0; i < connectionCount; i++)
            {
                sides.Add((ConnectionSide)Random.Range(0,4));
            }
            Debug.Log("next room must have " +  GetConnectionSide(-currentDir) + " and " + connectionCount + " connections");
            if (!sides.Contains(GetConnectionSide(-currentDir)))
            {
                
                sides[0] = GetConnectionSide(-currentDir);
            }
            Room currentRoom = null;
            List<GameObject> potentialRooms = GetRooms(new List<ConnectionSide>(){GetConnectionSide(-currentDir)}, connectionCount);
            Debug.Log("Finding valid room in direction: " + currentDir);
            currentRoom = FindValidRoom(potentialRooms, previousRoom);
            if (currentRoom == null) return false;
            
            previousRoom.ReservePassage(GetConnectionSide(currentDir)); 
            currentRoom.ReservePassage(GetConnectionSide(-currentDir));

            currentRoom.transform.position += currentRoom.Offset(currentDir) + previousRoom.Offset(currentDir);
            currentRoom.transform.position -= currentRoom.PassageOffset(GetConnectionSide(-currentDir)) - previousRoom.PassageOffset(GetConnectionSide(currentDir));
            currentRoom.SetText(c.ToString());

            

            roomPath.Push(currentRoom);
            allRooms.Add(currentRoom);
        }
        return true;
    }

    private Room FindValidRoom(List<GameObject> potentialRooms, Room previousRoom) {
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
        return Instantiate(validRooms[Random.Range(0, validRooms.Count)], previousRoom.transform).GetComponent<Room>();
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
                Debug.LogError("overlapping with " + r.name);
                return true;

            }
            else if (Mathf.Abs(Mathf.Round(newPosition.x - r.GetCenter().x)) == 0 && 
                Mathf.Abs(Mathf.Round(newPosition.z - r.GetCenter().z)) == 0)
            {
                Debug.LogError("overlapping with " + r.name);
                return true;
            }
        }
        return false;
    }
}

