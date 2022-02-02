using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class RunInfoHistory : ScriptableObject
{
    public List<RunInfo> runHistory;
    public int Count
    {
        get
        {
            return runHistory.Count;
        }
    }

    public RunInfo Current
    {
        get
        {
            return runHistory[runHistory.Count-1];
        }
    }

    public float GenerationCoefficient
    {
        get
        {
            return 1 - (1 /(float)Current.sector);
        }
    }

    public void Clear()
    {
        runHistory.Clear();
    }

    public void NewRun()
    {
        RunInfo newRun = new RunInfo();
        newRun.difficulty = 1;
        newRun.quadrant = 1;
        newRun.sector = 1;
        runHistory.Add(newRun);
    }
}
