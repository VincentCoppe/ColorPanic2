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

    public void Action(GridManager gridManager, TileGameObject block) {
        (int, int) mouse = gridManager.PositionToGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        try
        {
            BlockBase mouseBlock = gridManager.GridObject[mouse.Item1, mouse.Item2];
            mouseBlock?.DestroyTiles(mouse.Item1, mouse.Item2);
            
        }
        catch (IndexOutOfRangeException){ }
    }
}
