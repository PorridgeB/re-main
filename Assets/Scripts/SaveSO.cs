using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class SaveSO : ScriptableObject
{
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
    // Percentage of story completion
    public int StoryCompletion => 0;

    public Loadout SelectedLoadout => Loadouts[LoadoutIndex];
}
