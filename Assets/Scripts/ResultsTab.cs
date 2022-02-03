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
    private RunInfoHistory runInfoHistory;

    // Start is called before the first frame update
    void Start()
    {
        //Time
        CreateResult("Time", runInfoHistory.Current.time.ToString());
        CreateResult("Stage", runInfoHistory.Current.quadrant.ToString() + " - " + runInfoHistory.Current.sector.ToString());
        CreateResult("Scrap", runInfoHistory.Current.scrap.ToString());
        CreateResult("Data Fragments", runInfoHistory.Current.dataFragments.ToString());
        CreateResult("Kills", runInfoHistory.Current.kills.ToString());
        CreateResult("Damage", runInfoHistory.Current.damage.ToString());
        CreateResult("Largest Hit", runInfoHistory.Current.largestHit.ToString());
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
