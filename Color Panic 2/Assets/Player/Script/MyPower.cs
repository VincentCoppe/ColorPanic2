using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MyPower
{
    Dictionary<string, bool> PowersState = new Dictionary<string, bool>();
    List<Power> Powers = new List<Power>();

    public MyPower(){
        PowersState = new Dictionary<string, bool>
        {
            { "DoubleJump", false },
            { "Fly", false }
        };

    }

    public void AddPower(Power power){
        if (PowersState.ContainsKey(power.GetType().ToString())){
            PowersState[power.GetType().ToString()] = true;
            Powers.Add(power);
        }
    }

    public void ResetPowers(){
        foreach(Power p in Powers){
            p.gameObject.SetActive(true);
        }
        foreach(string p in PowersState.Keys.ToList()){
            PowersState[p] = false;
        }
        Powers = new List<Power>();
    }

    public bool HavePower(string power){
        return PowersState[power];
    }
}
