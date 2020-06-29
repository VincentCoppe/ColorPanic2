using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseParent : MonoBehaviour
{
    public void HideParent()
    {
        this.transform.parent.gameObject.SetActive(false);
    }
}
