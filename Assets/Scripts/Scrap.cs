using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrap : MonoBehaviour
{
    [SerializeField]
    private Sprite small;
    [SerializeField]
    private Sprite medium;
    [SerializeField]
    private Sprite large;

    [SerializeField]
    private int value;

    [SerializeField]
    private AnimationCurve speedCurve;
    private float timeInRange;

    private SpriteRenderer render;

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        SetValue(value);
    }

    public void SetValue(int v){
        value = v;
        
        if (value == 0) Destroy(gameObject);
        
        if (value < 5){
            render.sprite = small;
        }
        else if (value >= 5 && value < 20){
            render.sprite = medium;
        }
        else {
            render.sprite = large;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")){
            timeInRange += Time.deltaTime;
            Vector3 dir = (other.transform.position - transform.position);
            dir.y = 0;
            dir = dir.normalized;
            transform.position += dir*speedCurve.Evaluate(timeInRange);
            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(other.transform.position.x, other.transform.position.z)) < 0.5f){
                Destroy(gameObject);
            }
        }
    }  
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")){
            timeInRange = 0;
        }
    }
}
