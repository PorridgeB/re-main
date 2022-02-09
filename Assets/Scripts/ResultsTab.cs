using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultsTab : MonoBehaviour
{
    [SerializeField]
    private GameObject result;
    [SerializeField]
    private GameObject container;
    [SerializeField]
    private SaveSO currentSave;

    // Start is called before the first frame update
    void Start()
    {
        //Time
        CreateResult("Time", currentSave.CurrentRun.time.ToString());
        CreateResult("Stage", currentSave.CurrentRun.quadrant.ToString() + " - " + currentSave.CurrentRun.sector.ToString());
        CreateResult("Scrap", currentSave.CurrentRun.scrap.ToString());
        CreateResult("Data Fragments", currentSave.CurrentRun.dataFragments.ToString());
        CreateResult("Kills", currentSave.CurrentRun.kills.ToString());
        CreateResult("Damage", currentSave.CurrentRun.damage.ToString());
        CreateResult("Largest Hit", currentSave.CurrentRun.largestHit.ToString());
    }

    private void CreateResult(string name, string value)
    {
        Instantiate(result, container.transform).GetComponent<Result>().SetValues(name, value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
