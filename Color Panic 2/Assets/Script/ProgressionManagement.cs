using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Text.RegularExpressions;

[System.Serializable]
public class ProgressionManagement : MonoBehaviour
{



    public GameObject[] Worlds;
    private Dictionary<string, LevelWorld> Levels = new Dictionary<string, LevelWorld>();
    
    int TotalCoins;
    int TotalCoinsGot;
    int[] TotalCoinsWorld = new int[8];
    int[] TotalCoinsWorldGot= new int[8];
    public int ActiveWorld = 0;
     
    public static Dictionary<string,string> progression;

    private void Awake() {
        progression = SaveProgression.LoadProgression();
        SetupLevels();
        UpdateLevelsInfo();
        SetTotalCoins();
        EnableLevels();
    }

    private void Start() {
        //UpdateText();
        
    }

    private void SetupLevels()
    {
       

        LevelWorld[] levelWorlds = Worlds[ActiveWorld].GetComponentsInChildren<LevelWorld>();
        foreach(LevelWorld level in levelWorlds)
        {
            Levels[level.levelWorld + "-" + level.levelNum] = level;
            level.SetScore();
        }
        
    }

    private void UpdateLevelsInfo()
    {
        
        foreach (KeyValuePair<string, string> level in progression)
        {
            
            Levels[level.Key].levelWorld = int.Parse(level.Key.Split('-')[0]);
            Levels[level.Key].levelNum = int.Parse(level.Key.Split('-')[1]);
            Levels[level.Key].Death = int.Parse(level.Value.Split('-')[0]);   
            Levels[level.Key].CoinPlayer = int.Parse(level.Value.Split('-')[1]);
            string[] time = level.Value.Split('-')[2].Split(':');
            Levels[level.Key].Timer = int.Parse(time[2]) + int.Parse(time[1]) * 100 + int.Parse(time[0]) * 60 * 100;
            Levels[level.Key].levelcomplete = true;
            Levels[level.Key].SetScore();
        }
    }

    private void EnableLevels(){
        foreach (KeyValuePair<string, LevelWorld> level in Levels)
        {
            if(int.Parse(level.Key.Split('-')[0]) == ActiveWorld)
            {
                if(Regex.Match(level.Key, @"[0-9]+-0").Success) level.Value.Unlock();
                
                else if(level.Value.secret)
                {
                    if (TotalCoinsWorld[ActiveWorld] == TotalCoinsWorldGot[ActiveWorld]) level.Value.Unlock();
                }
                else if (Levels[level.Value.levelWorld + "-" + (level.Value.levelNum-1)].levelcomplete) level.Value.Unlock();
                level.Value.SetScore();
            }

        }
        
    }
    /*
    private void UpdateText(){
        for (int i = 1; i <= NumberOfLevels; i++){
            Coins[i-1].text = progression[i].ToString()+"/3";
        }
    }
    */
    private void SetTotalCoins(){
        TotalCoins = 0;
        TotalCoinsGot = 0;
        foreach(KeyValuePair<string, LevelWorld> level in Levels){
            TotalCoins += level.Value.CoinTotal;
            TotalCoinsGot += level.Value.CoinPlayer;
            TotalCoinsWorld[level.Value.levelWorld] += level.Value.CoinTotal;
            TotalCoinsWorldGot[level.Value.levelWorld] += level.Value.CoinPlayer;
        }
        
    }
}
