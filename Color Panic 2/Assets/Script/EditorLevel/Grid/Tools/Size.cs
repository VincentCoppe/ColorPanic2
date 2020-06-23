using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Size : MonoBehaviour
{
    [SerializeField] private Image little = null;
    [SerializeField] private Image medium = null;
    [SerializeField] private Image large = null;
    public int size;

    // Petite taille par défault
    private void Start() {
        LittleSize();
    }
    
    public void LittleSize() {
        size = 1;
        little.color = new Color(0,1,0);
        medium.color = new Color(1,1,1);
        large.color = new Color(1,1,1);
    }

    public void MediumSize() {
        size = 2;
        little.color = new Color(1,1,1);
        medium.color = new Color(0,1,0);
        large.color = new Color(1,1,1);
    }

    public void LargeSize() {
        size = 3;
        little.color = new Color(1,1,1);
        medium.color = new Color(1,1,1);
        large.color = new Color(0,1,0);
    }
}
