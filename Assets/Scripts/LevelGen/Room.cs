using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Room : MonoBehaviour
{
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


    public Vector3 Offset(Vector3 dir)
    {
        return new Vector3(dir.x * cellSize.x, dir.y, dir.z*cellSize.y)/2;
    }

    public Vector3 GetBound(Vector3 dir)
    {
        Vector3 offset = Offset(dir);
        return offset;
    }

    public Vector3 HalfExtent
    {
        get
        {
            return Offset(new Vector3(1, 0, 1));
        }
    }

    public void OpenPassage(Vector3 dir)
    {
        walls[GetRoomSide(dir)].SetActive(false);
    }

    public void CenterRoom(Room previousRoom)
    {
        transform.position += (previousRoom.Offset(new Vector3(1,0,1)) - Offset(new Vector3(1, 0, 1))) ;
    }

    public Vector3 GetCenter()
    {
        return transform.position + Offset(new Vector3(1, 0, 1));
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
        //textfield.text = text;
        name = text;
    }

    public virtual void Generate()
    {
    }
}
