using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSaveLoad : MonoBehaviour {

    [SerializeField] private LevelManager _level;
    public LevelManager Level { get { return _level; }}

    public void SaveLevel(Text path) {

        Texture2D Save = new Texture2D(Level.GridManagers.GetLength(0) * 50, Level.GridManagers.GetLength(1) * 30, TextureFormat.RGBA32, false);

        for (int y = 0; y < Save.height; y++)
        {
            for (int x = 0; x < Save.width; x++)
            {
                Color32 color = new Color32();
                try
                {

                    BlockBase block = Level.GridManagers[Mathf.FloorToInt(x / 50), Mathf.FloorToInt(y / 30)].GridObject[x % 50, y % 30];
                    long save = block.Save();
                    byte[] b = BitConverter.GetBytes(save);
                    color.r = b[3];
                    color.g = b[2];
                    color.b = b[1];
                    color.a = b[0];

                }
                catch (NullReferenceException e)
                {
                    color = Color.white;
                }
                finally
                {
                    Save.SetPixel(x, y, color);

                }

            }
        }
        Save.Apply();

        byte[] bytes = Save.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/Resources/levels/"+path.text+".png", bytes);
       
    }

    public LevelData LoadLevel(string path) {
        byte[] bytes = File.ReadAllBytes(path);
        Texture2D source = null;
        ImageConversion.LoadImage(source, bytes);
        LevelData level = new LevelData(source.width/50, source.height/30, null);

        for (int x=0; x<source.width/50; x++)
        {
            for (int y = 0; y < source.height/30; y++)
            {
                level.SetGrid(x, y, LoadGrid(source, x, y));
            }
        }
        return level;
    }

    private BlockBase[,] LoadGrid(Texture2D source, int x, int y) {
        BlockBase[,] blocks = new BlockBase[50,30];
        for (int dx = 0; dx < 50; dx++)
        {
            for (int dy = 0; dy < 30; dy++)
            {
                Color32 pixel = source.GetPixel(x*50+dx, y*30+dy);
                blocks[dx,dy] = CreateBlockBase(pixel);
            }
        }
        return blocks;
    }

    private static BlockBase CreateBlockBase(Color32 pixel) {
        BlockEnum block = (BlockEnum) pixel.r;
        BlockBase blockBase = block.NewBlock(null);
        long data = (pixel.r << 24) | (pixel.g << 16) | (pixel.b << 8) | pixel.a;
        blockBase.Init(data);
        return blockBase;
    }
}