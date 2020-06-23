﻿using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bucket : ToolManager, Tool
{
    [SerializeField] private Image image = null;

    public void ClickIcon() {
        SetTool(this);
    }
    public void SetBgColor(Color color) {
        image.color = color;
    }

    public void Action(GridManager gridManager, TileGameObject block, int size) {
        Stack<(int, int)> stack = new Stack<(int, int)>();
        HashSet<(int, int)> reachable = new HashSet<(int, int)>();

        (int, int) mouse = gridManager.PositionToGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        try
        {
            BlockEnum mouseTypeBlock = gridManager.Grid[mouse.Item1, mouse.Item2];
            if(mouseTypeBlock != block.Block) {

                reachable.Add(mouse);
                stack.Push(mouse);

                while(stack.Count > 0) {
                    (int, int) currentCoords = stack.Pop();
                    BlockBase currentBlock = gridManager.GridObject[currentCoords.Item1, currentCoords.Item2];
                    foreach ((int, int) neighbor in gridManager.Get4Neighbours(currentCoords.Item1, currentCoords.Item2).Where(c => gridManager.Grid[c.Item1, c.Item2] == mouseTypeBlock && !reachable.Contains(c))) {
                        reachable.Add(neighbor);
                        stack.Push(neighbor);
                    }
                }

                foreach((int, int) reached in reachable) {
                    block.Block.NewBlock(block).SpawnTiles(reached.Item1, reached.Item2, gridManager, gridManager.Colors.neutral);
                }
            }

        } catch (IndexOutOfRangeException e){}
    }
}