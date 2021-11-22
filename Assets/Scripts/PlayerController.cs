using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public delegate void Player();
    public static event Player Died;
    public static event Player PowerDepleted;

    private Animator anim;

    private float scrap = 0;
    private bool dashBlocked = true;
    private float viewDistance;
    private Vector2 facing;
    private Resource health;
    private ModuleInventory inventory;
    private Crosshair crosshair;
    //private Sword sword;
    //private Trail trail;

    public Vector2 GetFacing()
    {
        return facing;
    }



    // Start is called before the first frame update
    void Start()
    {
        health = GetComponentInChildren<Resource>();
        crosshair = GetComponentInChildren<Crosshair>();
        anim = GetComponent<Animator>();

        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        facing = (crosshair.transform.position - transform.position).normalized;
        anim.SetFloat("Horizontal", facing.x);
        anim.SetFloat("Vertical", facing.y);
    }
}
