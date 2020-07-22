using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour 
{
    public string levelName;
    public string folder = "GameLevels";
    private static LoadScenes _instance;
    public static LoadScenes Instance {  get { return _instance; } }
    public void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);

        } else
        {
            _instance = this;
            DontDestroyOnLoad(this.transform);
            return;

        }
    }
    public void LoadLevel(){

        SceneManager.LoadScene("Level");
    }

    public void LoadLevelSelection()
    {
        SceneManager.LoadScene("LevelSelection");
    }

    public void LoadEditor()
    {
        SceneManager.LoadScene("Editor");
    }

    public void SetupLevelName(string name)
    {
        levelName = name;
    }

    public void SetupLevelName(LoadListMenu loadlistmenu)
    {
        levelName = loadlistmenu.selectedFile;
    }

    public void SetFolder(string fold){
        this.folder = fold;
    }

    public void LoadLevelManager()
    {
            
        FindObjectOfType<LevelSaveLoad>().LoadLevel(levelName, folder);
        PlayerController p = FindObjectOfType<PlayerController>(true);
        GameManagement gm = FindObjectOfType<GameManagement>();
        p.transform.SetParent(null);
        gm.SetPlayer(FindObjectOfType<PlayerController>());
        gm.SetCurrentLevel(levelName);
        gm.SetCurrentFolder(folder);
        gm.SetCamera(Mathf.FloorToInt((p.transform.position.x+25)/50), Mathf.FloorToInt((p.transform.position.y+15)/30));
        FindObjectOfType<LevelManager>().ChangeLevelIG(Mathf.FloorToInt((p.transform.position.x+25)/50), Mathf.FloorToInt((p.transform.position.y+15)/30));
        
    }


    public void Exit(){
        Application.Quit();
    }
}
