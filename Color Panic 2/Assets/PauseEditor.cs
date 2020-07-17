using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseEditor : MonoBehaviour
{
    private void Start() {
        Time.timeScale = 0;
    }

    public void TimePause(){
        if(Time.timeScale == 1){
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
        }
    }
}
