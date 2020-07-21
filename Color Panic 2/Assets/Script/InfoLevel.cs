using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoLevel : MonoBehaviour
{
    [SerializeField] private TMP_Text LevelName = null;
    [SerializeField] private TMP_Text Death = null;
    [SerializeField] private TMP_Text Coin = null;
    [SerializeField] private TMP_Text Timer = null;
    [SerializeField] private Image Score = null;
    [SerializeField] private Sprite[] ScoreSprite;
    [SerializeField] private LoaderMenu loader;
    private string levelNameForLoad;


    public void UpdateInfoLevel(LevelWorld level)
    {
        LevelName.text = "Level " + (level.levelWorld + 1) + "-" + (level.levelNum + 1);
        levelNameForLoad = level.levelWorld + "-" + level.levelNum;
        if (level.levelcomplete)
        {
            Death.text = level.Death + "";
            Coin.text = level.CoinPlayer + "/" + level.CoinTotal;
            Timer.text = (Mathf.FloorToInt(level.Timer / 6000)).ToString("00") + ":" + (Mathf.FloorToInt((level.Timer % 6000) / 100)).ToString("00") + ":" + (Mathf.FloorToInt((level.Timer % 6000) % 100)).ToString("00");
            Score.sprite = ScoreSprite[level.Score - 3];
            Score.gameObject.SetActive(true);
        } else
        {
            Score.gameObject.SetActive(false);
            Coin.text = "0/" + level.CoinTotal;
            Death.text = "";
            Timer.text = "";
            
        }
    }

    public void LoadLevel()
    {
        loader.LoadLevel(levelNameForLoad,"GameLevels");
    }

}
