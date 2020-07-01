using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{

    [SerializeField] PlayerController Player;
    [SerializeField] Camera Camera;
    [SerializeField] GameObject WinText;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] TMP_Text PowerText;
    private bool pause = false;
    private bool setActive = false;
    private string savedPower;

    private float MovementX;
    private float MovementY;
    [SerializeField] private float Lenght = 1.8f;
    [SerializeField] private float Width = 0.91f;

    private void Start() {
        MovementX = Lenght*Camera.orthographicSize;
        MovementY = Width*Camera.orthographicSize;
        Player.OppositeX = MovementX*2;
        Player.OppositeY = MovementY*2;
    }

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
        float size = Camera.orthographicSize;
        float PlayerLoc = Player.transform.localPosition.x;
        float CameraLoc = Camera.transform.localPosition.x;
        float DistanceX = CameraLoc - PlayerLoc;
        float PlayerLocY = Player.transform.localPosition.y;
        float CameraLocY = Camera.transform.localPosition.y;
        float DistanceY = CameraLocY - PlayerLocY;
        if (DistanceX < (-Lenght*size)) {
            Player.transform.localPosition = new Vector3(Player.transform.localPosition.x+1, Player.transform.localPosition.y, Player.transform.localPosition.z);
            Camera.transform.localPosition = new Vector3(Camera.transform.localPosition.x+(3.531f*size), Camera.transform.localPosition.y, Camera.transform.localPosition.z);
            //Player.transform.localPosition = new Vector3(Player.transform.localPosition.x-(1.8f*2*size), Player.transform.localPosition.y, Player.transform.localPosition.z);
        } else if (DistanceX > (Lenght*size)) {
            Player.transform.localPosition = new Vector3(Player.transform.localPosition.x-1, Player.transform.localPosition.y, Player.transform.localPosition.z);
            Camera.transform.localPosition = new Vector3(Camera.transform.localPosition.x-(3.531f*size), Camera.transform.localPosition.y, Camera.transform.localPosition.z);
        }
        if (DistanceY > (Width*size)) {
            Player.transform.localPosition = new Vector3(Player.transform.localPosition.x, Player.transform.localPosition.y-2f, Player.transform.localPosition.z);
            Camera.transform.localPosition = new Vector3(Camera.transform.localPosition.x, Camera.transform.localPosition.y-(2f*size), Camera.transform.localPosition.z);
        } else if (DistanceY < (-Width*size)){
            Player.transform.localPosition = new Vector3(Player.transform.localPosition.x, Player.transform.localPosition.y+2f, Player.transform.localPosition.z);
            Camera.transform.localPosition = new Vector3(Camera.transform.localPosition.x, Camera.transform.localPosition.y+(2f*size), Camera.transform.localPosition.z);
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
