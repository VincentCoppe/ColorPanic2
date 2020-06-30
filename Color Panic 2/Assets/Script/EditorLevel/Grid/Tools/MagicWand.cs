using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicWand : ToolManager, Tool
{
    [SerializeField] private Image image = null;
    [SerializeField] private ToolsHistory toolsHistory = null;

    private HashSet<(int, int)> selectedBlocks = new HashSet<(int, int)>();
    private BlockBase typeSelectedBlock = null;

    public void ClickIcon() {
        SetTool(this);
    }
    public void SetBgColor(Color color) {
        image.color = color;
    }

    public void Action(GridManager gridManager, TileGameObject block, int size, (int,int) mouse) {
        if(Input.GetMouseButtonDown(0) && typeSelectedBlock != null) {
            HashSet<(int, int)> res = new HashSet<(int, int)>();
            foreach((int, int) reached in selectedBlocks) {
                try {
                    if(typeSelectedBlock.Block.NewBlock(typeSelectedBlock.Prefab).SpawnTiles(mouse.Item1+reached.Item1, mouse.Item2+reached.Item2, gridManager, gridManager.Colors.neutral)) {
                        res.Add(reached);
                    }
                } catch (IndexOutOfRangeException) {}
            }
            toolsHistory.AddToUndoDraw(res); 
        }
        else if(Input.GetMouseButtonDown(0)) {
            Stack<(int, int)> stack = new Stack<(int, int)>();
            HashSet<(int, int)> reachable = new HashSet<(int, int)>();        
            BlockEnum mouseTypeBlock = gridManager.Grid[mouse.Item1, mouse.Item2];
            if(mouseTypeBlock != BlockEnum.Air) {
                typeSelectedBlock = gridManager.GridObject[mouse.Item1, mouse.Item2];
                
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
                int minX = 9999999;
                int minY = 9999999;
                int maxX = -1;
                int maxY = -1;
                foreach((int, int) reached in reachable) {
                    if(reached.Item1 < minX) minX = reached.Item1;
                    if(reached.Item2 < minY) minY = reached.Item2;
                    if(reached.Item1 > maxX) maxX = reached.Item1;
                    if(reached.Item2 > maxY) maxY = reached.Item2;
                }
                (int, int) mid = ((maxX-minX)/2, (maxY-minY)/2);
                foreach((int, int) reached in reachable) {
                    selectedBlocks.Add((maxX-reached.Item1-mid.Item1, maxY-reached.Item2-mid.Item2));
                }                
            }          
        }
        if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.C)) {
        }
        if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.V)) {
        }
        
    }

    public void EndAction(){}
    public HashSet<(int,int)> GetBlocksToHover(GridManager gridManager, int size, (int,int) mouse) {
        HashSet<(int, int)> res = new HashSet<(int, int)>();
        foreach((int, int) reached in selectedBlocks) {
            res.Add((mouse.Item1+reached.Item1, mouse.Item2+reached.Item2));
        }
        return res;
    }
}
