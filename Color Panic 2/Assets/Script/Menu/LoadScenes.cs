using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    public string levelName;
    public bool loaded = false;
    private string folder;
    
    public bool restart = false;
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
            PlayerController p = FindObjectOfType<PlayerController>(true);
            GameManagement gm = FindObjectOfType<GameManagement>();
            p.transform.SetParent(null);
            gm.SetPlayer(FindObjectOfType<PlayerController>());
            gm.SetCurrentLevel(levelName);
            gm.SetCurrentFolder(folder);
            if (!restart) { 
                gm.SetCamera(Mathf.FloorToInt((p.transform.position.x+25)/50), Mathf.FloorToInt((p.transform.position.y+15)/30));
                FindObjectOfType<LevelManager>().ChangeLevelIG(Mathf.FloorToInt((p.transform.position.x+25)/50), Mathf.FloorToInt((p.transform.position.y+15)/30));
            }
            loaded = true;
            restart = false;
        }
    }


    public void Exit(){
        Application.Quit();
    }
}
