using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum ResourceType {
    Data,
    Scrap
}

public class Room : MonoBehaviour
{
    [SerializeField]
    private List<Passage> passages;
    [SerializeField]
    private GameObject resourceSpawns;
    [SerializeField]
    private GameObject pickupSpawns;
    [SerializeField]
    private GameObject crateWalls;
    private ResourceType type;
    [SerializeField]
    private int resourceBudget;
    private int pickupBudget;
    private Vector2 size;
    [SerializeField]
    private GameObject dataFragment;
    [SerializeField]    
    private GameObject scrap;
    [SerializeField]
    private GameObject pickupPlaceholder;

    void Awake()
    {
        RectInt rect = GetComponent<RoomMesh>().GetRect();
        size = new Vector2(rect.xMax-rect.xMin+1, rect.yMax-rect.yMin+1);
        type = (ResourceType)Random.Range(0,2);
        resourceBudget = Random.Range(2, 150);

        
    }

    public void ReservePassage(ConnectionSide side, Room room){
        foreach (Passage p in passages){
            if (p.side == side){
                p.connection = room;
                //Debug.Log("Reserving Passage: " + p.side);
            }
        }
    }

    public void DisconnectPassages(Room room){
        foreach (Passage p in passages) {
            if (p.connection == room || p.connection == null){
                Debug.Log("disconnecting passage from " + name);
                p.connection = null;
            }
        }
    }
    public Passage GetUnconnectedPassage() {
        foreach (Passage p in passages){
            if (!p.Connected){
                return p;
            }
        }
        return null;
    }

    public Vector3 Offset(Vector3 dir)
    {
        return new Vector3(dir.x * size.x, dir.y, dir.z*size.y)/2;
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

    private void SpawnResources() {
        if (resourceSpawns == null){
            Debug.Log(name);
        }
        foreach (Transform t in transform.Find("ResourceSpawns")){
            int value = Mathf.Min(30, resourceBudget);
            resourceBudget -= value;
            GameObject go = null;
            switch(type){
                case ResourceType.Data:
                    go = dataFragment;
                    break;
                case ResourceType.Scrap:
                    go = scrap;
                    break;
            }
            Instantiate(go, t.position+Vector3.up, new Quaternion(), transform);
            if (resourceBudget == 0) return;
        }
    }

    private void EnableCrates() {
        foreach (Transform t in transform.Find("CrateWalls")){
            if (Random.value < 1){
                Debug.Log("enabling crate");
                t.gameObject.SetActive(true);
            }
        }
        
    }

    private void SpawnPickups() {
        foreach (Transform t in transform.Find("PickupSpawns")){
            //TODO: make this 0.5f/difficulty
            if (Random.value < 0.5f){
                Instantiate(pickupPlaceholder, t.transform.position+Vector3.up, new Quaternion(), transform);
            }
        }
    }

    public virtual void Generate()
    {
        SpawnResources();
        SpawnPickups();
        EnableCrates();
    }
}
