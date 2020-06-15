using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoColor : MonoBehaviour
{


   
    [SerializeField] private SpriteRenderer Circle = null;  



    public void OnClickChangeColor(Image image)
    {

        Sequence sequence = DOTween.Sequence();
        Circle.gameObject.SetActive(true);
        Circle.color = image.color;
        Circle.transform.localScale = Vector3.one;
        sequence.Append(Circle.transform.DOScale(Vector3.one * 3000, 0.5f));
        StartCoroutine(ChangeColor(image));
        DOTween.Play(sequence);
    }

    private IEnumerator ChangeColor(Image image)
    {   
        yield return new WaitForSecondsRealtime(0.5f);
        Circle.transform.localScale = Vector3.one;
        Camera.main.backgroundColor = image.color;
        Circle.gameObject.SetActive(false);

    }
}
