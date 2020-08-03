using UnityEngine;
using System.Collections;
using System.Threading.Tasks;

public class Volatile : MonoBehaviour
{
    [SerializeField] private float DisappearTime = 1f;
    [SerializeField] private float ReappearTime = 5f;
    private bool started;
    private SpriteRenderer rend;
    private Animator anim;
    [SerializeField] private BoxCollider2D collid;
    private PlayerController player;

    private void Start() {
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerController>();
        started = false;
    }

    private void Update() {
        if (player != null && player.respawning){
            rend.enabled = true;
            collid.enabled = true;
            started = false;
        }
    }

    private void OnEnable() {
        rend.enabled = true;
        collid.enabled = true;
        started = false;
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && !started){
            started = true;
            StartCoroutine(Disappear());
        }
    }

    IEnumerator Disappear(){
        anim.SetTrigger("Destroy");
        
        yield return new WaitForSecondsRealtime(DisappearTime);
        rend.enabled = false;
        collid.enabled = false;
        StartCoroutine(Reappear());  
    }

    IEnumerator Reappear(){
        yield return new WaitForSecondsRealtime(ReappearTime);
        rend.enabled = true;
        collid.enabled = true;
        started = false;
    }
}
