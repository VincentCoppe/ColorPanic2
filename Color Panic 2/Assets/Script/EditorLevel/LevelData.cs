using UnityEngine;

public class LevelData {
    private BlockBase[,][,] _blocks;
    private Color[] _colors;

    public BlockBase[,][,] Blocks { get { return _blocks; }}
    public Color[] Colors { get { return _colors; }}

    public int Width { get { return _blocks.GetLength(0); }}
    public int Height { get { return _blocks.GetLength(1); }}

    public LevelData(int gridsX, int gridsY, Color[] colors) {
        _colors = colors;
        _blocks = new BlockBase[gridsX, gridsY][,];
        for (int x=0; x<gridsX; x++) {
            for (int y = 0; y < gridsY; y++)
            {
                _blocks[x, y] = new BlockBase[50,30];
            }
        }
    }
}