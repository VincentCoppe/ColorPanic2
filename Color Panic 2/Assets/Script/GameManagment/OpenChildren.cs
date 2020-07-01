using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChildren : MonoBehaviour
{
    public void DisplayChild()
    {
        this.transform.GetChild(0).gameObject.SetActive(true);
    }
}
