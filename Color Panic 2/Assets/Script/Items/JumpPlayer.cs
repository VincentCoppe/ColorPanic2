using UnityEngine;

public class JumpPlayer : MonoBehaviour
{
    public float JumpForce;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")){
            float rota = transform.localEulerAngles.z;
            float sin = Mathf.Sin((rota*Mathf.PI)/180); 
            float cos = Mathf.Cos((rota*Mathf.PI)/180);
            other.gameObject.SendMessage("Push",(sin*JumpForce, cos*JumpForce));
        }
    }
}
