using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverBlock : MonoBehaviour
{
    Dictionary<(int, int), GameObject> blocksHovered = new Dictionary<(int, int), GameObject>();

    public void DisplayCells(GridManager Manager, GameObject _prefab, HashSet<(int, int)> coords) {
        foreach((int, int) coord in coords) {
            if(!blocksHovered.ContainsKey(coord) && (_prefab.name == "Erase" || Manager.GridObject[coord.Item1, coord.Item2] == null)) {
                GameObject go = Manager.Instantiate(_prefab);
                go.transform.localPosition = Manager.GridToPosition(coord.Item1, coord.Item2) + new Vector3(0.5f, 0.5f, 0);
                blocksHovered[coord] = go;
            }
        }
        List<(int, int)> toRemove = new List<(int, int)>();
        foreach((int, int) coord in blocksHovered.Keys) {
            if(!coords.Contains(coord)) {
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
