using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{

    [SerializeField] private SpriteRenderer[] Corner = new SpriteRenderer[8];

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
    


    public void calculateNeighbours(List<BlockEnum> Neighbours)
    {
        for(int i = 0; i<Corner.Length;i++) {
            bool a = false;
            
            foreach(int n in Placement[i])
            {
                if (Neighbours[n] != (BlockEnum)1) a = true;
            }
            Corner[i].gameObject.SetActive(a);
        }
    }


}
