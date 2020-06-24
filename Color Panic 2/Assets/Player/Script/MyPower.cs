using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MyPower
{
    Dictionary<string, bool> PowersState = new Dictionary<string, bool>();
    public List<Power> Powers;

    public MyPower(){
        Powers = new List<Power>();
        PowersState = new Dictionary<string,
         bool>
        {
            { "Green", false }, //DoubleJump
            { "Red", false } //Dash
        };

    }

    public void AddPower(Power power){
        if (PowersState.ContainsKey(power.GetType().ToString())){
            PowersState[power.GetType().ToString()] = true;
            Powers.Add(power);
        }
    }

    public void ResetPowers(List<Power> SavedPowers){
        foreach(Power p in Powers){
            if (!SavedPowers.Contains(p)){
                p.gameObject.SetActive(true);
            }
        }

        List<string> powernames = new List<string>();
        foreach (Power sp in SavedPowers){
            powernames.Add(sp.GetType().ToString());
        }

        foreach(string p in PowersState.Keys.ToList()){
                if (!powernames.Contains(p)){
                    PowersState[p] = false;
                } 
        }
        Powers = new List<Power>();
    }

    public bool HavePower(string power){
        return PowersState[power];
    }
}
