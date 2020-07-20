using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseEditor : MonoBehaviour
{
    [SerializeField] private Image image = null;
    [SerializeField] private LevelManager levelManager = null;

    private void Start() {
        Time.timeScale = 0;
        image.color = new Color(1,0,0);
    }

    public void TimePause(){
        if(Time.timeScale == 1){
            Vector3 coords = levelManager.getPlayerPlaced();
            if(coords != new Vector3(-1, -1, -1)) {
                PlayerController pc = FindObjectOfType<PlayerController>(true);
                pc.transform.position = coords;
                pc.ResetMovement();
            }
            Time.timeScale = 0;
            image.color = new Color(1,0,0);
        } else {
            Time.timeScale = 1;
            image.color = new Color(0,1,0);
        }
    }
}
