using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_MaxSpeed = 10f;      
    [SerializeField] private float m_JumpForce = 1000f;                
    [SerializeField] private float DashTime = 0.1f;                
    [SerializeField] private float DashSpeed = 6;         
    [SerializeField] private float GrabFallSpeed = 0.1f;                    
    [SerializeField] private LayerMask m_WhatIsGround;                 
    

    private Transform m_CeilingCheck;   // A position marking where to check for ceilings
    private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
    private Transform m_RightCheck;
    private Transform m_LeftCheck;
    private Animator m_Anim;            
    private Rigidbody2D m_Rigidbody2D;
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
    public bool Grounded { get; private set; } 
    public bool OnLeftWall { get; private set; }
    public bool OnRightWall { get; private set; }
    private bool FacingRight = true; 
    private Vector3 respawn = new Vector3(0,0,0);
    private Checkpoint CurrentCheckpoint = null;

    private MyPower power;
    private bool Djump;
    private bool dash;
    private bool dashing;
    private bool grabbing;
    private float dashTimer;
    private float gravity;


    private void Awake()
    {
        // Setting up references.
        m_GroundCheck = transform.Find("GroundCheck");
        m_CeilingCheck = transform.Find("CeilingCheck");
        m_LeftCheck = transform.Find("LeftWallCheck");
        m_RightCheck = transform.Find("RightWallCheck");
        m_Anim = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        gravity = m_Rigidbody2D.gravityScale;
        power = new MyPower();
        respawn = transform.localPosition;
    }

    public void Death()
    {
        if (CurrentCheckpoint != null){
            power.ResetPowers(CurrentCheckpoint.SavedPowers);
        } else {
            power.ResetPowers();
        }
        this.gameObject.transform.localPosition = respawn;
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
        OnLeftWall = false;
        OnRightWall = false;

        if (grabbing){
            Grab();
        } else{
            Ungrab();
        }
        m_Anim.SetBool("Grabbing",grabbing);
        //Check ground
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius*1.2f, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                Grounded = true;
                Djump = true;
                dash = true;
            }
        }
        m_Anim.SetBool("Grounded",Grounded);

        //Check wall on left
        Collider2D[] collidersL = Physics2D.OverlapCircleAll(m_LeftCheck.position, k_GroundedRadius*2, m_WhatIsGround);
        for (int i = 0; i < collidersL.Length; i++)
        {
            if (collidersL[i].gameObject != gameObject)
            {
                if (FacingRight){
                    OnRightWall = true;
                } else {
                    OnLeftWall = true;
                }
            }
        }

        //Check wall on right
        Collider2D[] collidersR = Physics2D.OverlapCircleAll(m_RightCheck.position, k_GroundedRadius*2, m_WhatIsGround);
        for (int i = 0; i < collidersR.Length; i++)
        {
            if (collidersR[i].gameObject != gameObject)
            {
                if (FacingRight){
                    OnLeftWall = true;
                } else {
                    OnRightWall = true;
                }
            }
        }

        //Force applied during the dash
        if (dashing){
            dashTimer -= Time.fixedDeltaTime;
            m_Rigidbody2D.gravityScale = 0f;
            if(FacingRight){
                m_Rigidbody2D.velocity = new Vector2(1f*DashSpeed, 0f);
            }
            else {
                m_Rigidbody2D.velocity = new Vector2(-1f*DashSpeed, 0f);
            }
            //End of the dash
            if(dashTimer<=0){
                dashing = false;
                m_Rigidbody2D.gravityScale = gravity;
                ResetMovement();
            }
        }

    }

    public void Move(float move, bool jump)
    {
        if (dashing) return; 

        //Flip sprite
        if ( (move > 0 && !FacingRight) || (move < 0 && FacingRight)){
            Flip();
        }

        //Grab if not grounded and there is a wall
        if (OnLeftWall && !Grounded || OnRightWall && !Grounded){
            if (OnLeftWall && move > 0 || OnRightWall && move < 0){
                grabbing = true;
                return;
            }
        }
        Debug.Log(jump);
        if(Grounded || OnLeftWall && move < 0 || OnRightWall && move > 0 || jump){
            Debug.Log("yes");
            grabbing = false;
        }

        //Move
        m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);
        m_Anim.SetFloat("AbsSpeed", Mathf.Abs(m_Rigidbody2D.velocity.x));


        //Jump
        if ( Grounded && jump && !dashing)
        {
            Jump(m_JumpForce);
        //Double jump
        } else if ( !Grounded && jump && power.HavePower("Green") && Djump && !dashing)
        {
            Jump(m_JumpForce);
            Djump = false;
        }
        //Dash
        if (Input.GetKey("space") && !Grounded && power.HavePower("Red") && dash ){
            Dash(move);
            dash = false;
        }
    }

    private void Grab(){
        m_Rigidbody2D.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
        MoveY(GrabFallSpeed);
    }

    private void Ungrab(){
        m_Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void MoveY(float move)
    {
        Vector3 vector = transform.localPosition;
        vector.y -= move;
        transform.localPosition = vector;
    }

    private void Jump(float force)
    {
        m_Anim.SetBool("Grounded", false);
        Grounded = false;
        ResetMovement();
        m_Rigidbody2D.AddForce(new Vector2(0f, force));
    }

    private void Dash(float move){
        ResetMovement();
        dashTimer = DashTime;
        dashing = true;
    }

    private void Push((float, float) xy)
    {
        m_Rigidbody2D.AddForce(new Vector2(xy.Item1, xy.Item2));
    }

    private void SetRespawn(Checkpoint checkpoint){
        //Disable old checkpoint
        if (CurrentCheckpoint != null && CurrentCheckpoint != checkpoint){
            CurrentCheckpoint.Activation(false);
        }
        //Set new checkpoint
        if (CurrentCheckpoint != checkpoint){
            checkpoint.Activation(true);
            checkpoint.SavedPowers =  new List<Power>(power.Powers);

            //Transfer power from the old checkpoint to the new one
            if (CurrentCheckpoint!=null){
                foreach(Power p in CurrentCheckpoint.SavedPowers){
                    if (!checkpoint.SavedPowers.Contains(p)){
                        checkpoint.SavedPowers.Add(p);
                    }
                }
            }

            //Set the respawn point of the player
            respawn = checkpoint.gameObject.transform.localPosition;
            CurrentCheckpoint = checkpoint;
        }
    }


    private void Flip()
    {
        FacingRight = !FacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void ResetMovement(){
        m_Rigidbody2D.velocity = new Vector2(0f, 0f);
    }
}
