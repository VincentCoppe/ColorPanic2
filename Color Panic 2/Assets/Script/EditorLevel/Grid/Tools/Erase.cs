﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Erase : ToolManager, Tool
{
    [SerializeField] private Image image = null;
    [SerializeField] private ToolsHistory toolsHistory = null;
    private Dictionary<BlockBase, (int,int)> blocksErased = new Dictionary<BlockBase, (int, int)>();

    public void ClickIcon() {
        SetTool(this);
    }

    public void SetBgColor(Color color) {
        image.color = color;
    }

    public void Action(GridManager gridManager, TileGameObject block, int size, (int,int) mouse) {
        if(Input.GetMouseButtonDown(0)) {
            blocksErased = new Dictionary<BlockBase, (int, int)>();
        } else if (Input.GetMouseButtonUp(0)) {
            toolsHistory.AddToUndoErase(blocksErased);
        }
        foreach ((int, int) blockToErase in ComputeBlocksToErase(gridManager, size, mouse)) {
            BlockBase blockErase = gridManager.GridObject[blockToErase.Item1, blockToErase.Item2];
            if(blockErase != null) {
                blocksErased[blockErase] = blockToErase;
                blockErase.DestroyTiles(blockToErase.Item1, blockToErase.Item2);
            }
        }
    }

    private HashSet<(int, int)> ComputeBlocksToErase(GridManager gridManager, int size, (int,int) mouse) {
        HashSet<(int, int)> blocksToErase = new HashSet<(int, int)>();
        blocksToErase.Add(mouse);
        size--;
        while(size > 0) {
            HashSet<(int, int)> tmp = new HashSet<(int, int)>(blocksToErase);
            foreach ((int, int) blockToPrint in tmp) {
                foreach ((int, int) neighbor in gridManager.Get8Neighbours(blockToPrint.Item1, blockToPrint.Item2)) {
                    blocksToErase.Add(neighbor);
                }
            }
            size--;
        }
        return blocksToErase;
    }

    public void EndAction() {
        toolsHistory.AddToUndoErase(blocksErased);
    }
    public HashSet<(int,int)> GetBlocksToHover(GridManager gridManager, int size, (int,int) mouse) {
        return ComputeBlocksToErase(gridManager, size, mouse);
    }
}
