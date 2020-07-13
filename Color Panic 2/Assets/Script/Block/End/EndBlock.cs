using UnityEngine;
using UnityEditor;

public class EndBlock : BlockBase
{

    public EndBlock(TileGameObject prefab) : base(prefab) { }


    public override void DestroyTiles(int x, int y)
    {
        if (Manager.Grid[x, y] == BlockEnum.End)
        {
            Manager.Grid[x, y] = BlockEnum.Air;
            Manager.GridObject[x, y] = null;
            UnityEngine.Object.Destroy(GameObject);
        }
    }
}