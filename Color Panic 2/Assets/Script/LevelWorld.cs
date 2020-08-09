using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelWorld : MonoBehaviour
{
    public int Score; /*{ get; private set; }*/
    public int Death;
    public int Timer;
    public int referenceTime = 0;
    public int CoinTotal = 0;
    public int CoinPlayer = 0;
    public bool levelcomplete = false;
    public bool available = false;
    public bool secret = false;
    public int levelWorld = 1;
    public int levelNum = 1;
    public Sprite[] SpriteLevel;
    [SerializeField] private InfoLevel infoLevelWindow;


    public void SetScore()
    {

        Score = (secret) ? 0 : 1;
        if (available) Score = 2;
        if (levelcomplete)
        {
            Score = 3;
            Score += (Death == 0) ? 1 : 0;
            Score += (Timer <= referenceTime) ? 1 : 0;
            Score += (CoinTotal == CoinPlayer) ? 1 : 0;

        }
        UpdateSprite();
    }

    private void UpdateSprite() => GetComponent<SpriteRenderer>().sprite = SpriteLevel[Score];

    public void Unlock() => available = true;

    public void OnMouseDown()
    {
        if (available)
        {
            infoLevelWindow.UpdateInfoLevel(this);
            infoLevelWindow.gameObject.SetActive(true);
        } else if(secret)
        {
            infoLevelWindow.UpdateInfoLevelSecret(this);
            infoLevelWindow.gameObject.SetActive(true);
        }
    }
}
