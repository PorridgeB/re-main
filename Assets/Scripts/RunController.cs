using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunController : MonoBehaviour
{
    public SaveSO currentSave;
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
        foreach (Module m in Resources.LoadAll<Module>("Modules"))
        {
            m.count = 0;
        }
        foreach (Attribute a in Resources.LoadAll<Attribute>("Attributes"))
        {
            a.Reset();
        }
        foreach (SoftwareUpgradeInstance s in currentSave.SelectedLoadout.SoftwareUpgrades)
        {
            foreach (Bonus b in s.SoftwareUpgrade.bonuses)
            {
                b.attribute.AddSoftwareBonus(b);
            }
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
