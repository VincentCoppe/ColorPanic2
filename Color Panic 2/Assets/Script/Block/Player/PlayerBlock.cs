﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlock : BlockBase
{

    private CBD_Player _data;
    public PlayerBlock(TileGameObject prefab) : base(prefab) { }

    protected override bool Spawn(int x, int y)
    {
        if (base.Spawn(x, y))
        {
            var data = Data<CBD_Player>();
            data.setLevelManager();
            if (data.LevelManager.getPlayerPlaced())
            {
                Manager.Grid[x, y] = BlockEnum.Air;
                Manager.GridObject[x, y] = null;
                UnityEngine.Object.Destroy(GameObject);
                return false;
            }
            data.LevelManager.setPlayerPlaced();
            return true;
        }
        return false;
    }

    public override void DestroyTiles(int x, int y)
    {
        if (Manager.Grid[x, y] == BlockEnum.Player)
        {
            var data = Data<CBD_Player>();
            data.setLevelManager();
            data.LevelManager.setPlayerPlaced();
            Manager.Grid[x, y] = BlockEnum.Air;
            Manager.GridObject[x, y] = null;
            UnityEngine.Object.Destroy(GameObject);
        }
    }
}
