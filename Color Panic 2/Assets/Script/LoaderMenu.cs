using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoaderMenu : MonoBehaviour
{
    public void LoadEditor()
    {
        FindObjectOfType<LoadScenes>().LoadEditor();
    }

    public void Exit()
    {
        FindObjectOfType<LoadScenes>().Exit();
    }

    public void LoadLevelSelection()
    {
        FindObjectOfType<LoadScenes>().LoadLevelSelection();
    }

    public void LoadLevel(TMP_Text name)
    {
        FindObjectOfType<LoadScenes>().SetFolder("PlayerLevels");
        FindObjectOfType<LoadScenes>().SetupLevelName(name.text);
        FindObjectOfType<LoadScenes>().LoadLevel();
    }

    public void LoadLevel(string name,string folder)
    {
        FindObjectOfType<LoadScenes>().SetFolder(folder);
        FindObjectOfType<LoadScenes>().SetupLevelName(name);
        FindObjectOfType<LoadScenes>().LoadLevel();
    }
}
