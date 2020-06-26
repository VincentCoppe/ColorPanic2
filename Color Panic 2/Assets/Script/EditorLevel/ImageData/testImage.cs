using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class testImage : MonoBehaviour
{
    [SerializeField] private GridManager grid = null;
    // Start is called before the first frame update
    void Start()
    {
        //WRITE
        //
        //
        /*
        Texture2D testSave = new Texture2D(8, 8, TextureFormat.RGBA32, false);

        for (int y = 0; y < testSave.height; y++)
        {
            for (int x = 0; x < testSave.width; x++)
            {
                Color32 color = new Color32();
                color.r = 0xff;
                color.g = 0xc0;
                color.b = 0x00;
                color.a = 0xff;
                testSave.SetPixel(x, y, color);
            }
        }
        testSave.Apply();

        byte[] bytes = testSave.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/Resources/levels/test.png", bytes);
        
        
        // READ
        //
        //
        byte[] bytesA = File.ReadAllBytes(Application.dataPath + "/Resources/levels/test.png");
        Texture2D testLoad = new Texture2D(8, 8, TextureFormat.RGBA32, false) ; 
        testLoad.LoadImage(bytesA);

        for (int y = 0; y < testLoad.height; y++)
        {
            for (int x = 0; x < testLoad.width; x++)
            {
                Color32 a = testLoad.GetPixel(x, y);
                print(a.r.ToString("X")+ a.g.ToString("X")+ a.b.ToString("X") + a.a.ToString("X"));
            }
        }
        */
    }


    public void CreateImageFromGrid()
    {
        Texture2D testSave = new Texture2D(grid.Grid.GetLength(0), grid.Grid.GetLength(1), TextureFormat.RGBA32, false);

        for (int y = 0; y < testSave.height; y++)
        {
            for (int x = 0; x < testSave.width; x++)
            {
                Color32 color = new Color32();
                try
                {
                    BlockBase block = grid.GridObject[x, y];
                    long save = block.Save();
                    byte[] b = BitConverter.GetBytes(save);
                    print(save.ToString("x"));
                    color.r = b[3];
                    color.g = b[2];
                    color.b = b[1];
                    color.a = b[0];

                } catch (NullReferenceException e)
                {
                    color = Color.white;
                } finally
                {
                    testSave.SetPixel(x, y, color);

                }

            }
        }
        testSave.Apply();

        byte[] bytes = testSave.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/Resources/levels/testGridLevel.png", bytes);
    }

}
