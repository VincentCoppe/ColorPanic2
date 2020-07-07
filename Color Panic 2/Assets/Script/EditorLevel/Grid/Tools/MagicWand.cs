using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicWand : ToolManager, ITool
{
    [SerializeField] private Image image = null;
    [SerializeField] private ToolsHistory toolsHistory = null;
    [SerializeField] private LineRenderer lineRenderer = null;

    private Dictionary<(int, int), BlockBase> selectedBlocks = new Dictionary<(int, int), BlockBase>();
    private (int, int)[] rect = new (int, int)[2];

    public void ClickIcon() {
        SetTool(this);
        selectedBlocks = new Dictionary<(int, int), BlockBase>();
        rect = new (int, int)[2];
    }
    public void SetBgColor(Color color) {
        image.color = color;
    }

    public void Action(GridManager gridManager, TileGameObject block, int size, (int,int) mouse) {
        if(Input.GetMouseButtonDown(1)) {
            selectedBlocks = new Dictionary<(int, int), BlockBase>();
            rect = new (int, int)[2];     
        }
        if(Input.GetMouseButtonDown(0) && selectedBlocks.Count == 0) {
            Stack<(int, int)> stack = new Stack<(int, int)>();
            Dictionary<(int, int), BlockBase> reachable = new Dictionary<(int, int), BlockBase>();
            if(gridManager.Grid[mouse.Item1, mouse.Item2] != BlockEnum.Air) {                
                reachable[mouse] = gridManager.GridObject[mouse.Item1, mouse.Item2];
                stack.Push(mouse);

                //List<Vector3> vectors = new List<Vector3>();
                while(stack.Count > 0) {
                    (int, int) currentCoords = stack.Pop();
                    BlockBase currentBlock = gridManager.GridObject[currentCoords.Item1, currentCoords.Item2];
                    foreach ((int, int) neighbor in gridManager.Get4Neighbours(currentCoords.Item1, currentCoords.Item2).Where(c => !reachable.ContainsKey(c))) {
                        if(gridManager.Grid[neighbor.Item1, neighbor.Item2] != BlockEnum.Air) {
                            reachable[neighbor] = gridManager.GridObject[neighbor.Item1, neighbor.Item2];
                            stack.Push(neighbor);
                        } else {
                            //vectors.Add(gridManager.GridToPosition(currentCoords.Item1, currentCoords.Item2));
                            //vectors.Add(gridManager.GridToPosition(neighbor.Item1, neighbor.Item2));
                        }
                    }
                }
                //lineRenderer.positionCount = vectors.Count;
                //lineRenderer.SetPositions(vectors.ToArray());
                int minX = 9999999;
                int minY = 9999999;
                int maxX = -1;
                int maxY = -1;
                foreach((int, int) reached in reachable.Keys) {
                    if(reached.Item1 < minX) minX = reached.Item1;
                    if(reached.Item2 < minY) minY = reached.Item2;
                    if(reached.Item1 > maxX) maxX = reached.Item1;
                    if(reached.Item2 > maxY) maxY = reached.Item2;
                }
                rect = new (int, int)[]{(minX, minY), (maxX, maxY)};
                (int, int) mid = ((maxX-minX)/2, (maxY-minY)/2);
                foreach((int, int) reached in reachable.Keys) {
                    selectedBlocks[(reached.Item1+mid.Item1-maxX, reached.Item2+mid.Item2-maxY)] = reachable[reached];
                }                
            }          
        }
        else if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.V) && selectedBlocks.Count != 0) {
            HashSet<(int, int)> res = new HashSet<(int, int)>();
            foreach((int, int) reached in selectedBlocks.Keys) {
                try {
                    if(selectedBlocks[reached].Block.NewBlock(selectedBlocks[reached].Prefab).SpawnTiles(mouse.Item1+reached.Item1, mouse.Item2+reached.Item2, gridManager, gridManager.Colors.neutral)) {
                        res.Add((reached.Item1+mouse.Item1, reached.Item2+mouse.Item2));
                    }
                } catch (IndexOutOfRangeException) {}
            }
            toolsHistory.AddToUndoDraw(res); 
        }
        
    }

    public void EndAction(){}
    
    public Dictionary<(int, int), GameObject> GetBlocksToHover(GridManager gridManager, TileGameObject block, int size, (int,int) mouse) {
        Dictionary<(int, int), GameObject> res = new Dictionary<(int, int), GameObject>();
        foreach ((int, int) blockToPrint in selectedBlocks.Keys) {
            res[(mouse.Item1+blockToPrint.Item1, mouse.Item2+blockToPrint.Item2)] = selectedBlocks[blockToPrint].Prefab.gameObject;
        }
        return res;
    }
}
