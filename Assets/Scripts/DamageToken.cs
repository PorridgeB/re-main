using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageToken : MonoBehaviour
{
    [SerializeField]
    private List<Color> colors;

    private DamageInstance damage;
    private Vector3 dir;
    [SerializeField]
    private TMP_Text text;

    public DamageInstance Damage
    {
        get
        {
            return damage;
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

    public void SetValue(DamageInstance instance)
    {
        damage = instance;
        text.text = damage.value.ToString();
        if (damage.crit)
        {
            text.color = colors[1];
            text.fontSize += 10;
        }
    }

    public void Finish()
    {
        Destroy(gameObject);
    }
}
