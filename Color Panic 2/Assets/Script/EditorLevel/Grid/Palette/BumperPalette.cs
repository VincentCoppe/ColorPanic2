using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BumperPalette : Palette
{
    [SerializeField] private Image BumperIcon;
    [SerializeField] private GameObject Palette;

    public override void ChangeTile(ToolManager manager)
    {

        base.ChangeTile(manager);
        Sprite color = GetComponent<Image>().sprite;
        BumperIcon.sprite = color;
        Palette.SetActive(false);
        GetComponent<Image>().color = Color.white;
        MasterPalette.updateActivePalette(BumperIcon);
    }
}
