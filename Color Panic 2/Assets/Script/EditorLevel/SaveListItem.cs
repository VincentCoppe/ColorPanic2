using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveListItem : MonoBehaviour
{
    [SerializeField] private TMP_Text _name = null;

    public void SetInfo(string name) {
        _name.text = name;
    }
}
