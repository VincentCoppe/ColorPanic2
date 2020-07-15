using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    private TMP_Text levelName;
    private bool loaded = false;
    public LevelSaveLoad aaaaa;

    public void Awake()
    {
        DontDestroyOnLoad(this.transform);
    }
    public void LoadLevel(){
        SceneManager.LoadScene("Level");
    }

    public void LoadEditor()
    {
        SceneManager.LoadScene("GridTest");
    }

    public void SetupLevelName(TMP_Text name)
    {
        levelName = name;
    }

    public void Update()
    {
        if(SceneManager.GetSceneByName("Level")== SceneManager.GetActiveScene() && !loaded)
        {         
            aaaaa = FindObjectOfType<LevelSaveLoad>();
          //  aaaaa.LoadLevel(levelName);
            loaded = true;
        }
        else if(SceneManager.GetSceneByName("Menu") == SceneManager.GetActiveScene() && loaded)
        {
            loaded = false;
        }
    }


    public void Exit(){
        Application.Quit();
    }
}
