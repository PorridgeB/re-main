using System.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MechanicStore : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scrap;

    public void Close()
    {
        Destroy(gameObject);
    }
}
