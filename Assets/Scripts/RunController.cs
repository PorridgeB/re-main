using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunController : MonoBehaviour
{
    public RunInfoHistory runHistory;

    private void Start()
    {
        if (runHistory.Count < 1)
        {
            runHistory.NewRun();
        }
        else if (runHistory.Current.ended)
        {
            runHistory.NewRun();
        }
    }

    public void RunEnded()
    {
        runHistory.Current.ended = true;
    }
}
