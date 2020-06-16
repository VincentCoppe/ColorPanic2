using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BlockGround : BlockBase
{

    [System.Serializable]
    public struct Setup
    {
        public Vector2Int boardSize;
        public Vector3 noiseOffset;
        public float noiseScale;
       // public GameModes gameMode;
    }


    [SerializeField] private SpriteRenderer[] Corner = new SpriteRenderer[8];
    GridManager Manager = null;
    private Dictionary<int, int[]> Placement = new Dictionary<int, int[]>(){
        {0,new int[]{0,1,3}},
        {1,new int[]{1}},
        {2,new int[]{1,2,4}},
        {3,new int[]{3}},
        {4,new int[]{4}},
        {5,new int[]{3,5,6}},
        {6,new int[]{6}},
        {7,new int[]{4,6,7}},
    };


    public override void Deserialize()
    {
        throw new System.NotImplementedException();
    }

    public override void DestroyTiles(int x, int y, GridManager manager)
    {

        Manager = manager;
        if (Manager.Grid[x, y] == (BlockEnum)1)
        {
            Manager.Grid[x, y] = BlockEnum.Air;
            CalculateNeighbours(x, y, true);
            Manager.Destroy(x, y);
        }
    }

    public override void Serialize()
    {
        throw new System.NotImplementedException();
    }

    public override void SpawnTiles(GridManager manager, int x, int y,GameObject toSpawn)
    {
        Manager = manager;
        if (Manager.Grid[x, y] == 0)
        {
            Manager.Grid[x, y] = BlockEnum.Ground;
            GameObject ground = Manager.Instantiate(toSpawn);
            Manager.GridObject[x, y] = ground;
            CalculateNeighbours(x, y, true);
            ground.transform.localPosition = Manager.GridToPosition(x, y) + new Vector3(0.5f, 0.5f, 0);
        }
    }

    public void CalculateNeighbours(int x, int y, bool first)
    {
        (int, int)[] Neighbours = GetNeighbours(x, y);

        List<BlockEnum> BeNeighbours = new List<BlockEnum>();
        foreach ((int, int) Neighbour in Neighbours)
        {
            if (first && Manager.Grid[Neighbour.Item1, Neighbour.Item2] == (BlockEnum)1) CalculateNeighbours(Neighbour.Item1, Neighbour.Item2, false);

            BeNeighbours.Add(Manager.Grid[Neighbour.Item1, Neighbour.Item2]);

        }
        BlockGround block;
        if (Manager.Grid[x, y] == (BlockEnum)1)
        {
            TileGameObject a = Manager.GridObject[x, y].GetComponent<TileGameObject>();
            a.SetBlock();
            block = (BlockGround)a.Block;
            block.CalculateEdge(BeNeighbours);
        }

    }

    public void CalculateEdge(List<BlockEnum> Neighbours)
    {
        for (int i = 0; i < Corner.Length; i++)
        {
            bool a = false;

            foreach (int n in Placement[i])
            {
                if (Neighbours[n] != (BlockEnum)1) a = true;
            }
            Corner[i].gameObject.SetActive(a);
        }
    }

    private (int, int)[] GetNeighbours(int x, int y)
    {
        Dictionary<int, (int, int)> d = new Dictionary<int, (int, int)>()
        {
            {0, (-1,1)},{1, (0,1)},{2,(1,1)},{3,(-1,0)},{4,(1,0)},{5,(-1,-1)},{6,(0,-1)},{7,(1,-1)}
        };
        (int, int)[] result = new(int, int)[8];
        for (int i = 0; i < 8; i++)
        {
            int xx = x + d[i].Item1;
            int yy = y + d[i].Item2;
            if (xx < 0 || yy < 0 || xx > Manager.Grid.GetLength(0) - 1 || yy > Manager.Grid.GetLength(1) - 1)
            {
                xx = x;
                yy = y;
            }
            result[i] = (xx, yy);
        }
        return result;
    }
}
