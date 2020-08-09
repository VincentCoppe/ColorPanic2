using UnityEngine;
using UnityEditor;

public class EndBlock : BlockBase
{
    private CBD_End _data;

    public EndBlock(TileGameObject prefab) : base(prefab) { }

    protected override bool Spawn(int x, int y)
    {
        if (base.Spawn(x, y))
        {
            var data = Data<CBD_End>();
            data.setLevelManager();
            if(data.LevelManager.getFinishPlaced())
            {
                Manager.Grid[x, y] = BlockEnum.Air;
                Manager.GridObject[x, y] = null;
                UnityEngine.Object.Destroy(GameObject);
                return false;
            }
            data.LevelManager.setFinishPlaced();
            return true;
        }
        return false;
    }

    public override void DestroyTiles(int x, int y)
    {
        if (Manager.Grid[x, y] == BlockEnum.End)
        {
            var data = Data<CBD_End>();
            data.setLevelManager();
            data.LevelManager.setFinishPlaced();
            Manager.Grid[x, y] = BlockEnum.Air;
            Manager.GridObject[x, y] = null;
            UnityEngine.Object.Destroy(GameObject);
        }
    }
}