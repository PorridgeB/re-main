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
    private List<Vector3> directions = new List<Vector3>();
    [SerializeField]
    private string generationTemplate;
    private Vector3 currentDir;
    private Stack<Room> roomPath = new Stack<Room>();
    private LevelGrammarGenerator grammarGenerator;


    // Start is called before the first frame update
    void Start()
    {
        grammarGenerator = GetComponent<LevelGrammarGenerator>();
        generationTemplate = grammarGenerator.GetGenerationTemplate();
        int i = 0;
        while (start == null)
        {
            i++;
            if (i > 10)
            {
                Debug.LogError("that didn't work");
                break;
            }
            CreateLevel();
        }
        Debug.Log("Success");
    }

    private void CreateLevel()
    {
        roomPath.Push(Instantiate(room).GetComponent<Room>());
        start = roomPath.Peek();
        roomPath.Peek().SetText("Start");
        Turn();
        foreach (char c in generationTemplate.ToCharArray())
        {
            if (!Step(c))
            {
                Debug.Log("Generation Failed");
                Destroy(start.gameObject);
                start = null;
                return;
            }

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


            
            Room currentRoom = Instantiate(room, previousRoom.transform).GetComponent<Room>();
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
        RaycastHit hit;
        foreach (Vector3 v in directions)
        {
            Physics.Raycast(roomPath.Peek().transform.position + roomPath.Peek().Offset(v) + Vector3.up / 2 + (Vector3.right * 8), Vector3.down, out hit);
            Debug.Log(roomPath.Peek().transform.position + roomPath.Peek().Offset(v) + Vector3.up / 2 + (Vector3.right * 8));
            if (hit.collider == null)
            {
                Debug.Log("found a way to go");
                currentDir = v;
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

