using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : Power
{
    public override void ApplyEffect(Collider2D other){
        other.gameObject.SendMessage("AddPower", this);
    }
}
