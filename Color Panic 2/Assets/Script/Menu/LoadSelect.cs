using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSelect : MonoBehaviour
{
    [SerializeField]GameObject Menu;
    [SerializeField]GameObject Levels;

    public void DisplayLevels(){
        Menu.gameObject.SetActive(false);
        Levels.gameObject.SetActive(true);
    }

    public void DisplayMenu(){
        Menu.gameObject.SetActive(true);
        Levels.gameObject.SetActive(false);       
    }
}
