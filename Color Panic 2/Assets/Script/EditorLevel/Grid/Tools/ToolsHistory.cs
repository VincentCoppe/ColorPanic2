using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolsHistory : MonoBehaviour
{
    [SerializeField] private GridManager gridManager = null;
    [SerializeField] private Button undo = null;
    [SerializeField] private Button redo = null;

    private Stack<HashSet<(int, int)>> historyDraw = new Stack<HashSet<(int, int)>>();
    private Stack<bool> historyErase = new Stack<bool>();

    public void AddToHistoryDraw(HashSet<(int, int)> action) {
        historyDraw.Push(action);
        historyErase.Push(false);
        CheckInteractable();
    }
    private void Update() {
        Debug.Log(historyErase.Count);
    }

    private void CheckInteractable() {
        undo.interactable = historyErase.Count != 0;
        //redo.interactable = history.Count != 0;
    }

    public void UndoAction() {
        // Si la dernière action est du dessin, alors on efface
        if(!historyErase.Pop()) {
            HashSet<(int, int)> lastAction = historyDraw.Pop();
            foreach ((int, int) blockToErase in lastAction)
            {
                BlockBase blockErase = gridManager.GridObject[blockToErase.Item1, blockToErase.Item2];
                blockErase?.DestroyTiles(blockToErase.Item1, blockToErase.Item2);
            }
        }

        CheckInteractable();

    }
    
    public void RedoAction() {

    }
}
