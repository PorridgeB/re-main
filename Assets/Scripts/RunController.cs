using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunController : MonoBehaviour
{
    public RunInfoHistory runHistory;
    public List<Module> modules;
    public List<Attribute> attributes;
    public Resource playerHP;
    public Resource playerEnergy;

    private void Start()
    {
        if (runHistory.Count < 1)
        {
            StartNewRun();
        }
        else if (runHistory.Current.ended)
        {
            StartNewRun();
        }
    }

    public void StartNewRun()
    {
        runHistory.NewRun();
        foreach (Module m in modules)
        {
            m.count = 0;
        }
        foreach (Attribute a in attributes)
        {
            a.Reset();
        }
        playerHP.Reset();
        playerEnergy.Reset();
    }

    public void RunEnded()
    {
        runHistory.Current.ended = true;
    }

    public void StageComplete()
    {
        runHistory.Current.sector++;
    }

    public void EnemyKilled()
    {
        runHistory.Current.kills++;
    }

    public void OnApplicationQuit()
    {
        //THIS IS ONLY BEING USED FOR TESTING
        //this will delete all runs in the history to ensure it resets everything in editor
        runHistory.Clear();
    }
}
