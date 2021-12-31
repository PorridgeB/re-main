using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField]
    private GameObject drone;
    private Room room;
    private bool triggered;

    private void Start()
    {
        room = transform.parent.GetComponent<Room>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Vector3.Distance(transform.position, other.gameObject.transform.position) < 5 && !triggered)
            {
                triggered = true;
                Activate();
            }
        }
    }

    private void Activate()
    {
        foreach (Transform t in room.EnemySpawns)
        {
            Instantiate(drone, t.position, new Quaternion(), null);
        }
    }
}
