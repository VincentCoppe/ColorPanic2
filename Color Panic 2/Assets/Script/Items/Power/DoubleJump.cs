using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : Power
{
    public override void ApplyEffect(Collider2D other){
        other.gameObject.SendMessage("AddPower", this);
    }
}
