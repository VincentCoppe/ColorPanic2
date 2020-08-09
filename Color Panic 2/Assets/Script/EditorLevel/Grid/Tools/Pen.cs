using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pen : ToolManager, ITool
{
    [SerializeField] private Image image = null;
    [SerializeField] private ToolsHistory toolsHistory = null;
    [SerializeField] private Sprite iconCursor = null;
    private HashSet<(int,int)> blocksPrinted = new HashSet<(int, int)>();

    public Sprite GetCursor() {
        return iconCursor;
    }

    public void ClickIcon() {
        SetTool(this);
    }
    public void SetBgColor(Color color) {
        image.color = color;
    }

    public void Action(GridManager gridManager, TileGameObject block, int size, (int,int) mouse) {
        if(Input.GetMouseButtonDown(0)) {
            blocksPrinted = new HashSet<(int, int)>();
        } else if (Input.GetMouseButtonUp(0)) {
            toolsHistory.AddToUndoDraw(blocksPrinted);
        } else if(Input.GetMouseButton(0)) {
            foreach ((int, int) blockToPrint in ComputeBlocksToPrint(gridManager, size, mouse)) {
                if(block.Block.NewBlock(block).SpawnTiles(blockToPrint.Item1, blockToPrint.Item2, gridManager, gridManager.Colors.neutral)) {
                    blocksPrinted.Add(blockToPrint);
                }
            }
        }
    }

    private HashSet<(int, int)> ComputeBlocksToPrint(GridManager gridManager, int size, (int,int) mouse) {
        HashSet<(int, int)> blocksToPrint = new HashSet<(int, int)>();
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
        return blocksToPrint;
    }

    public void EndAction() {
        toolsHistory.AddToUndoDraw(blocksPrinted);
    }

    public Dictionary<(int, int), GameObject> GetBlocksToHover(GridManager gridManager, TileGameObject block, int size, (int,int) mouse) {
        Dictionary<(int, int), GameObject> res = new Dictionary<(int, int), GameObject>();
        foreach ((int, int) blockToPrint in ComputeBlocksToPrint(gridManager, size, mouse)) {
            res[blockToPrint] = block.gameObject;
        }
        return res;
    }
}
