using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadTest : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager = null;
    [SerializeField] private GameObject windowTestable = null;
    [SerializeField] private GameObject windowNotTestable = null;
    [SerializeField] private TMP_Text missingElements = null;
   

    public void CheckLevelTestable() {
        if(levelManager.getPlayerPlaced() == new Vector3(-1,-1,-1) || !levelManager.getFinishPlaced()) {
            windowNotTestable.SetActive(true);
            string elements = "";
            if(levelManager.getPlayerPlaced() == new Vector3(-1,-1,-1))
                elements = "Player";
            if(!levelManager.getFinishPlaced()) {
                elements = elements == "" ? "" : elements+"\n";
                elements += "Finish flag";
            }
            missingElements.SetText(elements);
        } else {
            windowTestable.SetActive(true);
        }
    }
}
