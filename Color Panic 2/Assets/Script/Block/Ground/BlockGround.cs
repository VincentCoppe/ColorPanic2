using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BlockGround : BlockBase
{
    private CBD_Ground _data;


    private int[][] Placement = new int[][] {
        new int[]{0,1,3},
        new int[]{1},
        new int[]{1,2,4},
        new int[]{3},
        new int[]{4},
        new int[]{3,5,6},
        new int[]{6},
        new int[]{4,6,7},
    };

    public BlockGround(TileGameObject prefab) : base(prefab) {}

    public override void Init(long data) { }

    public override void DestroyTiles(int x, int y)
    {

        if (Manager.Grid[x, y] == (BlockEnum)1)
        {
            Manager.Grid[x, y] = BlockEnum.Air;
            Manager.GridObject[x, y] = null;
            CalculateNeighbours(x, y, true);
            OrientedSpikes(x, y);
            UnityEngine.Object.Destroy(GameObject);
        }
    }

    private void OrientedSpikes(int x, int y)
    {
        (int, int)[] neighbours = Manager.Get4Neighbours(x, y);
        
        foreach((int, int) neighbour in neighbours)
        {
            if (Manager.Grid[neighbour.Item1, neighbour.Item2] == BlockEnum.Spike)
            {                
                BlockSpike a = (BlockSpike)Manager.GridObject[neighbour.Item1, neighbour.Item2];
                a.CalculateOrientation(neighbour.Item1, neighbour.Item2);
            }
        }

    }

    public override long Save()
    {
        return base.Save();
    }

    protected override bool Spawn(int x, int y)
    {
        if (base.Spawn(x, y)) {
            CalculateNeighbours(x, y, true);
            OrientedSpikes(x, y);
            return true;
        }
        return false;
    }

    public void CalculateNeighbours(int x, int y, bool first)
    {
        (int, int)[] Neighbours = Manager.Get8Neighbours(x, y);

        List<BlockEnum> BeNeighbours = new List<BlockEnum>();
        foreach ((int, int) Neighbour in Neighbours)
        {
            if (first && Manager.Grid[Neighbour.Item1, Neighbour.Item2] == (BlockEnum)1) CalculateNeighbours(Neighbour.Item1, Neighbour.Item2, false);

            BeNeighbours.Add(Manager.Grid[Neighbour.Item1, Neighbour.Item2]);

        }
        BlockGround block;
        if (Manager.Grid[x, y] == (BlockEnum)1)
        {
            block = (BlockGround) Manager.GridObject[x, y];
            block?.CalculateEdge(BeNeighbours);
        }

    }

    public void CalculateEdge(List<BlockEnum> Neighbours)
    {
        var data = Data<CBD_Ground>();
        double AllFalse = 0;
        for (int i = 0; i < data.Corners.Length; i++)
        {
            bool a = false;

            foreach (int n in Placement[i])
            {
                if (Neighbours[n] != (BlockEnum)1) a = true;
            }
            AllFalse = (!a) ? AllFalse + Math.Pow(2, i) : AllFalse;
            data.Corners[i].gameObject.SetActive(a);
        }
        switch(Manager.Theme)
        {
            case ThemeEnum.Dungeon:
                int brick = 0;
                if (AllFalse == 255)
                {
                    brick = UnityEngine.Random.Range(0, 16);
                    if (brick >= 4) brick = 4;
                }
                else
                {
                    brick = UnityEngine.Random.Range(0, 4);
                }
                data.Center[0].sprite = data.Brick1[brick];
                data.Center[1].sprite = data.Brick2[brick];
                data.Center[2].sprite = data.Station[0];
                for (int i = 0; i < data.Corners.Length; i++)
                {
                    if(i == 0 || i == 2 || i == 5 || i == 7)
                    {
                        data.Corners[i].sprite = data.BrickCorner[0];
                    } else
                    {
                        data.Corners[i].sprite = data.BrickCorner[1];
                    }
                }
                return;
            case ThemeEnum.Space:
                for (int i = 0; i < data.Corners.Length; i++)
                {
                    if (i == 0 || i == 2 || i == 5 || i == 7)
                    {
                        data.Corners[i].sprite = data.StationCorner[0];
                    }
                    else
                    {
                        data.Corners[i].sprite = data.StationCorner[1];
                    }
                    data.Corners[i].color = Colors[2];
                }
                data.Center[0].sprite = null;
                data.Center[1].sprite = null;
                data.Center[2].sprite = null;
                return;
            case ThemeEnum.Station:
                for (int i = 0; i < data.Corners.Length; i++)
                {
                    if (i == 0 || i == 2 || i == 5 || i == 7)
                    {
                        data.Corners[i].sprite = data.StationCorner[0];
                    }
                    else
                    {
                        data.Corners[i].sprite = data.StationCorner[1];
                    }
                    data.Corners[i].color = Colors[2];
                }
                
                
 
                data.Center[0].sprite = data.Station[0];
                data.Center[1].sprite = data.Station[1];
                data.Center[2].sprite = data.Station[1];

                return;
        }
    }

    public override void UpdateColors(Color[] Colors)
    {
        var data = Data<CBD_Ground>();
        this.Colors = Colors;
        switch (Manager.Theme)
        {
            case ThemeEnum.Dungeon:
                data.Center[0].color = Colors[1];
                data.Center[1].color = Colors[2];
                data.Center[2].color = Colors[0];
                foreach(var cor in data.Corners)
                {
                    cor.color = Color.black;
                }
                return;
            case ThemeEnum.Space:
                foreach (var cor in data.Corners)
                {
                    cor.color = Colors[2];
                }
                return;
            case ThemeEnum.Station:
                data.Center[0].color = Colors[1];
                data.Center[1].color = Colors[0];
                data.Center[2].color = Colors[0];
                foreach (var cor in data.Corners)
                {
                    cor.color = Colors[2];
                }
                return;

        }
        base.UpdateColors(Colors);
    }

    public override void ChangeTheme(int x, int y)
    {
        CalculateNeighbours(x, y, true);
        UpdateColors(Colors);
    }
}
