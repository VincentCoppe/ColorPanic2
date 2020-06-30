using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{

    [SerializeField] PlayerController Player;
    [SerializeField] GameObject Camera;
    [SerializeField] TMP_Text WinText;
    [SerializeField] GameObject PauseMenu;
    private bool pause = false;
    private bool setActive = false;

    void Update()
    {
        CameraManagement();
        WinManagement();
        PauseManagement();

        if(Input.GetKeyDown("escape")){pause = !pause;}
    }

    private void CameraManagement(){
        float PlayerLoc = Player.transform.localPosition.x;
        float CameraLoc = Camera.transform.localPosition.x;
        float DistanceX = CameraLoc - PlayerLoc;
        float PlayerLocY = Player.transform.localPosition.y;
        float CameraLocY = Camera.transform.localPosition.y;
        float DistanceY = CameraLocY - PlayerLocY;
        if (DistanceX < -18) {
            Player.transform.localPosition = new Vector3(Player.transform.localPosition.x+1, Player.transform.localPosition.y, Player.transform.localPosition.z);
            Camera.transform.localPosition = new Vector3(Camera.transform.localPosition.x+35.31f, Camera.transform.localPosition.y, Camera.transform.localPosition.z);
        } else if (DistanceX > 18) {
            Player.transform.localPosition = new Vector3(Player.transform.localPosition.x-1, Player.transform.localPosition.y, Player.transform.localPosition.z);
            Camera.transform.localPosition = new Vector3(Camera.transform.localPosition.x-35.31f, Camera.transform.localPosition.y, Camera.transform.localPosition.z);
        }
        if (DistanceY > 9.1) {
            Player.transform.localPosition = new Vector3(Player.transform.localPosition.x, Player.transform.localPosition.y-1, Player.transform.localPosition.z);
            Camera.transform.localPosition = new Vector3(Camera.transform.localPosition.x, Camera.transform.localPosition.y-20, Camera.transform.localPosition.z);
        } else if (DistanceY < -9.1){
            Player.transform.localPosition = new Vector3(Player.transform.localPosition.x, Player.transform.localPosition.y+1, Player.transform.localPosition.z);
            Camera.transform.localPosition = new Vector3(Camera.transform.localPosition.x, Camera.transform.localPosition.y+20, Camera.transform.localPosition.z);
        }
    }

    private void WinManagement(){
        if (Player.win){
            WinText.transform.gameObject.SetActive(true);
            StartCoroutine(WaitForWin());
        }
    }

    IEnumerator WaitForWin(){
            yield return new  WaitForSeconds(4);
            SceneManager.LoadScene("Menu");
    }

    private void PauseManagement(){
        Player.pause = pause;
        if (setActive && pause && !PauseMenu.transform.gameObject.active){
            pause = false;
            setActive = false;
            return;
        }
        if (pause){
            PauseMenu.transform.gameObject.SetActive(true);
            setActive = true;
            return;
        }
        
        PauseMenu.transform.gameObject.SetActive(false);
        setActive = false;
    }


}
