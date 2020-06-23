using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pen : ToolManager, Tool
{
    [SerializeField] private Image image = null;

    // Pen sélectionné par défaut
    private void Start() {
        ClickIcon();
    }

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
            block.Block.NewBlock(block).SpawnTiles(mouse.Item1, mouse.Item2, gridManager, gridManager.Colors.neutral);
            
        } catch (IndexOutOfRangeException e){}
    }
}
