using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setupUiTable : MonoBehaviour
{
    public GameObject a00;
   

    
    void Start()
    {
        GameObject[][] table = new GameObject[8][] { new GameObject[8], new GameObject[8], new GameObject[8], new GameObject[8], new GameObject[8], new GameObject[8], new GameObject[8], new GameObject[8] };

        for(int x=0; x<8; x++)
        {
            for(int y=0; y<8; y++)
            {
                if (x != 0 || y != 0) { 
                table[x][y] = Instantiate(a00, this.transform);
       
                table[x][y].GetComponent<RectTransform>().localPosition = new Vector3(-63 + x * 18, -36 + y * 11, 0);
                
                table[x][y].name = "(" + x + "," + y + ")";
               
                }
            }
        }
        a00.GetComponent<Image>().color = Color.green;
    }
}
