using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSaveLoadIG : MonoBehaviour {

    [SerializeField] private TileGameObject[] Block;
    [SerializeField] private TileGameObject[] Powerup;
    [SerializeField] private TileGameObject[] Tapis;
    [SerializeField] private TileGameObject[] Object;
    [SerializeField] private TileGameObject[] Bumper;
    [SerializeField] private TileGameObject[] KeyBlock;


    [SerializeField] private LevelManagerIG _level;
    [SerializeField] private ColorPicker ColorPicker = null;
    public LevelManagerIG Level { get { return _level; }}

    public void LoadLevel(string path, string folder) {
        DirectoryInfo directoryInfo = new DirectoryInfo(Application.streamingAssetsPath);
        byte[] bytes = null;
        try
        {
            bytes = File.ReadAllBytes(Application.streamingAssetsPath + "/levels/"+folder+"/" + path);

        }
        catch(Exception e)
        {
            Debug.Log(e);
            return;
        }
        Texture2D source = new Texture2D(Level.GridManagers.GetLength(0) * 50, Level.GridManagers.GetLength(1) * 30, TextureFormat.RGBA32, false);
        source.LoadImage(bytes);
        _level.Clear();
        bool theme =false;
        for (int x=0; x<source.width; x++)
        {
            for (int y = 0; y < source.height; y++)
            {
                theme = CreateBlockBase(source.GetPixel(x, y),x,y,theme);
            }
        }
    }

    private bool CreateBlockBase(Color32 pixel,int x, int y,bool themeBool) {
        BlockEnum block = (BlockEnum) pixel.r;
        if ((int)block == 255) block = BlockEnum.Air;

        switch (block)
        {
            case (BlockEnum.Air):
                if (!themeBool)
                {
                    _level.ChangeTheme((pixel.g / 10));
                    themeBool = true;
                }
                return themeBool;
            case (BlockEnum.Powerup):
                block.NewBlock(Powerup[pixel.g]).SpawnTiles(x % 50, y % 30, _level.GridManagers[Mathf.FloorToInt(x / 50), Mathf.FloorToInt(y / 30)], ColorPicker.neutral);
                return themeBool;
            case (BlockEnum.Object):
                ObjectEnum objet = (ObjectEnum)pixel.g;
                switch (objet)
                {
                    case (ObjectEnum.Tapis):
                        block.NewBlock(Tapis[((pixel.b == 180) ? 0 : 1)]).SpawnTiles(x % 50, y % 30, _level.GridManagers[Mathf.FloorToInt(x / 50), Mathf.FloorToInt(y / 30)], ColorPicker.neutral);
                        return themeBool;
                    case (ObjectEnum.Jumper):
                        block.NewBlock(Bumper[((pixel.b == 180) ? 0 : 1)]).SpawnTiles(x % 50, y % 30, _level.GridManagers[Mathf.FloorToInt(x / 50), Mathf.FloorToInt(y / 30)], ColorPicker.neutral);
                        return themeBool;

                    case (ObjectEnum.KeyBlock):
                        block.NewBlock(KeyBlock[((pixel.b == 100) ? 1 : 0)]).SpawnTiles(x % 50, y % 30, _level.GridManagers[Mathf.FloorToInt(x / 50), Mathf.FloorToInt(y / 30)], ColorPicker.neutral);
                        return themeBool;
                    default:
                        block.NewBlock(Object[(int)objet]).SpawnTiles(x % 50, y % 30, _level.GridManagers[Mathf.FloorToInt(x / 50), Mathf.FloorToInt(y / 30)], ColorPicker.neutral);
                        return themeBool;
                    

                }
                

            default:
                
            
                block.NewBlock(Block[(int)block]).SpawnTiles(x % 50, y % 30, _level.GridManagers[Mathf.FloorToInt(x / 50), Mathf.FloorToInt(y / 30)], ColorPicker.neutral);
                return themeBool;
                
        }
        
    }
}