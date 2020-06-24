using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContourRect : ToolManager, Tool
{
    [SerializeField] private Image image = null;
    private HashSet<(int, int)> rect = new HashSet<(int, int)>();
    private (int, int) start = (99999, 99999);

    public void ClickIcon() {
        SetTool(this);
    }
    public void SetBgColor(Color color) {
        image.color = color;
    }

    public void Action(GridManager gridManager, TileGameObject block, int size) {
        try
        {
            if(Input.GetMouseButtonDown(0)) {
                start = gridManager.PositionToGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            } else if (Input.GetMouseButtonUp(0)) {
                foreach ((int, int) blockToPrint in rect)
                {
                    block.Block.NewBlock(block).SpawnTiles(blockToPrint.Item1, blockToPrint.Item2, gridManager, gridManager.Colors.neutral);
                }
            } else if (start != (99999, 99999)) {
                rect = new HashSet<(int, int)>();
                rect.Add(start);
                (int, int) mouse = gridManager.PositionToGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                while(mouse.Item1 > start.Item1) {
                    rect.Add((mouse.Item1, start.Item2));
                    rect.Add((mouse.Item1, mouse.Item2));
                    mouse.Item1--;
                }
                while(mouse.Item1 < start.Item1) {
                    rect.Add((mouse.Item1, start.Item2));
                    rect.Add((mouse.Item1, mouse.Item2));
                    mouse.Item1++;
                }
                mouse = gridManager.PositionToGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                while(mouse.Item2 > start.Item2) {
                    rect.Add((start.Item1, mouse.Item2));
                    rect.Add((mouse.Item1, mouse.Item2));
                    mouse.Item2--;
                }
                while(mouse.Item2 < start.Item2) {
                    rect.Add((start.Item1, mouse.Item2));
                    rect.Add((mouse.Item1, mouse.Item2));
                    mouse.Item2++;
                }
            }

        } catch (IndexOutOfRangeException){
            rect = new HashSet<(int, int)>();
            start = (99999, 99999);
        }
    }
}
