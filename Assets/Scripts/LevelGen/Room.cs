using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Room : MonoBehaviour
{
    [SerializeField]
    private Vector2 cellSize;
    [SerializeField]
    private List<Passage> passages;

    [SerializeField]
    private List<Transform> enemySpawns;
    public List<Transform> EnemySpawns
    {
        get
        {
            return enemySpawns;
        }
    }

    public void ReservePassage(ConnectionSide side){
        foreach (Passage p in passages){
            if (p.side == side){
                p.connected = true;
                Debug.Log("Reserving Passage: " + p.side);
            }
        }
    }

    public Passage GetUnconnectedPassage() {
        foreach (Passage p in passages){
            if (!p.connected){
                return p;
            }
        }
        return null;
    }

    public Vector3 Offset(Vector3 dir)
    {
        return new Vector3(dir.x * cellSize.x, dir.y, dir.z*cellSize.y)/2;
    }

    public Vector3 PassageOffset(ConnectionSide side){
        
        Vector3 t = GetPassage(side).Offset - HalfExtent;
        t.y = 0;
        switch(side){
            case ConnectionSide.Top:
            case ConnectionSide.Bottom:
                t.z = 0;
                break;
            case ConnectionSide.Left:
            case ConnectionSide.Right:
                t.x = 0;
                break;
        }
        return t;
    }

    public Passage GetPassage(ConnectionSide side) {
        foreach (Passage p in passages){
            if (p.side == side){
                Debug.Log(p);
                return p;
            }
        }
        Debug.LogError("Couldn't find passage");
        return null;
    }

    public void CenterRoom(Room previousRoom)
    {
        transform.position += (previousRoom.Offset(new Vector3(1,0,1)) - Offset(new Vector3(1, 0, 1))) ;
    }

    public Vector3 GetCenter(){
        return transform.position + Offset(new Vector3(1,0,1));
    }

    public Vector3 HalfExtent
    {
        get
        {
            return Offset(new Vector3(1, 0, 1));
        }
    }

    public List<Passage> Passages{
        get{
            return passages;
        }
    }

    public bool HasSides(List<ConnectionSide> sides) {
        foreach (ConnectionSide s in sides){
            if (!HasSide(s)){
                return false;
            }
        }
        
        return true;
    }

    private bool HasSide(ConnectionSide side){
        foreach (Passage p in passages){
            if (p.side == side) {
                return true;
            }
        }
        return false;
    }

    public void SetText(string text)
    {
        name = text;
    }

    public virtual void Generate()
    {
        
    }
}
