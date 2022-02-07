using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modules : MonoBehaviour
{
    [SerializeField]
    private GameObject emptyModuleSlotPrefab;
    [SerializeField]
    private GameObject moduleSlotPrefab;
    [SerializeField]
    private GameObject moduleGrid;

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(moduleSlotPrefab, moduleGrid.transform);
        }

        for (int i = 0; i < 40; i++)
        {
            Instantiate(emptyModuleSlotPrefab, moduleGrid.transform);
        }
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}
