using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grey : Power
{
    LevelManager levelManager;
    public override void ApplyEffect(Collider2D other){
        other.gameObject.SendMessage("AddPower", this);
        if (!levelManager)
        {
            levelManager = LevelManager.Instance;

        }
        levelManager.ChangeColor(ColorPicker.Instance.neutral);
    }
}
