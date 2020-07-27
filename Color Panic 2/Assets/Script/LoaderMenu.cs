using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoaderMenu : MonoBehaviour
{
    public void LoadEditor()
    {
        LoadScenes.Instance.LoadEditor();
    }

    public void Exit()
    {
        LoadScenes.Instance.Exit();
    }

    public void LoadLevelSelection()
    {
        LoadScenes.Instance.LoadLevelSelection();
    }

    public void LoadLevel(TMP_Text name)
    {
        LoadScenes.Instance.SetFolder("PlayerLevels");
        LoadScenes.Instance.SetupLevelName(name.text);
        LoadScenes.Instance.LoadLevel();
    }

    public void LoadLevel(string name,string folder)
    {
        LoadScenes.Instance.SetFolder(folder);
        LoadScenes.Instance.SetupLevelName(name);
        LoadScenes.Instance.LoadLevel();
    }
}
