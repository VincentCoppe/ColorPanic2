using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Tool
{
    void ClickIcon();
    void SetBgColor(Color color);
    void Action(GridManager gridManager, TileGameObject block, int size);
}
