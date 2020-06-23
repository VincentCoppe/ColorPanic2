using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Erase : ToolManager, Tool
{
    [SerializeField] private Image image = null;

    public void ClickIcon() {
        SetTool(this);
    }

    public void SetBgColor(Color color) {
        image.color = color;
    }

    public void Action(GridManager gridManager, TileGameObject block, int size) {
        HashSet<(int, int)> blocksToErase = new HashSet<(int, int)>();
        (int, int) mouse = gridManager.PositionToGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        try
        {
            blocksToErase.Add(mouse);
            size--;
            while(size > 0) {
                HashSet<(int, int)> tmp = new HashSet<(int, int)>(blocksToErase);
                foreach ((int, int) blockToErase in tmp) {
                    foreach ((int, int) neighbor in gridManager.Get8Neighbours(blockToErase.Item1, blockToErase.Item2)) {
                        blocksToErase.Add(neighbor);
                    }
                }
                size--;
            }

            foreach ((int, int) blockToErase in blocksToErase) {
                BlockBase blockErase = gridManager.GridObject[blockToErase.Item1, blockToErase.Item2];
                blockErase?.DestroyTiles(blockToErase.Item1, blockToErase.Item2);
            }
            
            
        } catch (IndexOutOfRangeException e){}
    }
}
