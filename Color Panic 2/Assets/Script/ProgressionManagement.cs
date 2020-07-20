using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

 [System.Serializable]
public class ProgressionManagement : MonoBehaviour
{

    private void Awake() {
        progression = SaveProgression.LoadProgression();
        times = SaveProgression.LoadTimes();
    }

    [SerializeField] List<GameObject> Levels;
    [SerializeField] List<GameObject> SLevels;
    [SerializeField] List<TMP_Text> Coins;
    [SerializeField] TMP_Text TotalCoins;
    public static List<int> progression;
    public static Dictionary<string,string> times;
    [SerializeField] int NumberOfLevels = 3;

    private void Start() {
        EnableLevels();
        EnableSpecialsLevels();
        SetText();
        SetTotalCoins();
    }

    private void EnableLevels(){
        for(int i = 0; i <= progression[0]; i++){
            Levels[i].SetActive(true);
        }
    }

    private void EnableSpecialsLevels(){
        int coins = 0;
        for(int i = 1; i <= NumberOfLevels; i++){
            coins += progression[i];
        }
        for (int i = 0; i <= (coins/9)-1; i++){
            SLevels[i].SetActive(true);
        }
    }
    
    private void SetText(){
        for (int i = 1; i <= NumberOfLevels; i++){
            Coins[i-1].text = progression[i].ToString()+"/3";
        }
    }

    private void SetTotalCoins(){
        int TCoins = 0;
        for (int i = 1; i <= NumberOfLevels; i++){
            TCoins += progression[i];
        }
        TotalCoins.text = "Coins : "+TCoins;
    }
}
