using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Room : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textfield;
    [SerializeField]
    private Vector2 cellSize;
    [SerializeField]
    private List<GameObject> walls;
    [SerializeField]
    private List<Transform> enemySpawns;
    public List<Transform> EnemySpawns
    {
        get
        {
            return enemySpawns;
        }
    }

    [SerializeField]
    private GameObject chest;
    [SerializeField]
    private GameObject boss;
    [SerializeField]
    private GameObject end;
    [SerializeField]
    private GameObject npc;
    [SerializeField]
    private GameObject dummy;
    [SerializeField]
    private GameObject artifact;
    [SerializeField]
    private GameObject ooze;
    [SerializeField]
    private GameObject drone;
    [SerializeField]
    private GameObject gnat;


    public Vector3 Offset(Vector3 dir)
    {
        return new Vector3(dir.x * cellSize.x, dir.y, dir.z*cellSize.y)/2;
    }

    public void OpenPassage(Vector3 dir)
    {
        walls[GetRoomSide(dir)].SetActive(false);
    }

    private int GetRoomSide(Vector3 dir)
    {
        if (dir.z == 1)
        {
            return 0;
        }
        else if (dir.x == 1)
        {
            return 1;
        }
        else if (dir.z == -1)
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }

    public void SetText(string text)
    {
        textfield.text = text;
        name = text;
    }

    public void Generate()
    {
        GameObject go;
        switch (name)
        {
            
            case "Start":
                break;
            case "b":
                for (int i = 0; i < Random.Range(3,6); i++)
                {
                    Instantiate(drone, transform.position + Vector3.right * 8 + Vector3.up, new Quaternion(), null);
                }
                break;
            case "r":
                go = Instantiate(chest, transform);
                go.transform.localPosition = Vector3.right * 8 + Vector3.up;
                break;
            case "f":
                go = Instantiate(end, transform);
                go.transform.localPosition = Vector3.right * 8 + Vector3.up;
                break;
            case "n":
                go = Instantiate(npc, transform);
                go.transform.localPosition = Vector3.right * 8 + Vector3.up;
                break;
            case "e":
                if (Random.value < 0.25)
                {
                    go = gnat;
                }
                else
                {
                    go = ooze;
                }
                for (int i = 0; i < Random.Range(2,10); i++)
                {
                    Instantiate(go, transform.position + Vector3.right * 8 + Vector3.up, new Quaternion(), null);                   
                }
                break;
            case "a":
                go = Instantiate(artifact, transform);
                go.transform.localPosition = Vector3.right * 8 + Vector3.up;
                break;
        }
    }
}
