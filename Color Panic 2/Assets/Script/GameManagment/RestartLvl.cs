using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLvl : MonoBehaviour
{
    public string level;
    public string folder;
    public void RestartLevel(){
        Time.timeScale = 1;
        LoadScenes LS = FindObjectOfType<LoadScenes>();
        LS.LoadLevel();
        LS.SetFolder(folder);
        LS.levelName = level;
        LS.loaded = false;
    }
}
