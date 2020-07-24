using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class LoadTest : MonoBehaviour
{
    
    [SerializeField] private LevelManager levelManager = null;

    [SerializeField] private GameObject windowTestable = null;
    [SerializeField] private Button buttonTest = null;
    [SerializeField] private TMP_Text textConfirm = null;

    [SerializeField] private GameObject windowNotTestable = null;
    [SerializeField] private Leave leave = null;
    [SerializeField] private TMP_Text missingElements = null;

    public string LevelName;
   

    public void CheckLevelTestable() {
        if(levelManager.getPlayerPlaced() == new Vector3(-1,-1,-1) || !levelManager.getFinishPlaced() || leave.ModifUnsaved) {
            windowNotTestable.SetActive(true);
            string elements = "";
            if(levelManager.getPlayerPlaced() == new Vector3(-1,-1,-1))
                elements = "Missing Player";
            if(!levelManager.getFinishPlaced()) {
                elements = elements == "" ? "" : elements+"\n\n";
                elements += "Missing finish flag";
            }
            if(leave.ModifUnsaved) {
                elements = elements == "" ? "" : elements+"\n\n";
                elements += "The level isn't saved";
            }
            missingElements.SetText(elements);
        } else {
            textConfirm.SetText("You are about to test your level "+LevelName+".\nDo you want to continue?");
            windowTestable.SetActive(true);
        }
    }

    public void OnClickGoTest() {
        LoadScenes a = FindObjectOfType<LoadScenes>();
        a.SetFolder("PlayerLevelsEditor");
        a.SetupLevelName(LevelName);
        a.TestingLevel = true;
        a.LoadLevel();
    }
}
