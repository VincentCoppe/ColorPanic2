using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [System.Serializable]
public class ProgressionManagement : MonoBehaviour
{

    private void Awake() {
        progression = SaveProgression.LoadProgression();
        Debug.Log("Load : "+progression);
    }

    [SerializeField] List<GameObject> Levels;
    public static int progression;

    private void Update() {
        EnableLevels();
    }

    private void EnableLevels(){
        for(int i = 0; i <= progression; i++){
            Levels[i].SetActive(true);
        }
    }
}
