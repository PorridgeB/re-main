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
            return runHistory[0];
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
        runHistory.Add(newRun);
    }
}
