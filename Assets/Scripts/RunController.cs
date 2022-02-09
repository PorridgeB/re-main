using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunController : MonoBehaviour
{
    public SaveSO currentSave;
    public List<Module> modules;
    public List<Attribute> attributes;
    public Resource playerHP;
    public Resource playerEnergy;

    private void Awake()
    {
        if (currentSave.Count < 1)
        {
            StartNewRun();
        }
        else if (currentSave.CurrentRun.ended)
        {
            StartNewRun();
        }
    }

    public void StartNewRun()
    {
        currentSave.NewRun();
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
        currentSave.CompleteRun();
    }

    public void StageComplete()
    {
        currentSave.CurrentRun.sector++;
    }

    public void EnemyKilled()
    {
        currentSave.CurrentRun.kills++;
    }

    public void OnApplicationQuit()
    {
        currentSave.Clear();
    }
}
