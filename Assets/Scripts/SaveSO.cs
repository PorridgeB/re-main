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

    public int Count
    {
        get
        {
            return save.Runs.Count;
        }
    }

    public void AddTime(float time)
    {
        save.TotalTime += time;
    }

    public int Difficulty
    {
        get
        {
            return CurrentRun.difficulty;
        }
    }

    public RunInfo CurrentRun
    {
        get
        {
            return save.Runs[save.Runs.Count - 1];
        }
    }

    public float GenerationCoefficient
    {
        get
        {
            return 1 - (1 / (float)CurrentRun.sector);
        }
    }

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
