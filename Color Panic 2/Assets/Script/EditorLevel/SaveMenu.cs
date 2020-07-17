﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveMenu : MonoBehaviour
{
    [SerializeField] private LevelSaveLoad lsl = null;
    [SerializeField] private TMP_InputField inputField = null;
    [SerializeField] private Button saveButton = null;

    [SerializeField] private SaveListItem itemSaveList = null;
    [SerializeField] private Transform _content = null;
    private Dictionary<string,SaveListItem> ListFiles = new Dictionary<string, SaveListItem>();

    private void OnEnable() {
        foreach (string path in System.IO.Directory.GetFiles(Application.streamingAssetsPath + "/levels/")){
            string[] tmp = path.Split('/');
            string file = tmp[tmp.Length-1];
            if(!file.EndsWith("meta") && !ListFiles.ContainsKey(file)) {
                SaveListItem listing = Instantiate(itemSaveList, _content);
                listing.SetInfo(file);
                ListFiles[file] = listing;
            }
        }
    }
    
    public void CheckInteractible() {
        saveButton.interactable = inputField.text.Length != 0;
    }

    public void OnClickSave() {
        lsl.SaveLevel(inputField);
    }
}
