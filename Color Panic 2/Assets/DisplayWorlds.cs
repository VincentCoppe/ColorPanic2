using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayWorlds : MonoBehaviour
{
    [SerializeField] List<Canvas> Worlds;

    public void DisplayW1(){
        Worlds[1].gameObject.SetActive(false);
        Worlds[0].gameObject.SetActive(true);
    }

    public void DisplayW2(){
        Worlds[0].gameObject.SetActive(false);
        Worlds[1].gameObject.SetActive(true);
    }
}
