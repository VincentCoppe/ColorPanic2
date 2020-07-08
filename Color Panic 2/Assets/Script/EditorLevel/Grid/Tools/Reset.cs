using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reset : MonoBehaviour
{
    [SerializeField] private ToolsHistory toolsHistory = null;

    private Dictionary<BlockBase, (int,int)> blocksErased = new Dictionary<BlockBase, (int, int)>();
    public void Action() {
        GridManager gridManager  = toolsHistory.GridManager;
        for(int y=0; y<gridManager.GridObject.GetLength(1); y++)
        {
            for(int x=0; x<gridManager.GridObject.GetLength(0); x++) {
                BlockBase blockToErase = gridManager.GridObject[x, y];
                if(blockToErase != null) {
                    blocksErased[blockToErase] = (x, y);
                    blockToErase.DestroyTiles(x, y);
                }
            }
        }  
        toolsHistory.AddToUndoErase(blocksErased);      
    }
}
