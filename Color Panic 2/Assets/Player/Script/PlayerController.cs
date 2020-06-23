using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
    [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.     
    [SerializeField] private float DashForce = 1000f;                            
    [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

    private Transform m_CeilingCheck;   // A position marking where to check for ceilings
    private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
    private Transform m_RightCheck;
    private Transform m_LeftCheck;
    private Animator m_Anim;            // Reference to the player's animator component.
    private Rigidbody2D m_Rigidbody2D;
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
    public bool Grounded { get; private set; } // Whether or not the player is grounded.
    public bool OnLeftWall { get; private set; }
    public bool OnRightWall { get; private set; }
    private bool FacingRight = true; // For determining which way the player is currently facing.
    private Vector3 respawn = new Vector3(0,0,0);
    private GameObject CurrentCheckpoint = null;

    private MyPower power;
    private bool Djump;
    private bool dash;

    private void Awake()
    {
        // Setting up references.
        m_GroundCheck = transform.Find("GroundCheck");
        m_CeilingCheck = transform.Find("CeilingCheck");
        m_LeftCheck = transform.Find("LeftWallCheck");
        m_RightCheck = transform.Find("RightWallCheck");
        m_Anim = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        power = new MyPower();
    }

    public void Death()
    {
        this.gameObject.transform.localPosition = respawn;
        power.ResetPowers();
    }

    public void MoveX(float move)
    {
        Vector3 vector = transform.localPosition;
        vector.x -= move;
        transform.localPosition = vector;
    }

    public void AddPower(Power newpower){
        power.AddPower(newpower);
    }

    private void FixedUpdate()
    {
        Grounded = false;
        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                Grounded = true;
                Djump = true;
                dash = true;
            } else {
                Grounded = false;
            }
        }
        m_Anim.SetBool("Grounded",Grounded);
    }


    public void Move(float move, bool jump)
    {

        //only control the player if grounded or airControl is turned on
        

            // The Speed animator parameter is set to the absolute value of the horizontal input.

            // Move the character
            m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);
           
            

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !FacingRight)
            {
                // ... flip the player.
                
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && FacingRight)
            {
            // ... flip the player.
           
            Flip();
            }
        m_Anim.SetFloat("AbsSpeed", Mathf.Abs(m_Rigidbody2D.velocity.x));
        // If the player should jump...
        if ( Grounded && jump)
        {
            // Add a vertical force to the player.
            Jump(m_JumpForce);
        } else if ( !Grounded && jump && power.HavePower("Green") && Djump) // or if he can double jump
        {
            Jump(m_JumpForce);
            Djump = false;
        }
        if (Input.GetKey("space") && !Grounded && power.HavePower("Red") && dash ){
            Dash(move);
            dash = false;
        }
    }

    private void Jump(float force)
    {
        m_Anim.SetBool("Grounded", false);
        Grounded = false;
        m_Rigidbody2D.AddForce(new Vector2(0f, force));
    }

    private void Dash(float move){
        if (FacingRight){
            m_Rigidbody2D.velocity = new Vector2(DashForce*Time.fixedDeltaTime, 0f);
        } else {
            m_Rigidbody2D.velocity = new Vector2(-DashForce*Time.fixedDeltaTime, 0f);
        }
    }

    private void Push((float, float) xy)
    {
        m_Rigidbody2D.AddForce(new Vector2(xy.Item1, xy.Item2));
    }

    private void SetRespawn(GameObject checkpoint){
        if (CurrentCheckpoint != null){
            CurrentCheckpoint.gameObject.GetComponent<Renderer>().material.color = Color.cyan;
        }
        CurrentCheckpoint = checkpoint;
        respawn = checkpoint.gameObject.transform.localPosition;
    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        FacingRight = !FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
