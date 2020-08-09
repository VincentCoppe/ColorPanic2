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
    [SerializeField] TMP_Text DeathText;
    [SerializeField] TMP_Text CoinText;
    [SerializeField] TMP_Text TimerText;
    [SerializeField] RestartLvl restart;
    [SerializeField] Color Red;
    [SerializeField] Color Purple;
    [SerializeField] Color Viridian;
    [SerializeField] Color Green;
    [SerializeField] Color Yellow;
    [SerializeField] Color Orange;
    public bool pause = false;
    private bool setActive = false;
    private string savedPower;

    private float MovementX;
    private float MovementY;
    private string CurrentMap;
    private string CurrentFolder;
    private double Timer = 00;
    private int TimerMin = 00;
    private double TimerMillis = 00;
    [SerializeField] private float Lenght = 1.8f;
    [SerializeField] private float Width = 0.91f;

    public void SetPlayer(PlayerController player){
        this.Player = player;
        Player.OppositeX = 48.5f;
        Player.OppositeY = 28.5f;
    }

    public void SetCurrentLevel(string level){
        this.CurrentMap = level;
        restart.level = CurrentMap;
    }

    public void SetCurrentFolder(string folder){
        this.CurrentFolder = folder;
        restart.folder = CurrentFolder;
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
        UpdateText();
        if(Input.GetKeyDown("escape")){pause = !pause;}
    }

    private void FixedUpdate() {
        HandleTimer();
    }

    private void HandleTimer(){
        if (Player != null && !Player.win) Timer += Time.deltaTime;
        TimerMillis = (int)((Timer - (int)Timer) * 100);
        if (Timer >= 60){
            TimerMin++;
            Timer = Timer-60;
        }
    }

    public void ResetTimer(){
        Timer = 0;
        TimerMillis = 0;
        TimerMin = 0;
    }

    private void UpdateText(){
        DeathText.text = Player.death.ToString();
        CoinText.text = Player.coin.ToString();
        TimerText.text = TimerMin.ToString("00")+":"+Timer.ToString("00")+":"+TimerMillis.ToString("00");
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
            case "Green" : PowerText.text = "You can now double jump"; PowerText.color = Green; break;
            case "Red" : PowerText.text = "You can now dash"; PowerText.color = Red; break;
            case "Viridian" : PowerText.text = "You can now reverse the gravity"; PowerText.color = Viridian; break;
            case "Purple" : PowerText.text = "You can now teleport"; PowerText.color = Purple; break;
            case "Yellow" : PowerText.text = "You can now Wall jump"; PowerText.color = Yellow; break;
            case "Orange" : PowerText.text = "You can now Fly"; PowerText.color = Orange; break;
            default : PowerText.text = "You lost your power"; PowerText.color = Color.white; break;
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
            Player.transform.localPosition = new Vector3(Player.transform.localPosition.x, Player.transform.localPosition.y, Player.transform.localPosition.z);
            Camera.transform.localPosition = new Vector3(Camera.transform.localPosition.x+(50), Camera.transform.localPosition.y, Camera.transform.localPosition.z);
            LvManager.ChangeLevelIG(1,0);
        } else if (DistanceX > (Lenght*size)) {
            Player.transform.localPosition = new Vector3(Player.transform.localPosition.x, Player.transform.localPosition.y, Player.transform.localPosition.z);
            Camera.transform.localPosition = new Vector3(Camera.transform.localPosition.x-(50), Camera.transform.localPosition.y, Camera.transform.localPosition.z);
            LvManager.ChangeLevelIG(-1,0);
        }
        if (DistanceY > (Width*size+1)) {
            Player.transform.localPosition = new Vector3(Player.transform.localPosition.x, Player.transform.localPosition.y, Player.transform.localPosition.z);
            Camera.transform.localPosition = new Vector3(Camera.transform.localPosition.x, Camera.transform.localPosition.y-(30), Camera.transform.localPosition.z);
            LvManager.ChangeLevelIG(0,-1);
        } else if (DistanceY < (-Width*size-1)){
            Player.transform.localPosition = new Vector3(Player.transform.localPosition.x, Player.transform.localPosition.y, Player.transform.localPosition.z);
            Camera.transform.localPosition = new Vector3(Camera.transform.localPosition.x, Camera.transform.localPosition.y+(30), Camera.transform.localPosition.z);
            LvManager.ChangeLevelIG(0,1);
        }
    }

    private void WinManagement(){
        if (Player.win){
            WinText.transform.gameObject.SetActive(true);
            Time.timeScale = 0;
            StartCoroutine(WaitForWin());
        }
    }

    IEnumerator WaitForWin(){
            yield return new  WaitForSeconds(3f);
            if (CurrentFolder == "GameLevels"){
                string[] nameParse = CurrentMap.Split('-');
                int num = int.Parse(nameParse[1]);
                int world = int.Parse(nameParse[0]);
                Save(num,world);
                LoadScenes.Instance.LoadLevelSelection();
                
            } else
            {
                LoadScenes a = LoadScenes.Instance;
                if (a.TestingLevel){
                    a.LoadEditor();
                    a.setLevelTested(true);
                } else
                {
                    a.LoadMenu();

                }
            }
    }

    private void Save(int num, int world){
        string result = "";
        if (!ProgressionManagement.progression.ContainsKey(world+"-"+num))
        {
            result += Player.death + "-";
            result += Player.coin + "-";
            result += TimerText.text;
        } else
        {
            int oldDeath = int.Parse(ProgressionManagement.progression[world + "-" + num].Split('-')[0]);
            result += Mathf.Min(Player.death, oldDeath) + "-";
            int oldCoin = int.Parse(ProgressionManagement.progression[world + "-" + num].Split('-')[1]);
            result += Mathf.Max(Player.coin, oldCoin) + "-";
            int oldMin = int.Parse(ProgressionManagement.progression[world + "-" + num].Split('-')[2].Split(':')[0]);
            int oldSec = int.Parse(ProgressionManagement.progression[world + "-" + num].Split('-')[2].Split(':')[1]);
            int oldMillis = int.Parse(ProgressionManagement.progression[world + "-" + num].Split('-')[2].Split(':')[2]);
            int oldTime = oldMillis + (oldSec * 100) + (oldMin * 100 * 60);
            int Min = int.Parse(TimerText.text.Split(':')[0]);
            int Sec = int.Parse(TimerText.text.Split(':')[1]);
            int Millis = int.Parse(TimerText.text.Split(':')[2]);
            int newTime = Millis + (Sec * 100) + (Min * 100 * 60);
            result += (newTime<oldTime) ? TimerText.text : oldMin + ":" + oldSec + ":" + oldMillis;
        }
        ProgressionManagement.progression[world + "-" + num] = result;
        SaveProgression.SaveProg(ProgressionManagement.progression);
    }

    private void PauseManagement(){
        Player.pause = pause;
        if (setActive && pause && !PauseMenu.gameObject.activeInHierarchy){
            pause = false;
            setActive = false;
            Time.timeScale = 1; 
            return;
        }
        if (pause){
            PauseMenu.transform.gameObject.SetActive(true);
            setActive = true;
            Time.timeScale = 0; 
            return;
        }
        Time.timeScale = 1; 
        PauseMenu.transform.gameObject.SetActive(false);
        setActive = false;
    }


}
