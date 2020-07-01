using UnityEngine;
using System.Collections;

public class Volatile : MonoBehaviour
{
    [SerializeField] private float DisappearTime = 1f;
    [SerializeField] private float ReappearTime = 5f;
    [SerializeField] private Color BaseColor;
    [SerializeField] private Color TouchColor;
    private GameObject main;
    private bool started;
    private Renderer rend;
    [SerializeField] private BoxCollider2D collid;

    private void Start() {
        main = this.transform.GetChild(0).gameObject;
        rend = main.GetComponent<Renderer>();
        rend.material.color = BaseColor;
        started = false;
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && !started){
            started = true;
            StartCoroutine(Disappear());
        }
    }

    IEnumerator Disappear(){
        rend.material.color = TouchColor;
        yield return new WaitForSecondsRealtime(DisappearTime);
        main.transform.gameObject.SetActive(false);
        collid.enabled = false;
        StartCoroutine(Reappear());  
    }

    IEnumerator Reappear(){
        yield return new WaitForSecondsRealtime(ReappearTime);
        main.transform.gameObject.SetActive(true);
        rend.material.color = BaseColor;
        collid.enabled = true;
        started = false;
    }
}
