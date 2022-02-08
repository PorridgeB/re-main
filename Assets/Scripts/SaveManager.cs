using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
[System.Serializable]
public class SaveList
{
    public Save[] saves;
}

public class SaveManager : MonoBehaviour
{
    [SerializeField]
    private SaveList saves = new SaveList();
    [SerializeField]
    private SaveSO currentSave;
    [SerializeField]
    private TextAsset jsonFile;

    private void Awake()
    {
        //saves.saves = new Save[] { new Save(), new Save(), new Save(), new Save()};
        ImportSaves();

        DontDestroyOnLoad(gameObject);
    }

    public Save GetSave(int index)
    {
        Debug.Log(index);
        Debug.Log(saves.saves[index].TotalTime);
        if (saves.saves[index].TotalTime == 0) return null;

        return saves.saves[index];
    }

    public void SetCurrentSave(int index)
    {
        var selectedSave = saves.saves[index];

        currentSave.Created = selectedSave.Created;
        currentSave.DataFragments = selectedSave.DataFragments;
        currentSave.LoadoutIndex = selectedSave.LoadoutIndex;
        currentSave.Loadouts = selectedSave.Loadouts;
        currentSave.Runs = selectedSave.Runs;
        currentSave.Scrap = selectedSave.Scrap;
        currentSave.SoftwareUpgradeCapacity = selectedSave.SoftwareUpgradeCapacity;
        currentSave.TotalTime = selectedSave.TotalTime;
        currentSave.UnlockedGadgets = selectedSave.UnlockedGadgets;
        currentSave.UnlockedSoftwareUpgrades = selectedSave.UnlockedSoftwareUpgrades;
        currentSave.UnlockedWeaponAttachments = selectedSave.UnlockedWeaponAttachments;
    }

    public void OnApplicationQuit()
    {
        ExportSaves();
    }

    public void ImportSaves()
    {
        saves = JsonUtility.FromJson<SaveList>(jsonFile.text);
    }

    public void ExportSaves()
    {
        string strOutput = JsonUtility.ToJson(saves);
        File.WriteAllText(Application.dataPath + "/saves.txt", strOutput);
    }

    public void CreateNewSave(int i)
    {
        Save s = new Save();
        s.Created = System.DateTime.Now;
        Debug.Log(System.DateTime.Now);
        s.TotalTime = 10;
        s.DataFragments = 0;
        s.Scrap = 0;
        s.SoftwareUpgradeCapacity = 0;
        s.UnlockedSoftwareUpgrades = new List<string>() { "a", "b", "c" };
        s.UnlockedGadgets = new List<string>() { "a", "b", "c" };
        s.Loadouts = new List<Loadout>() { new Loadout() };
        s.LoadoutIndex = 0;
        s.Runs = new List<RunInfo>();
        saves.saves.SetValue(s, i);
    }
}
