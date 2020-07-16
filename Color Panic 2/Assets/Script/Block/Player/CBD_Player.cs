using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBD_Player : CustomBlockData
{
    

    public LevelManager LevelManager = null;

    public void setLevelManager()
    {
        LevelManager = FindObjectOfType<LevelManager>(true);
    }
}
