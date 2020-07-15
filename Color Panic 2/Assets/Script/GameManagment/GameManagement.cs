using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{

    private PlayerController Player;
    [SerializeField] Camera Camera;
    [SerializeField] LevelManager LvManager;
    [SerializeField] GameObject WinText;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] TMP_Text PowerText;
    [SerializeField] GameObject PowerImg;
    private bool pause = false;
    private bool setActive = false;
    private string savedPower;

    private float MovementX;
    private float MovementY;
    private string CurrentMap;
    [SerializeField] private float Lenght = 1.8f;
    [SerializeField] private float Width = 0.91f;

    public void SetPlayer(PlayerController player){
        this.Player = player;
        MovementX = Lenght*Camera.orthographicSize;
        MovementY = Width*Camera.orthographicSize;
        Player.OppositeX = MovementX*2;
        Player.OppositeY = MovementY*2;
    }

    public void SetCurrentLevel(string level){
        this.CurrentMap = level;
        Debug.Log(CurrentMap);
    }

    public void SetCamera(int x, int y){
        Camera.transform.localPosition = new Vector3(Camera.transform.localPosition.x+(50*x), Camera.transform.localPosition.y, Camera.transform.localPosition.z);
        Camera.transform.localPosition = new Vector3(Camera.transform.localPosition.x, Camera.transform.localPosition.y+(30*y), Camera.transform.localPosition.z);
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
        PowerImg.gameObject.SetActive(true);
        switch (power){
            case "Green" : PowerText.text = "You can now double jump"; break;
            case "Red" : PowerText.text = "You can now dash"; break;
            case "Viridian" : PowerText.text = "You can now reverse the gravity"; break;
            case "Purple" : PowerText.text = "You can now teleport"; break;
            //others
        }
        yield return new WaitForSecondsRealtime(2f);
        PowerText.gameObject.SetActive(false);
        PowerImg.gameObject.SetActive(false);
    }

    private void CameraManagement(){
        float size = Camera.orthographicSize;
        float PlayerLoc = Player.transform.localPosition.x;
        float CameraLoc = Camera.transform.localPosition.x;
        float DistanceX = CameraLoc - PlayerLoc;
        Player.DistanceX = DistanceX;
        float PlayerLocY = Player.transform.localPosition.y;
        float CameraLocY = Camera.transform.localPosition.y;
        float DistanceY = CameraLocY - PlayerLocY;
        Player.DistanceY = DistanceY;
        if (DistanceX < (-Lenght*size)) {
            Player.transform.localPosition = new Vector3(Player.transform.localPosition.x+2, Player.transform.localPosition.y, Player.transform.localPosition.z);
            Camera.transform.localPosition = new Vector3(Camera.transform.localPosition.x+(50), Camera.transform.localPosition.y, Camera.transform.localPosition.z);
            LvManager.ChangeLevelIG(1,0);
        } else if (DistanceX > (Lenght*size)) {
            Player.transform.localPosition = new Vector3(Player.transform.localPosition.x-2, Player.transform.localPosition.y, Player.transform.localPosition.z);
            Camera.transform.localPosition = new Vector3(Camera.transform.localPosition.x-(50), Camera.transform.localPosition.y, Camera.transform.localPosition.z);
            LvManager.ChangeLevelIG(-1,0);
        }
        if (DistanceY > (Width*size+1)) {
            Player.transform.localPosition = new Vector3(Player.transform.localPosition.x, Player.transform.localPosition.y-3f, Player.transform.localPosition.z);
            Camera.transform.localPosition = new Vector3(Camera.transform.localPosition.x, Camera.transform.localPosition.y-(30), Camera.transform.localPosition.z);
            LvManager.ChangeLevelIG(0,-1);
        } else if (DistanceY < (-Width*size-1)){
            Player.transform.localPosition = new Vector3(Player.transform.localPosition.x, Player.transform.localPosition.y+3f, Player.transform.localPosition.z);
            Camera.transform.localPosition = new Vector3(Camera.transform.localPosition.x, Camera.transform.localPosition.y+(30), Camera.transform.localPosition.z);
            LvManager.ChangeLevelIG(0,1);
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
            string namenum = CurrentMap.Split('-')[1];
            int num = int.Parse(namenum);
            if (SceneManager.GetActiveScene().name.Split('-')[0] == "Level"){
                Save(num);
            }
            SceneManager.LoadScene("Menu");
    }

    private void Save(int num){
        if(ProgressionManagement.progression[0] < num) ProgressionManagement.progression[0] = num;
        if(ProgressionManagement.progression[num] < Player.coin) ProgressionManagement.progression[num] = Player.coin;
        SaveProgression.SaveProg(ProgressionManagement.progression);
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
