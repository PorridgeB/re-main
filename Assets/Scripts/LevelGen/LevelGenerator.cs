using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> rooms;
    [SerializeField]
    private GameObject room;
    [SerializeField]
    private GameObject connector;
    [SerializeField]
    private List<Vector3> directons = new List<Vector3>();
    [SerializeField]
    private string generationTemplate;
    private Vector3 currentDir;
    [SerializeField]
    private Stack<Room> currentRoom = new Stack<Room>();
    private Room branchBase;
    private Vector3 branchDir;
    private LevelGrammarGenerator grammarGenerator;
    

    // Start is called before the first frame update
    void Start()
    {
        grammarGenerator = GetComponent<LevelGrammarGenerator>();
        generationTemplate = grammarGenerator.GetGenerationTemplate();
        currentRoom.Push(Instantiate(room).GetComponent<Room>());
        currentRoom.Peek().SetText("Start");
        Turn();
        int i = 1;
        foreach (char c in generationTemplate.ToCharArray())
        {
            StartCoroutine(Step(c, i * 0.4f));
            i++;
        }
    }

    public IEnumerator Step(char c, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (c == '(')
        {
            currentRoom.Push(currentRoom.Peek());
            branchDir = currentDir;
            FindTurn(currentRoom.Peek(), ParentRoomDir(currentRoom.Peek(), currentRoom.Peek().transform.parent));
        }
        else if (c == ')')
        {
            currentRoom.Pop();
            FindTurn(currentRoom.Peek(), ParentRoomDir(currentRoom.Peek(), currentRoom.Peek().transform.parent));
        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(currentRoom.Peek().transform.position + currentRoom.Peek().Offset(currentDir) + Vector3.up + (Vector3.right * 8), Vector3.down, out hit))
            {
                Debug.Log("blocked. Changing direction");
                FindTurn(currentRoom.Peek(), ParentRoomDir(currentRoom.Peek(), currentRoom.Peek().transform.parent));
            }
            Room previousRoom = currentRoom.Pop();
            previousRoom.OpenPassage(currentDir);

            currentRoom.Push(Instantiate(room, previousRoom.transform).GetComponent<Room>());
            currentRoom.Peek().transform.position += currentRoom.Peek().Offset(currentDir);
            currentRoom.Peek().SetText(c.ToString());
            currentRoom.Peek().OpenPassage(-currentDir);
        }
        
    }

    
    private Vector3 ParentRoomDir(Room room, Transform parentRoom)
    {
        if (parentRoom == null) return Vector3.zero;
        return (parentRoom.position - room.transform.position).normalized;
    }

    public void FindTurn(Room startingRoom, Vector3 parentRoomDir)
    {
        List<Vector3> availableDirections = new List<Vector3>();
        foreach (Vector3 dir in directons)
        {
            availableDirections.Add(dir);
        }
        availableDirections.Remove(parentRoomDir);
        for (int i = 0; i < availableDirections.Count; i++){
            if (Physics.Raycast(startingRoom.transform.position + startingRoom.Offset(availableDirections[i])+Vector3.up + (Vector3.right * 8), Vector3.down))
            {
                availableDirections.Remove(availableDirections[i]);
            }
        }
        int r = Random.Range(0, availableDirections.Count - 1);
        currentDir = availableDirections[r];
    }

    private void Turn()
    {
        if (Mathf.Abs(currentDir.x) == 1)
        {
            currentDir.x = 0;
            currentDir.y = Random.value > 0.5 ? -1 : 1;
        }
        else
        {
            currentDir.y = 0;
            currentDir.x = Random.value > 0.5 ? -1 : 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
