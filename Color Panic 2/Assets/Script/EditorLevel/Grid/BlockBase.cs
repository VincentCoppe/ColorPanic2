using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BlockBase
{


    public abstract void Serialize();

    public abstract void Deserialize();

    public abstract void SpawnTiles(GridManager manager,int x, int y,GameObject toSpawn);

    public abstract void DestroyTiles(int x, int y, GridManager manager);
}
