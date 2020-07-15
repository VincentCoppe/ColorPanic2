using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadListMenu : MonoBehaviour
{
    [SerializeField] private LoadListItem _loadListItem = null;
    [SerializeField] private Transform _content = null;
    [SerializeField] private Button _loadButton = null;
    [SerializeField] private LevelSaveLoad _levelSaveLoad = null;


    private string selectedFile = null;
    private Dictionary<string,LoadListItem> ListFiles = new Dictionary<string, LoadListItem>();

    private void OnEnable() {
        foreach(LoadListItem item in ListFiles.Values) {
            item.SetBgColor(new Color(1,1,1));
        }
        selectedFile = null;
        foreach (string path in System.IO.Directory.GetFiles(Application.streamingAssetsPath + "/levels/")){
            string[] tmp = path.Split('/');
            string file = tmp[tmp.Length-1];
            if(!file.EndsWith("meta") && !ListFiles.ContainsKey(file)) {
                LoadListItem listing = Instantiate(_loadListItem, _content);
                listing.SetInfo(file);
                ListFiles[file] = listing;
            }
        }
    }

    public void SetSelectedFile(string name) {
        if(selectedFile != null) {
            ListFiles[selectedFile].SetBgColor(new Color(1,1,1));
        }
        selectedFile = name;
        ListFiles[selectedFile].SetBgColor(new Color(65/255f, 200/255f, 65/255f));
        _loadButton.interactable = true;
    }

    public void LoadFile() {
        _levelSaveLoad.LoadLevel(selectedFile);
    }
}
