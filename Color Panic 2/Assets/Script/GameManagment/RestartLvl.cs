﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLvl : MonoBehaviour
{
    public string level;
    public string folder;
    private void Update() {
        if(Input.GetKey(KeyCode.F)){
            Time.timeScale = 0;
            RestartLevel();
        }
    }
    public void RestartLevel(){
        LoadScenes.Instance.LoadLevel();
        
    }
}
