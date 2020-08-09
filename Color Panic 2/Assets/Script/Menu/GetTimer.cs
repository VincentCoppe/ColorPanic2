using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetTimer : MonoBehaviour
{
    [SerializeField] TMP_Text Name;
    [SerializeField] TMP_Text This;
    private void Start() {
        string time = "";
       // if (ProgressionManagement.times.ContainsKey(Name.text)){
        //    time = ProgressionManagement.times[Name.text];
       // }
        This.text = time;
    }
}
