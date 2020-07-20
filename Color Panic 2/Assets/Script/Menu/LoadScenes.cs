using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    private string levelName;
    private bool loaded = false;
    private string folder;

    public void Awake()
    {
        DontDestroyOnLoad(this.transform);
    }
    public void LoadLevel(){
        SceneManager.LoadScene("Level");
    }

    public void LoadEditor()
    {
        SceneManager.LoadScene("Editor");
    }

    public void SetupLevelName(TMP_Text name)
    {
        levelName = name.text;
    }

    public void SetupLevelName(LoadListMenu loadlistmenu)
    {
        levelName = loadlistmenu.selectedFile;
    }

    public void SetFolder(string fold){
        this.folder = fold;
    }

    public void Update()
    {
        if(SceneManager.GetSceneByName("Level")== SceneManager.GetActiveScene() && !loaded){         
            FindObjectOfType<LevelSaveLoad>().LoadLevel(levelName, folder);
            loaded = true;
            PlayerController p = FindObjectOfType<PlayerController>(true);
            GameManagement gm = FindObjectOfType<GameManagement>();
            p.transform.SetParent(null);
            gm.SetPlayer(FindObjectOfType<PlayerController>());
            gm.SetCurrentLevel(levelName);
            gm.SetCurrentFolder(folder);
            gm.SetCamera(Mathf.FloorToInt((p.transform.position.x+25)/50), Mathf.FloorToInt((p.transform.position.y+15)/30));
            FindObjectOfType<LevelManager>().SetCurrentGameManager(Mathf.FloorToInt((p.transform.position.x+25)/50), Mathf.FloorToInt((p.transform.position.y+15)/30));
        }
    }


    public void Exit(){
        Application.Quit();
    }
}
