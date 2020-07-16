using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    private TMP_Text levelName;
    private bool loaded = false;

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
        levelName = name;
    }

    public void Update()
    {
        if(SceneManager.GetSceneByName("Level")== SceneManager.GetActiveScene() && !loaded){         
            FindObjectOfType<LevelSaveLoad>().LoadLevel(levelName.text);
            loaded = true;
            PlayerController p = FindObjectOfType<PlayerController>(true);
            GameManagement gm = FindObjectOfType<GameManagement>();
            p.transform.SetParent(null);
            gm.SetPlayer(FindObjectOfType<PlayerController>());
            gm.SetCurrentLevel(levelName.text);
            gm.SetCamera(Mathf.FloorToInt((p.transform.position.x+25)/50), Mathf.FloorToInt((p.transform.position.y+15)/30));
            FindObjectOfType<LevelManager>().SetCurrentGameManager(Mathf.FloorToInt((p.transform.position.x+25)/50), Mathf.FloorToInt((p.transform.position.y+15)/30));
        }
    }


    public void Exit(){
        Application.Quit();
    }
}
