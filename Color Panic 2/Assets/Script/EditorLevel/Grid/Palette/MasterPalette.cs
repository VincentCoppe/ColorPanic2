using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterPalette : MonoBehaviour
{
    [SerializeField] private List<Image> ListPalette = new List<Image>();
    // Start is called before the first frame update
    public void updateActivePalette(Image image)
    {
        foreach(Image img in ListPalette)
        {
            img.color = Color.white;
        }
        image.color = Color.green;
    }
}
