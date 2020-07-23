using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class LoadTest : MonoBehaviour
{
    
    [SerializeField] private LevelManager levelManager = null;
    [SerializeField] private LevelSaveLoad lsl = null;
    [SerializeField] private Leave leave = null;
    [SerializeField] private GameObject confirmOverwrite = null;

    [SerializeField] private GameObject windowTestable = null;
    [SerializeField] private Button buttonTest = null;
    [SerializeField] private TMP_InputField fileName = null;

    [SerializeField] private GameObject windowNotTestable = null;
    [SerializeField] private TMP_Text missingElements = null;

    [SerializeField] private SaveListItem itemSaveList = null;
    [SerializeField] private Transform _content = null;
    private Dictionary<string,SaveListItem> ListFiles = new Dictionary<string, SaveListItem>();

    private void OnEnable() {
        foreach (string path in System.IO.Directory.GetFiles(Application.streamingAssetsPath + "/levels/PlayerLevelsEditor/")){
            string[] tmp = path.Split('/');
            string file = tmp[tmp.Length-1];
            if(!file.EndsWith("meta") && !ListFiles.ContainsKey(file)) {
                SaveListItem listing = Instantiate(itemSaveList, _content);
                listing.SetInfo(file);
                ListFiles[file] = listing;
            }
        }
    }
   

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

    public void CheckInteractible() {
        buttonTest.interactable = fileName.text.Length != 0;
    }

    public void OnClickSave() {
        if(File.Exists(Application.streamingAssetsPath + "/levels/PlayerLevelsEditor/" + fileName.text)) {
            confirmOverwrite.SetActive(true);
        } else {
            lsl.SaveLevel(fileName.text, "PlayerLevelsEditor");
            leave.ModifUnsaved = false;
            levelManager.gameObject.SetActive(true);
            windowTestable.SetActive(false);
            
            LoadScenes a = FindObjectOfType<LoadScenes>();
            a.SetFolder("PlayerLevelsEditor");
            a.SetupLevelName(fileName.text);
            a.TestingLevel = true;
            a.LoadLevel();
            
        }
    }

    public void ConfirmOverwrite() {
        File.Delete(Application.streamingAssetsPath + "/levels/PlayerLevelsEditor/" + fileName.text);
        lsl.SaveLevel(fileName.text, "PlayerLevelsEditor");
        leave.ModifUnsaved = false;
        confirmOverwrite.SetActive(false);
        levelManager.gameObject.SetActive(true);
        windowTestable.SetActive(false);
        LoadScenes a = FindObjectOfType<LoadScenes>();
        a.SetFolder("PlayerLevelsEditor");
        a.SetupLevelName(fileName.text);
        a.TestingLevel = true;
        a.LoadLevel();

        
    }
}
