using System;
using System.IO;
using UnityEngine;

public static class LevelSaveLoad {
    public static void SaveLevel(LevelData level, string path) {
        Texture2D target = new Texture2D(level.Width * 50, level.Height * 30, TextureFormat.RGBA32, false);
        
        for (int x = 0; x < level.Width; x++)
        {
            for (int y = 0; y < level.Height; y++)
            {
                SaveGrid(target, level.Blocks[x,y]);
            }
        }

        target.Apply();

        byte[] bytes = target.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/Resources/levels/testGridLevel.png", bytes);
    }

    private static void SaveGrid(Texture2D target, BlockBase[,] blocks) {
        for (int x = 0; x < blocks.GetLength(0); x++)
        {
            for (int y = 0; y < blocks.GetLength(1); y++)
            {
                Color32 color = new Color32();
                try
                {
                    BlockBase block = blocks[x,y];
                    long save = block.Save();
                    byte[] b = BitConverter.GetBytes(save);
                    color.r = b[3];
                    color.g = b[2];
                    color.b = b[1];
                    color.a = b[0];

                } catch (NullReferenceException e)
                {
                    color = Color.white;
                } finally
                {
                    target.SetPixel(y, x, color);
                }
            }
        }
    }

    public static LevelData LoadLevel(string path) {
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

    private static BlockBase[,] LoadGrid(Texture2D source, int x, int y) {
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