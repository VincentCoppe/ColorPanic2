using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverBlock : MonoBehaviour
{
    Dictionary<(int, int), GameObject> blocksHovered = new Dictionary<(int, int), GameObject>();

    public void DisplayCells(GridManager Manager, Dictionary<(int, int), GameObject> blocks) {
        foreach((int, int) coord in blocks.Keys) {
            try {
                if((!blocksHovered.ContainsKey(coord)) && (blocks[coord].name == "Erase" || Manager.GridObject[coord.Item1, coord.Item2] == null)) {
                    GameObject go = Manager.Instantiate(blocks[coord]);
                    go.transform.localPosition = Manager.GridToPosition(coord.Item1, coord.Item2) + new Vector3(0.5f, 0.5f, 0);
                    blocksHovered[coord] = go;
                } else if ((blocksHovered.ContainsKey(coord) && blocksHovered[coord] != blocks[coord])) {
                    UnityEngine.Object.Destroy(blocksHovered[coord]);
                    GameObject go = Manager.Instantiate(blocks[coord]);
                    go.transform.localPosition = Manager.GridToPosition(coord.Item1, coord.Item2) + new Vector3(0.5f, 0.5f, 0);
                    blocksHovered[coord] = go;
                }
            } catch (IndexOutOfRangeException) {}
        }
        List<(int, int)> toRemove = new List<(int, int)>();
        foreach((int, int) coord in blocksHovered.Keys) {
            if(!blocks.ContainsKey(coord)) {
                UnityEngine.Object.Destroy(blocksHovered[coord]);
                toRemove.Add(coord);
            }
        }
        foreach((int, int) coord in toRemove) {
            blocksHovered.Remove(coord);
        }
    }

    public void CleanCells() {
        foreach(GameObject go in blocksHovered.Values) {
            UnityEngine.Object.Destroy(go);
        }
        blocksHovered.Clear();    
    }
}
