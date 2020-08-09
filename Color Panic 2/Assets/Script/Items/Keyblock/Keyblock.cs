using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Keyblock : MonoBehaviour
{
    public static UnityEvent<Vector3> KeyBlockEvent = new UnityEvent<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        KeyBlockEvent.AddListener(open);

    }

    private void open(Vector3 door)
    {
        if(door.x+1 == transform.position.x || 
            door.x - 1 == transform.position.x || 
            door.y + 1 == transform.position.y || 
            door.y - 1 == transform.position.y)
        {
            
            KeyBlockEvent.Invoke(transform.position);

            StartCoroutine(Destroy());

        }
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.3f);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    
}
