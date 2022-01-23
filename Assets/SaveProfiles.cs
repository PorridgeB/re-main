using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveProfiles : MonoBehaviour
{
    public enum Mode
    {
        Continue,
        NewGame,
        Delete,
    }

    public Mode CurrentMode = Mode.Continue;

    [SerializeField]
    private GameObject profiles;

    private void Start()
    {
        var save = new Save { DataFragments = 42, Scrap = 64, TotalTime = 300, Loadouts = new List<Loadout> { new Loadout { Weapon1 = "A", Weapon2 = "B", Gadget = "C" } } };

        Debug.Log(JsonUtility.ToJson(save));

        foreach (var saveProfile in profiles.GetComponentsInChildren<SaveProfile>())
        {
            saveProfile.Save = save;
            saveProfile.Refresh();
        }
    }

    private void OnSaveProfileSelected(SaveProfile saveProfile)
    {
        switch (CurrentMode)
        {
            case Mode.Continue:
                break;
            case Mode.NewGame:
                break;
            case Mode.Delete:
                break;
        }
    }
}
