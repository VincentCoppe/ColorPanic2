using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITool
{
    void ClickIcon();
    void SetBgColor(Color color);
    void Action(GridManager gridManager, TileGameObject block, int size, (int,int) mouse);
    void EndAction();
    Dictionary<(int, int), GameObject> GetBlocksToHover(GridManager gridManager, TileGameObject block, int size, (int,int) mouse);
}
