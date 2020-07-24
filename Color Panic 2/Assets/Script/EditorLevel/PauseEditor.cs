using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseEditor : MonoBehaviour
{
    [SerializeField] private Sprite play = null;
    [SerializeField] private Sprite pause = null;
    [SerializeField] private Image image = null;
    [SerializeField] private LoadTest lt = null;
    [SerializeField] private LevelSaveLoad levelSaveLoad = null;
    [SerializeField] private GameObject windowCantPlay = null;
    [SerializeField] private LevelManager levelManager = null;

    private void Start() {
        Time.timeScale = 0;
        image.color = new Color(1,0,0);
        image.sprite = pause;
    }

    public void TimePause(){
        if(lt.LevelName != "") {
            if(Time.timeScale == 1){
                levelSaveLoad.SaveLevel(lt.LevelName, "PlayerLevelsEditor");
                levelSaveLoad.LoadLevel(lt.LevelName, "PlayerLevelsEditor");
                Time.timeScale = 0;
                image.color = new Color(1,0,0);
                image.sprite = pause;
            } else {
                Time.timeScale = 1;
                image.color = new Color(0,1,0);
                image.sprite = play;
            }
        } else {
            levelManager.gameObject.SetActive(false);
            windowCantPlay.SetActive(true);
        }
    }
}
