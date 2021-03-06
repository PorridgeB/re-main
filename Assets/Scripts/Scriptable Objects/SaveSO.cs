using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class SaveSO : ScriptableObject
{
    [SerializeField]
    private Save save;

    public void ImportSave(Save s)
    {
        Debug.Log("Importing Save");
        save = s;
    }

    public RunInfo CurrentRun => save.Runs[save.Runs.Count - 1];
    public int Count => save.Runs.Count;
    public int Scrap => save.Scrap;
    public int DataFragments => save.DataFragments;
    public int Difficulty => CurrentRun.difficulty;
    public int Rings => save.UnlockedSoftwareRings;

    public float GenerationCoefficient => 1 - (1 / (float)CurrentRun.sector);

    public void AddTime(float time)
    {
        save.TotalTime += time;
    }

    public void UnlockRing()
    {
        save.UnlockedSoftwareRings++;
    }
    
    public bool CanBuyWithScrap(int cost)
    {
        return save.Scrap >= cost;
    }

    public void MakePurchaseWithData(int cost)
    {
        save.DataFragments -= cost;
    }

    public void MakePurchaseWithScrap(int cost)
    {
        save.Scrap -= cost;
    }

    public void AddSoftware(string name)
    {
        save.UnlockedSoftwareUpgrades.Add(name);
    }

    public void AddGadget(string name)
    {
        save.UnlockedGadgets.Add(name);
    }

    public bool SoftwareIsUnlocked(string name)
    {
        return save.UnlockedSoftwareUpgrades.Contains(name);
    }

    public int GetSoftwareCount(string name)
    {
        int i = 0;
        foreach (SoftwareUpgradeInstance s in SelectedLoadout.SoftwareUpgrades)
        {
            if (s.SoftwareUpgrade.name == name)
            {
                i++;
            }
        }
        return i;
    }

    public bool GadgetIsUnlocked(string name)
    {
        return save.UnlockedGadgets.Contains(name);
    }

    public Loadout SelectedLoadout => save.Loadouts[save.LoadoutIndex];

    

    

    public void Clear()
    {
        save.Runs.Clear();
    }

    public void NewRun()
    {
        RunInfo newRun = new RunInfo();
        newRun.difficulty = save.Difficulty;
        newRun.quadrant = 1;
        newRun.sector = 1;
        save.Runs.Add(newRun);
    }

    public void CompleteRun()
    {
        CurrentRun.ended = true;
        save.Scrap += CurrentRun.scrap;
        save.DataFragments += CurrentRun.dataFragments;
    }
}
