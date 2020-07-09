using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    private PlayerController m_Character;
    private bool m_Jump;


    private void Awake()
    {
        m_Character = GetComponent<PlayerController>();
    }


    private void Update()
    {
        if (!m_Jump)
        {
            // Read the jump input in Update so button presses aren't missed.
            m_Jump = Input.GetButtonDown("Jump");
        }
    }


    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        //Take the last input of the player
        if ( (Input.GetKey(KeyCode.RightArrow) && h < 0) || (Input.GetKey(KeyCode.LeftArrow) && h > 0) ){
            h *= -1;
        }
        // Pass all parameters to the character control script.
        m_Character.Move(h, m_Jump);
        m_Jump = false;
    }
}
