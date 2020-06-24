using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reset : MonoBehaviour
{
    [SerializeField] private GridManager gridManager = null;
    public void Action() {
        for(int y=0; y<gridManager.GridObject.GetLength(1); y++)
        {
            for(int x=0; x<gridManager.GridObject.GetLength(0); x++) {
                BlockBase blockToErase = gridManager.GridObject[x, y];
                blockToErase?.DestroyTiles(x, y);
            }
        }        
    }
}
