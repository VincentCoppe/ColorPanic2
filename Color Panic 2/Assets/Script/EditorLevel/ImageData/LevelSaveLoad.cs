using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSaveLoad : MonoBehaviour {

    [SerializeField] private TileGameObject[] Block;
    [SerializeField] private TileGameObject[] Powerup;
    [SerializeField] private TileGameObject[] Tapis;
    [SerializeField] private TileGameObject[] Monsters;
    [SerializeField] private TileGameObject[] Object;
    [SerializeField] private TileGameObject[] Bumper;
    [SerializeField] private TileGameObject[] KeyBlock;


    [SerializeField] private LevelManager _level;
    [SerializeField] private ColorPicker ColorPicker = null;
    [SerializeField] private LoadTest lt = null;
    public LevelManager Level { get { return _level; }}


    public void SaveLevel(string path, string folder) {

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
                    color.r = 255;
                    color.g = (byte)((int)_level.GridManagers[0,0].Theme*10);
                    color.b = 255;
                    color.a = 255;
                }
                finally
                {
                    Save.SetPixel(x, y, color);

                }

            }
        }
        Save.Apply();
        byte[] bytes = Save.EncodeToPNG();
        File.WriteAllBytes(Application.streamingAssetsPath + "/levels/" + folder + "/" + path, bytes);
        _level.ClearHistory();
        lt.LevelName = path;
    }

    public void LoadLevel(string path, string folder) {
        DirectoryInfo directoryInfo = new DirectoryInfo(Application.streamingAssetsPath);
        byte[] bytes = null;
        try
        {
            bytes = File.ReadAllBytes(Application.streamingAssetsPath + "/levels/" + folder + "/" + path);

        }
        catch(Exception e)
        {
            Debug.Log(e);
            return;
        }
        Texture2D source = new Texture2D(Level.GridManagers.GetLength(0) * 50, Level.GridManagers.GetLength(1) * 30, TextureFormat.RGBA32, false);
        source.LoadImage(bytes);
        _level.Clear();
        if(_level.ToolsHistory!=null)
            _level.ClearHistory();
        bool theme =false;
        for (int x=0; x<source.width; x++)
        {
            for (int y = 0; y < source.height; y++)
            {
                theme = CreateBlockBase(source.GetPixel(x, y),x,y,theme);
            }
        }
        if(lt != null)
            lt.LevelName = path;
    }
    /*
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
    }*/

    private bool CreateBlockBase(Color32 pixel,int x, int y,bool themeBool) {
        BlockEnum block = (BlockEnum) pixel.r;
        if ((int)block == 255) block = BlockEnum.Air;

        switch (block)
        {
            case (BlockEnum.Air):
                if (!themeBool)
                {
                    _level.ChangeThemeint((pixel.g / 10));
                    if(!(SceneManager.GetActiveScene().name == "Editor"))
                        AudioScript.Instance.ChangeAudioClip((pixel.g/10) + 2);
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
                    case (ObjectEnum.Monsters):
                        block.NewBlock(Monsters[pixel.b]).SpawnTiles(x % 50, y % 30, _level.GridManagers[Mathf.FloorToInt(x / 50), Mathf.FloorToInt(y / 30)], ColorPicker.neutral);
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