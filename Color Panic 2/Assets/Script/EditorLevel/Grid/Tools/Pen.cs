using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pen : ToolManager, Tool
{
    [SerializeField] private Image image = null;

    public void ClickIcon() {
        SetTool(this);
    }
    public void SetBgColor(Color color) {
        image.color = color;
    }

    public void Action(GridManager gridManager, TileGameObject ground) {
        (int, int) test = gridManager.PositionToGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        try
        {
            ground.Block.NewBlock(ground).SpawnTiles(test.Item1, test.Item2, gridManager, gridManager.Colors.neutral);
            
        } catch (IndexOutOfRangeException e){}
    }
}
