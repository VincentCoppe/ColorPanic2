using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveMenu : MonoBehaviour
{
    [SerializeField] private LevelSaveLoad lsl = null;
    [SerializeField] private TMP_InputField inputField = null;
    [SerializeField] private Button saveButton = null;
    [SerializeField] private GameObject confirmOverwrite = null;
    [SerializeField] private GameObject save = null;
    [SerializeField] private LevelManager levelManager = null;
    [SerializeField] private Leave leave = null;

    [SerializeField] private SaveListItem itemSaveList = null;
    [SerializeField] private Transform _content = null;
    private Dictionary<string, SaveListItem> ListFiles = new Dictionary<string, SaveListItem>();

    private void OnEnable()
    {
        foreach (string path in System.IO.Directory.GetFiles(Application.streamingAssetsPath + "/levels/PlayerLevelsEditor/"))
        {
            string[] tmp = path.Split('/');
            string file = tmp[tmp.Length - 1];
            if (!file.EndsWith("meta") && !ListFiles.ContainsKey(file))
            {
                SaveListItem listing = Instantiate(itemSaveList, _content);
                listing.SetInfo(file);
                ListFiles[file] = listing;
            }
        }
    }

    public void CheckInteractible()
    {
        saveButton.interactable = inputField.text.Length != 0;
    }

    public void OnClickSave()
    {
        if (File.Exists(Application.streamingAssetsPath + "/levels/PlayerLevelsEditor/" + inputField.text))
        {
            confirmOverwrite.SetActive(true);
        }
        else
        {
            lsl.SaveLevel(inputField.text, "PlayersLevelsEditor");
            leave.ModifUnsaved = false;
            levelManager.gameObject.SetActive(true);
            save.SetActive(false);
        }
    }

    public void ConfirmOverwrite()
    {
        File.Delete(Application.streamingAssetsPath + "/levels/PlayerLevelsEditor/" + inputField.text);
        lsl.SaveLevel(inputField.text, "PlayersLevelsEditor");
        leave.ModifUnsaved = false;
    }
}
