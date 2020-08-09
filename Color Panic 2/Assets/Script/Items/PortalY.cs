using UnityEngine;

public class PortalY : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")){
            if (transform.localEulerAngles.z == 0)
                other.gameObject.SendMessage("MoveOppositeY", true);
            else if (transform.localEulerAngles.z == 90) 
                other.gameObject.SendMessage("MoveOppositeX", true);
            else if (transform.localEulerAngles.z == 180)
                other.gameObject.SendMessage("MoveOppositeY", false);
            else if (transform.localEulerAngles.z == 270)
                other.gameObject.SendMessage("MoveOppositeX", false);
        }
    }
}
