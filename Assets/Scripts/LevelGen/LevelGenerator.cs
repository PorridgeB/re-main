using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private Room start;
    [SerializeField]
    private GameObject room;
    [SerializeField]
    private GameObject hall;
    [SerializeField]
    private List<Vector3> directions = new List<Vector3>();
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
            roomPath.Push(Instantiate(room).GetComponent<Room>());
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
            return;
        }
    }

    public void FixedUpdate()
    {
        if (index < generationTemplate.Length)
        {
            CreateLevel(index);
        }
        else
        {
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
            if (!FindEmptyCell()) return false;
            Room previousRoom = roomPath.Pop();
            Room currentRoom;
            if (c == 'h')
            {
                currentRoom = Instantiate(hall, previousRoom.transform).GetComponent<Room>();
            }
            else
            {
                currentRoom = Instantiate(room, previousRoom.transform).GetComponent<Room>();
            }
            
            
            currentRoom.transform.position += currentRoom.Offset(currentDir);
            currentRoom.SetText(c.ToString());
            currentRoom.OpenPassage(-currentDir);
            previousRoom.OpenPassage(currentDir);

            roomPath.Push(currentRoom);
        }
        return true;
    }

    private bool FindEmptyCell()
    {
        List<Vector3> possibleDirections = new List<Vector3>();
        RaycastHit hit;
        foreach (Vector3 v in directions)
        {
            Physics.Raycast(roomPath.Peek().transform.position + roomPath.Peek().Offset(v) + Vector3.up / 2 + (Vector3.right * 8), Vector3.down, out hit);
            if (hit.collider == null)
            {
                possibleDirections.Add(v);
            }
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

    private void Turn()
    {
        currentDir = directions[Random.Range(0, directions.Count - 1)];
    }
}

