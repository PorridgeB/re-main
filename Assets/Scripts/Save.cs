using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class Save
{
    // Date and time the save file was created
    public DateTime Created;
    // Number of seconds spent playing the game (does not include paused time)
    public int TotalTime;
    // Quantity of data fragments in the player's inventory
    public int DataFragments;
    // Quantity of scrap in the player's inventory
    public int Scrap;
    // Number of rings allocated for software upgrades (D.O.R.A.I.)
    public int SoftwareUpgradeRings;
    // List of unlocked software upgrades
    public List<string> UnlockedSoftwareUpgrades;
    // List of bought weapon attachments
    public List<string> UnlockedWeaponAttachments;
    // List of bought gadgets
    public List<string> UnlockedGadgets;
    // Loadouts that the player has configured
    public List<Loadout> Loadouts;
    // Index of the selected loadout
    public int LoadoutIndex;
    // List of completed runs
    public RunInfoHistory Runs;
    // Percentage of story completion
    public int StoryCompletion => 0;
    // Selected loadout
    public Loadout SelectedLoadout => Loadouts[LoadoutIndex];

    //public static Save Read(string filename = "/gamesave.save")
    //{
    //    var json = File.ReadAllText(Application.persistentDataPath + filename);
    //    return JsonUtility.FromJson<Save>(json);
    //}

    //public static bool Write(Save save, string filename = "/gamesave.save")
    //{
    //    var path = Application.persistentDataPath + filename;

    //    if (!File.Exists(path))
    //    {
    //        return false;
    //    }

    //    var json = JsonUtility.ToJson(save);
    //    var writer = new StreamWriter(path);
    //    writer.Write(json);
    //    writer.Close();

    //    return true;
    //}
}
