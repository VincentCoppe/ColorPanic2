using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterPalette : MonoBehaviour
{
    
    [SerializeField] private Image Selected;
    // Start is called before the first frame update
    public void updateActivePalette(Image image)
    {
        Selected.color = Color.white;
        
        
        image.color = Color.green;
        Selected = image;
    }
}
