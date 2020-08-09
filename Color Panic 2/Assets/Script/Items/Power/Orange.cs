using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orange : Power
{
    LevelManager levelManager;
    public override void ApplyEffect(Collider2D other){
        other.gameObject.SendMessage("AddPower", this);
        if (!levelManager)
        {
            levelManager = LevelManager.Instance;

        }
        levelManager.ChangeColor(ColorPicker.Instance.Orange);
    }
}
