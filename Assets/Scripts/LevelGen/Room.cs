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
    private List<Passage> passages;
    private int currentPassageIndex;

    [SerializeField]
    private List<Transform> enemySpawns;
    public List<Transform> EnemySpawns
    {
        get
        {
            return enemySpawns;
        }
    }

    public Passage CurrentPassage(){
        return passages[currentPassageIndex];
    }

    public void ReservePassage(ConnectionSide side){
        Passage removeTarget = null;
        foreach (Passage p in passages){
            if (p.side == side){
                removeTarget = p;
                Debug.Log("Reserving Passage: " + p.side);
            }
        }
        passages.Remove(removeTarget);
    }

    public Passage NewPassage(){
        currentPassageIndex = Random.Range(0, passages.Count);
        return CurrentPassage();
    }

    public Vector3 Offset(Vector3 dir)
    {
        return new Vector3(dir.x * cellSize.x, dir.y, dir.z*cellSize.y)/2;
    }

    public void CenterRoom(Room previousRoom)
    {
        transform.position += (previousRoom.Offset(new Vector3(1,0,1)) - Offset(new Vector3(1, 0, 1))) ;
    }

    public Vector3 GetCenter(){
        return transform.position + Offset(new Vector3(1,0,1));
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

    public virtual void Generate()
    {
        
    }
}
