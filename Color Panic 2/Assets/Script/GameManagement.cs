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
        float Distance = CameraLoc - PlayerLoc;
        if (Distance < -18) {
            Player.transform.localPosition = new Vector3(Player.transform.localPosition.x+1, Player.transform.localPosition.y, Player.transform.localPosition.z);
            Camera.transform.localPosition = new Vector3(Camera.transform.localPosition.x+35.31f, Camera.transform.localPosition.y, Camera.transform.localPosition.z);
        } else if (Distance > 18) {
            Player.transform.localPosition = new Vector3(Player.transform.localPosition.x-1, Player.transform.localPosition.y, Player.transform.localPosition.z);
            Camera.transform.localPosition = new Vector3(Camera.transform.localPosition.x-35.31f, Camera.transform.localPosition.y, Camera.transform.localPosition.z);
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
