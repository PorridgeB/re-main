using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;
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
        AssetDatabase.Refresh();

        //saves.saves = new Save[] { new Save(), new Save(), new Save(), new Save()};
        ImportSaves();

        DontDestroyOnLoad(gameObject);

        SceneManager.sceneUnloaded += UpdateTime;
    }

    public Save GetSave(int index)
    {
        if (saves.saves[index].TotalTime == 0) return null;

        return saves.saves[index];
    }

    public void SetCurrentSave(int index)
    {
        currentSave.ImportSave(saves.saves[index]);
    }

    public void OnApplicationQuit()
    {
        UpdateTime(SceneManager.GetActiveScene());
        ExportSaves();
    }

    public void ImportSaves()
    {
        Debug.Log("Importing Saves");
        saves = JsonUtility.FromJson<SaveList>(jsonFile.text);
    }

    public void ExportSaves()
    {
        Debug.Log("Exporting Saves");
        string strOutput = JsonUtility.ToJson(saves);
        File.WriteAllText(Application.dataPath + "/saves.txt", strOutput);
    }

    public void CreateNewSave(int i)
    {
        Save s = new Save();
        saves.saves.SetValue(s, i);
        SetCurrentSave(i);
    }

    public void UpdateTime(Scene current)
    {
        if (current.name == "Level" || current.name == "Hub")
        {
            currentSave.AddTime(Time.timeSinceLevelLoad);
        }
    }
}
