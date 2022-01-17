using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doorway : MonoBehaviour
{
    public bool playerInRoom;
    private GameObject door;

    public void Start()
    {
        door = transform.Find("Door").gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRoom = true;
        }
    }

    public void Close()
    {
        door.SetActive(true);
    }

    public void Open()
    {
        door.SetActive(false);
    }
}
