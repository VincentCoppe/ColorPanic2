using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{

    [SerializeField] PlayerController Player;
    [SerializeField] GameObject Camera;
    [SerializeField] GameObject WinText;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] TMP_Text PowerText;
    private bool pause = false;
    private bool setActive = false;
    private string savedPower;

    void Update()
    {
        CameraManagement();
        WinManagement();
        PauseManagement();
        HandlePower();

        if(Input.GetKeyDown("escape")){pause = !pause;}
    }

    private void HandlePower(){
        if (savedPower != Player.power.LastPower){
            StartCoroutine(DisplayPower(Player.power.LastPower));
            savedPower = Player.power.LastPower;
        }
    }

    IEnumerator DisplayPower(string power){
        PowerText.gameObject.SetActive(true);
        switch (power){
            case "Green" : PowerText.text = "You can now double jump"; break;
            case "Red" : PowerText.text = "You can now dash"; break;
            case "Viridian" : PowerText.text = "You can now reverse the gravity"; break;
            case "Purple" : PowerText.text = "You can now teleport"; break;
            //others
        }
        yield return new WaitForSecondsRealtime(2f);
        PowerText.gameObject.SetActive(false);
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
            yield return new  WaitForSeconds(3f);
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
