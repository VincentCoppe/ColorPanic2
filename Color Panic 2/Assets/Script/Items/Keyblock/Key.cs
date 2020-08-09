using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Key : MonoBehaviour
{
    private Transform Player;
    [SerializeField] private Rigidbody2D rigidbody;
    float KeyNb;
    private bool follow = false;
    private bool used = false;
    public static UnityEvent<Transform> keyUsed = new UnityEvent<Transform>();

    public void Start()
    {
        keyUsed.AddListener(UseKey);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && !follow){
            Player = other.gameObject.transform;
            other.gameObject.SendMessage("IncreaseKeys");
            follow = true;
            KeyNb = Player.gameObject.GetComponent<PlayerController>().keys * 1f;
        }
    }

   private void Update()
   {
        followPlayer();
   }

    public void followPlayer()
    {
        if (follow && Vector3.Distance(transform.position, Player.position) > KeyNb && !used)
        {
            Vector3 Dir = (Player.position - transform.position).normalized;
            float Angle = Mathf.Atan2(Dir.y, Dir.x) * Mathf.Rad2Deg;
            Quaternion RotateToTarget = Quaternion.AngleAxis(Angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, RotateToTarget, Time.deltaTime * 5);
            rigidbody.velocity = new Vector2(Dir.x*10, Dir.y*10);
            
        } else
        {
            rigidbody.velocity = Vector2.zero;
        }
    }

    void UseKey(Transform keyblock)
    {
        if (KeyNb == 1)
        {
            used = true;
            Sequence p = DOTween.Sequence();
            p.Append(transform.DOLocalMove(keyblock.position,0.3f));
            p.Join(transform.DORotate(Vector3.zero, 0.3f));
            DOTween.Play(p);
            Destroy(this.gameObject, 0.3f);
        } else
        {
            KeyNb--;
        }
    }
}
