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

    public Vector3 Offset(Vector3 dir)
    {
        return new Vector3(dir.x * cellSize.x, dir.y, dir.z*cellSize.y);
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
}
