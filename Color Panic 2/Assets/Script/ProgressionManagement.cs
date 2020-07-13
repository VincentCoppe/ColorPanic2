using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [System.Serializable]
public class ProgressionManagement : MonoBehaviour
{

    private void Awake() {
        progression = SaveProgression.LoadProgression();
    }

    [SerializeField] List<GameObject> Levels;
    [SerializeField] List<GameObject> SLevels;
    public static List<int> progression;
    [SerializeField] int NumberOfLevels = 3;

    private void Update() {
        EnableLevels();
        EnableSpecialsLevels();
    }

    private void EnableLevels(){
        for(int i = 0; i <= progression[0]; i++){
            Levels[i].SetActive(true);
        }
    }

    private void EnableSpecialsLevels(){
        int coins = 0;
        for(int i = 1; i <= NumberOfLevels; i++){
            coins += progression[i];
        }
        for (int i = 0; i <= (coins/9)-1; i++){
            SLevels[i].SetActive(true);
        }
    }
}
