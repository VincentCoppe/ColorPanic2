using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MyPower
{
    public List<Power> Powers;
    public string LastPower;

    public MyPower(){
        LastPower = null;
        Powers = new List<Power>();
    }

    public void AddPower(Power power){
        if (!Powers.Contains(power)){
            Powers.Add(power);
        }
        LastPower = power.GetType().ToString();
    }

    public void ResetPowers(){
        foreach(Power p in Powers){
            p.gameObject.SetActive(true);
        }
        Powers = new List<Power>();
        LastPower = null;
    }

    public bool HavePower(string power){
        return (power == LastPower);
    }
}
