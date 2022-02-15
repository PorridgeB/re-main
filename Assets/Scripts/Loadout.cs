using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Loadout
{
    public List<SoftwareUpgradeInstance> SoftwareUpgrades;
    public WeaponSlotConfiguration Weapon1;
    public WeaponSlotConfiguration Weapon2;
    public Gadget Gadget;
}
