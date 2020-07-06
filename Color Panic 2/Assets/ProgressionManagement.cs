using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionManagement : MonoBehaviour
{
    
    [SerializeField] List<GameObject> Levels;
    public static int progression;


    private void Update() {
        EnableLevels();
    }

    private void EnableLevels(){
        for(int i = 0; i < progression; i++){
            Levels[i].SetActive(true);
        }
    }
}
