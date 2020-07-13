﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

 [System.Serializable]
public class ProgressionManagement : MonoBehaviour
{

    private void Awake() {
        progression = SaveProgression.LoadProgression();
    }

    [SerializeField] List<GameObject> Levels;
    [SerializeField] List<GameObject> SLevels;
    [SerializeField] List<TMP_Text> Coins;
    public static List<int> progression;
    [SerializeField] int NumberOfLevels = 3;

    private void Update() {
        EnableLevels();
        EnableSpecialsLevels();
        SetText();
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
}
