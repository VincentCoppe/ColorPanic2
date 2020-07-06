using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    public void LoadLevel1(){
        SceneManager.LoadScene("Level1");
    }
    
    public void LoadLevel2(){
        SceneManager.LoadScene("Level2");
    }

    public void LoadEditor(){
        SceneManager.LoadScene("GridTest");
    }
}
