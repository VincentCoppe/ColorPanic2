using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolsHistory : MonoBehaviour
{
    [SerializeField] private Button undo = null;
    [SerializeField] private Button redo = null;

    public GridManager GridManager;

    // VARIABLES POUR UNDO
    private Stack<HashSet<(int, int)>> undoDraw = new Stack<HashSet<(int, int)>>();
    private Stack<Dictionary<BlockBase, (int,int)>> undoErase = new Stack<Dictionary<BlockBase, (int, int)>>();
    private Stack<bool> undoActions = new Stack<bool>();

    // VARIABLES POUR REDO
    private Stack<Dictionary<BlockBase, (int,int)>> redoDraw = new Stack<Dictionary<BlockBase, (int, int)>>();
    private Stack<HashSet<(int, int)>> redoErase = new Stack<HashSet<(int, int)>>();
    private Stack<bool> redoActions = new Stack<bool>();

    public void SetCurrentGM(GridManager gm) {
        GridManager = gm;
    }

    // METHODES POUR UNDO
    public void AddToUndoDraw(HashSet<(int, int)> action) {
        if(action.Count > 0) {
            undoDraw.Push(action);
            undoActions.Push(true);
            ResetRedo();
            CheckInteractable();
        }
    }

    public void AddToUndoErase(Dictionary<BlockBase, (int,int)> action) {
        if(action.Count > 0) {
            undoErase.Push(action);
            undoActions.Push(false);
            ResetRedo();
            CheckInteractable();
        }
    }

    private void ResetRedo() {
        redoDraw = new Stack<Dictionary<BlockBase, (int, int)>>();
        redoErase = new Stack<HashSet<(int, int)>>();
        redoActions = new Stack<bool>();
    }

    public void ResetHistory() {
        undoDraw = new Stack<HashSet<(int, int)>>();
        undoErase = new Stack<Dictionary<BlockBase, (int, int)>>();
        undoActions = new Stack<bool>();
        ResetRedo();
        CheckInteractable();
    }

    private void CheckInteractable() {
        undo.interactable = undoActions.Count != 0;
        redo.interactable = redoActions.Count != 0;
    }

    public void UndoAction() {
        if(GridManager == null) {
            GridManager = Object.FindObjectOfType<LevelManager>().CurrentGM;
        }
        // Si la dernière action est du dessin, alors on efface
        if(undoActions.Pop()) {
            HashSet<(int, int)> blocksDrawn = undoDraw.Pop();
            Dictionary<BlockBase, (int,int)> blocksToErase = new Dictionary<BlockBase, (int, int)>();
            foreach ((int, int) blockToErase in blocksDrawn)
            {
                BlockBase blockErase = GridManager.GridObject[blockToErase.Item1, blockToErase.Item2];
                blocksToErase[blockErase] = blockToErase;
                blockErase?.DestroyTiles(blockToErase.Item1, blockToErase.Item2);
            }
            redoDraw.Push(blocksToErase);
            redoActions.Push(true);
        } else {
            // Sinon on redessine ce qui a été effacé
            Dictionary<BlockBase, (int,int)> blocksErased = undoErase.Pop();
            HashSet<(int, int)> blocksToDraw = new HashSet<(int, int)>();
            foreach (BlockBase block in blocksErased.Keys)
            {
                (int, int) coords = blocksErased[block];
                block.Block.NewBlock(block.Prefab).SpawnTiles(coords.Item1, coords.Item2, GridManager, GridManager.Colors.neutral);
                blocksToDraw.Add(coords);
            }
            redoErase.Push(blocksToDraw);
            redoActions.Push(false);
        }
        CheckInteractable();
    }
    
    public void RedoAction() {
        // Si la premiere action a refaire est du dessin suite a un effacage
        if(redoActions.Pop()) {
            Dictionary<BlockBase, (int,int)> blocksErased = redoDraw.Pop();
            HashSet<(int, int)> blocksToDraw = new HashSet<(int, int)>();
            foreach (BlockBase block in blocksErased.Keys)
            {
                (int, int) coords = blocksErased[block];
                block.Block.NewBlock(block.Prefab).SpawnTiles(coords.Item1, coords.Item2, GridManager, GridManager.Colors.neutral);
                blocksToDraw.Add(coords);
            }
            undoDraw.Push(blocksToDraw);
            undoActions.Push(true);
        } else {
            // Sinon on efface ce qui a été redessiné
            HashSet<(int, int)> blocksDrawn = redoErase.Pop();
            Dictionary<BlockBase, (int,int)> blocksToErase = new Dictionary<BlockBase, (int, int)>();
            foreach ((int, int) blockToErase in blocksDrawn)
            {
                BlockBase blockErase = GridManager.GridObject[blockToErase.Item1, blockToErase.Item2];
                blocksToErase[blockErase] = blockToErase;
                blockErase?.DestroyTiles(blockToErase.Item1, blockToErase.Item2);
            }
            undoErase.Push(blocksToErase);
            undoActions.Push(false);
        }
        CheckInteractable();
    }
}
