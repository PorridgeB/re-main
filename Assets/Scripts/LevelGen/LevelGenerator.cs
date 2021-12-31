using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject cube;
    [SerializeField]
    private Room start;

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

    private void CreateLevel(int i)
    {
        if (i == 0)
        {
            roomPath.Push(Instantiate(rooms[0], transform).GetComponent<Room>());
            start = roomPath.Peek();
            roomPath.Peek().SetText("Start");
            Turn();
        }
        index++;
        if (!Step(generationTemplate.ToCharArray()[i]))
        {
            Debug.Log("Generation Failed");
            generationAttempts++;
            if (generationAttempts > 5)
            {
                generationAttempts = 0;
                generationTemplate = grammarGenerator.GetGenerationTemplate();
                generationTemplate = grammarGenerator.FillTemplate(generationTemplate);
            }
            Destroy(start.gameObject);
            start = null;
            index = 0;
        }
    }

    public void FixedUpdate()
    {
        if (index < generationTemplate.Length)
        {
            CreateLevel(index);
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
            
            Room previousRoom = roomPath.Pop();
            GameObject prefab;
            switch (c)
            {
                case 'h':
                    prefab = hall;
                    break;
                case 'r':
                    prefab = chestRooms[Random.Range(0, chestRooms.Count)];
                    break;
                case 'o':
                    prefab = obstacleRooms[Random.Range(0, obstacleRooms.Count)];
                    break;
                case 'e':
                    prefab = combatRooms[Random.Range(0, combatRooms.Count)];
                    break;
                case 't':
                    prefab = trapRooms[Random.Range(0, trapRooms.Count)];
                    break;
                default:
                    prefab = rooms[Random.Range(0, rooms.Count)];
                    break;

            }
            Room currentRoom = Instantiate(prefab, previousRoom.transform).GetComponent<Room>();

            if (!FindEmptyCell(previousRoom, currentRoom)) return false;

            currentRoom.transform.position += currentRoom.Offset(currentDir) + previousRoom.Offset(currentDir);
            currentRoom.SetText(c.ToString());
            currentRoom.OpenPassage(-currentDir);
            previousRoom.OpenPassage(currentDir);
            roomPath.Push(currentRoom);
        }
        return true;
    }

    private bool FindEmptyCell(Room previous, Room current)
    {
        List<Vector3> possibleDirections = new List<Vector3>();
        foreach (Vector3 v in directions)
        {
            if (CheckBoxCollision(previous, current, v))
            {
                continue;
            }
            possibleDirections.Add(v);

        }
        if (possibleDirections.Count > 0) {
            if (possibleDirections.Contains(currentDir) && roomsSinceTurn >= maxRoomsSinceTurn)
            {
                roomsSinceTurn++;
                return true;
            }
            roomsSinceTurn = 0;
            currentDir = possibleDirections[Random.Range(0, possibleDirections.Count - 1)];
            return true;
        }
        return false;

    }

    private bool CheckBoxCollision(Room previous, Room current, Vector3 v)
    {
        //GameObject go = Instantiate(cube, previous.transform.position + Vector3.up / 2 + (Vector3.right * 8) + current.Offset(v) + previous.Offset(v), new Quaternion(), null);
        //go.transform.localScale = current.Offset(new Vector3(2, 5, 2));
        foreach (Vector3 cardinalPoint in corners)
        {
            RaycastHit hit;
            if (Physics.Raycast(previous.transform.position + Vector3.up * 2 + (Vector3.right * 8) + current.Offset(v) + previous.Offset(v) + current.Offset(cardinalPoint), Vector3.down, out hit))
            {
                return true;
            }
        }
        return false;
    }

    private void Turn()
    {
        currentDir = directions[Random.Range(0, directions.Count - 1)];
    }
}

