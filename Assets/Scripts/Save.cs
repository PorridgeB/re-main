using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class Save
{
    public Save()
    {
        Created = System.DateTime.Now;
        Difficulty = 1;
        TotalTime = 10;
        DataFragments = 0;
        Scrap = 0;
        SoftwareUpgradeCapacity = 0;
        UnlockedSoftwareUpgrades = new List<string>() { "a", "b", "c" };
        UnlockedGadgets = new List<string>() { "a", "b", "c" };
        Loadouts = new List<Loadout>() { new Loadout() };
        LoadoutIndex = 0;
        Runs = new List<RunInfo>();
    }

    // Date and time the save file was created
    public DateTime Created;
    // Number of seconds spent playing the game (does not include paused time)
    public int TotalTime;
    // Quantity of data fragments in the player's inventory
    public int DataFragments;
    // Quantity of scrap in the player's inventory
    public int Scrap;
    // Maximum number of points allocated for software upgrades
    public int SoftwareUpgradeCapacity;
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
    public List<RunInfo> Runs;
    // The difficulty set by the player from the settings. Determines the difficulty of the next run
    public int Difficulty;
    // Percentage of story completion
    public int StoryCompletion => 0;

    public Loadout SelectedLoadout => Loadouts[LoadoutIndex];
}
