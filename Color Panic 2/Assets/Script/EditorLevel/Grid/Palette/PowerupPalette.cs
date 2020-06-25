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
        Color color = GetComponent<Image>().color;
        
        base.ChangeTile(manager);
        PowerupIcon.color = color;
        Palette.SetActive(false);
        GetComponent<Image>().color = color;
    }



}
