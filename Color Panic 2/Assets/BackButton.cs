﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    [SerializeField] LoaderMenu LoaderMenu;
    public void OnMouseDown()
    {
        LoadScenes.Instance.LoadMenu();
    }
}
