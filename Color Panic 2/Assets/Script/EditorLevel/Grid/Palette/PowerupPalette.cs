using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupPalette : Palette
{

    [SerializeField] private Image PowerupIcon;
    [SerializeField] private GameObject Palette;

    public override void ChangeTile(ToolManager manager)
    {
        Sprite sprite = GetComponent<Image>().sprite;
        Color color = GetComponent<Image>().color;
        base.ChangeTile(manager);
        PowerupIcon.sprite = sprite;
        Palette.SetActive(false);
        GetComponent<Image>().color = color;
    }



}
