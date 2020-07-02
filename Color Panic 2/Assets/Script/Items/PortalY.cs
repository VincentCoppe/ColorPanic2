using UnityEngine;

public class PortalY : MonoBehaviour
{
    public bool Up;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")){
            other.gameObject.SendMessage("MoveOppositeY", Up);
        }
    }
}
