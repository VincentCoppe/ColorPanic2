using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pen : ToolManager, Tool
{
    [SerializeField] private Image image = null;
    [SerializeField] private ToolsHistory toolsHistory = null;
    private HashSet<(int,int)> blocksPrinted = new HashSet<(int, int)>();

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

    public void Action(GridManager gridManager, TileGameObject block, int size, (int,int) mouse) {
        HashSet<(int, int)> blocksToPrint = new HashSet<(int, int)>();
        
        if(Input.GetMouseButtonDown(0)) {
            blocksPrinted = new HashSet<(int, int)>();
        } else if (Input.GetMouseButtonUp(0)) {
            toolsHistory.AddToUndoDraw(blocksPrinted);
        }
        
        blocksToPrint.Add(mouse);
        size--;
        while(size > 0) {
            HashSet<(int, int)> tmp = new HashSet<(int, int)>(blocksToPrint);
            foreach ((int, int) blockToPrint in tmp) {
                foreach ((int, int) neighbor in gridManager.Get8Neighbours(blockToPrint.Item1, blockToPrint.Item2)) {
                    blocksToPrint.Add(neighbor);
                }
            }
            size--;
        }

        foreach ((int, int) blockToPrint in blocksToPrint) {
            if(block.Block.NewBlock(block).SpawnTiles(blockToPrint.Item1, blockToPrint.Item2, gridManager, gridManager.Colors.neutral)) {
                blocksPrinted.Add(blockToPrint);
            }
        }
        
    }
}
