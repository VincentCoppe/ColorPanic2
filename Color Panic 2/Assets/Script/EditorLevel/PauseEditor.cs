using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseEditor : MonoBehaviour
{
    [SerializeField] private Image image = null;
    private void Start() {
        Time.timeScale = 0;
        image.color = new Color(1,0,0);
    }

    public void TimePause(){
        if(Time.timeScale == 1){
            Time.timeScale = 0;
            image.color = new Color(1,0,0);
        } else {
            Time.timeScale = 1;
            image.color = new Color(0,1,0);
        }
    }
}
