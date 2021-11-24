using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageToken : MonoBehaviour
{
    private int value;
    private Vector3 dir;
    [SerializeField]
    private TMP_Text text;

    public int Value
    {
        get
        {
            return value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        float xVel = (float)(Random.Range(0, 100) - 50) / 10000;
        dir = new Vector3(xVel, 0.014f, 0);

    }

    // Update is called once per frame
    void Update()
    {
        text.transform.position += dir;
        dir += new Vector3(0, -0.0001f, 0);
    }

    public void SetValue(int damage)
    {
        value = damage;
        text.text = value.ToString();
    }

    public void Finish()
    {
        Destroy(gameObject);
    }
}
