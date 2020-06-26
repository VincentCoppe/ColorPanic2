using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class testImage : MonoBehaviour
{
    
  


    public void CreateAssetFromGrid(GameObject levelDrawer)
    {
        PrefabUtility.SaveAsPrefabAsset(levelDrawer, Application.dataPath + "/Resources/levels/leveltest.prefab");
   }

}
