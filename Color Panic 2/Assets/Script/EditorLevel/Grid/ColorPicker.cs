using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPicker : MonoBehaviour
{

    public Color[] Viridian = null;
    public Color[] Blue = null;
    public Color[] Red = null;
    public Color[] Yellow = null;
    public Color[] Green = null;
    public Color[] Purple = null;
    public Color[] Pink = null;
    public Color[] neutral = null;
    private static ColorPicker _instance;
    public static ColorPicker Instance { get { return _instance; } }

    public void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);

        }
        else
        {
            _instance = this;
            return;

        }
    }
}
