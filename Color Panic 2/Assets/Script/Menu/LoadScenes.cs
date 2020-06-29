using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    public void LoadGame(){
        SceneManager.LoadScene("GameScene");
    }

    public void LoadEditor(){
        SceneManager.LoadScene("GridTest");
    }
}
