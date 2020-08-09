using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadListItem : MonoBehaviour
{
    [SerializeField] private TMP_Text _name = null;
    [SerializeField] private Image image = null;

    public void SetInfo(string name) {
        _name.text = name;
    }

    public void OnClickItem() {
        LoadListMenu loadList = Object.FindObjectOfType<LoadListMenu>();
        loadList.SetSelectedFile(_name.text);
    }

    public void SetBgColor(Color color) {
        image.color = color;
    }
}
