using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveProfiles : MonoBehaviour
{
    public enum Mode
    {
        Continue,
        NewGame,
    }

    public Mode CurrentMode = Mode.Continue;

    [SerializeField]
    private Toggle delete;
    [SerializeField]
    private GameObject profiles;
    [SerializeField]
    private GameObject yesNoDialog;
    [SerializeField]
    private SaveManager saveManager;

    private void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        int i = 0;
        foreach (var saveProfile in profiles.GetComponentsInChildren<SaveProfile>())
        {
            saveProfile.Save = saveManager.GetSave(i);
            saveProfile.Interactable = true;
            saveProfile.index = i;
            saveProfile.Refresh();
            i++;
        }
        foreach (var saveProfile in profiles.GetComponentsInChildren<SaveProfile>())
        {
            saveProfile.Interactable = CurrentMode == Mode.NewGame || (CurrentMode == Mode.Continue && saveProfile.Save != null);
        }
    }

    private void OnSaveProfileSelected(SaveProfile saveProfile)
    {
        if (delete.isOn && !saveProfile.IsEmpty)
        {
            var dialog = Instantiate(yesNoDialog, transform).GetComponent<YesNoDialog>();

            dialog.Prompt = "Are you sure you want to delete this save profile?";
            dialog.OnYes += delegate { saveProfile.Delete(); saveProfile.Refresh(); Refresh(); };

            delete.isOn = false;

            return;
        }

        delete.isOn = false;
    
        if (CurrentMode == Mode.NewGame)
        {
            if (saveProfile.IsEmpty)
            {
                saveManager.CreateNewSave(saveProfile.index);
                SceneManager.LoadScene("Hub");
            }
            else
            {
                var dialog = Instantiate(yesNoDialog, transform).GetComponent<YesNoDialog>();

                dialog.Prompt = "Are you sure you want to overwrite this save profile?";
                dialog.OnYes += delegate { saveProfile.Delete(); saveProfile.Refresh(); Refresh(); SceneManager.LoadScene("Hub"); };
            }
            
            return;
        }

        if (CurrentMode == Mode.Continue && !saveProfile.IsEmpty)
        {
            saveManager.SetCurrentSave(saveProfile.index);
            SceneManager.LoadScene("Hub");
        }
    }
    
    public void EnterContinue()
    {
        CurrentMode = Mode.Continue;
        Refresh();
    }

    public void EnterNewGame()
    {
        CurrentMode = Mode.NewGame;
        Refresh();
    }
}
