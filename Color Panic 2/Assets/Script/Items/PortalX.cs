using UnityEngine;

public class PortalX : MonoBehaviour
{
    public bool Right;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")){
            other.gameObject.SendMessage("MoveOppositeX", Right);
        }
    }
}
