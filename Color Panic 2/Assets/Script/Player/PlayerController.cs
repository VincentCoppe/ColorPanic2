using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_MaxSpeed = 10f;      
    [SerializeField] private float m_JumpForce = 1000f;
    [SerializeField] private float WallJumpPushForce = 400;
    [SerializeField] private float FlyForce = 200;     
    [SerializeField] private float FlySpeedCap =  25;
    [SerializeField] private float FlyFuel = 50;            
    [SerializeField] private float DashTime = 0.1f;                
    [SerializeField] private float DashSpeed = 6;     
    [SerializeField] private float GrabUpFactor = 0.95f;     
    [SerializeField] private float GrabDownFactor = 0.7f;   
    [SerializeField] private float HighJumpFactor = 0.8f;  
    [SerializeField] private float LowJumpFactor = 0.8f;                   
    [SerializeField] private LayerMask m_WhatIsGround;  
    [SerializeField] private float GravityFactorViridian = 0.20f; 
    [SerializeField] private float VelocityYCap = 40; 
    [SerializeField] private Color Viridian; 
    [SerializeField] private Color Red;   
    [SerializeField] private Color Green;       
    [SerializeField] private Color Purple;   
    [SerializeField] private Color Yellow; 
    [SerializeField] private Color Blue;
    
    [SerializeField] private ParticleSystem SpawnParticle;
    [SerializeField] private ParticleSystem DeadParticle;

    private Transform m_CeilingCheck;   // A position marking where to check for ceilings
    private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
    private Transform m_RightCheck;
    private Transform m_LeftCheck;
    private Transform m_RightCheckLow;
    private Transform m_LeftCheckLow;

    private Animator m_Anim;       
    
    private Rigidbody2D m_Rigidbody2D;
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
    public bool Grounded { get; private set; } 
    public bool Ceiling { get; private set; } 
    public bool OnLeftWall { get; private set; }
    public bool OnRightWall { get; private set; }
    public bool FacingRight = true; 
    public Vector3 respawn = new Vector3(0,0,0); //Saved respawn location of the player
    private Checkpoint CurrentCheckpoint = null; //Current saved checkpoint of the user
    public MyPower power;
    public bool Djump; //Can the player do a double jump ?
    private bool Hjump; //High jump
    public bool dash;  //The player can dash ?
    private bool dashing;   //The player is currently dashing ?
    private bool grabbing;  //The player is currently grabbing ?
    private float dashTimer;  
    public float gravity;
    private float WalljumpTimer = 0;
    private bool reverse = false; //Gravity reverse
    private bool gravityReverse; //Can the player reverse the gravity ?
    public bool win = false;
    public bool pause = false;
    private bool respawning = false;
    public bool teleport;
    public int keys = 0;

    public float OppositeX;
    public float OppositeY;
    public float DistanceX;
    public float DistanceY;
    public int coin = 0;
    public int death = 0;
    private float CurrentFlyFuel = 0;
    private bool col = true; //bool to block multiple collisions with multiples teleport objects, resulting in a big teleportation out of the map

    private void Awake()
    {
        m_GroundCheck = transform.Find("GroundCheck");
        m_CeilingCheck = transform.Find("CeilingCheck");
        m_LeftCheck = transform.Find("LeftWallCheck");
        m_RightCheck = transform.Find("RightWallCheck");
        m_LeftCheckLow = transform.Find("LeftWallCheckLow");
        m_RightCheckLow = transform.Find("RightWallCheckLow");
        m_Anim = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        gravity = m_Rigidbody2D.gravityScale;
        power = new MyPower();
        respawn = transform.localPosition;
    }

    private void FixedUpdate()
    {
        Grounded = false;
        Ceiling = false;
        OnLeftWall = false;
        OnRightWall = false;
        col = true;

        //Die if press R
        if(Input.GetKey(KeyCode.R)){
            Death();
        }
        //Check if the player is in grab
        if (grabbing){
            Grab();
        } else{
            Ungrab();
        }
        m_Anim.SetBool("Grabbing",grabbing);
        
        HandleGroundCheck();
        HandleCeilingCheck();
        HandleLeftCheck();
        HandleRightCheck();
        HandleDash();

        if (WalljumpTimer>0){
            WalljumpTimer -= Time.fixedDeltaTime;
        }

        //Reduce the gravity if the player have the viridian power
        if (power.HavePower("Viridian")){
            m_Rigidbody2D.gravityScale = gravity*GravityFactorViridian;
        } else {
            m_Rigidbody2D.gravityScale = gravity;
        }
        //Cancel the gravity reverse if the player loose viridian
        if (reverse && !power.HavePower("Viridian")){
            GravityReverse();
        }

    }

    //All the movements of the player called when he press keys to move or jump
    public void Move(float move, bool jump)
    {
        //if the player can move, return
        if (dashing || WalljumpTimer>0 || win || pause || respawning) return; 
        HandleGrab(move, jump);

        //Flip sprite
        if ( ((move > 0 && !FacingRight) || (move < 0 && FacingRight)) && reverse == false){
            Flip();
        } else if ( ((move < 0 && !FacingRight) || (move > 0 && FacingRight)) && reverse == true){
            Flip();
        }

        //Move
        m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);
        m_Anim.SetFloat("AbsSpeed", Mathf.Abs(m_Rigidbody2D.velocity.x));

        //Handle jump & differents powers
        HandleJump(move,jump);
        if (Input.GetKey("space"))
            HandlePowerAction(move, jump);
    }

    private void Update() {
        if(Input.GetKeyDown("space")){
            teleport = true;
        }

        //Cap velocity max y
        if(m_Rigidbody2D.velocity.y >= VelocityYCap){
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, VelocityYCap);
        }
        if(m_Rigidbody2D.velocity.y <= -VelocityYCap){
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, -VelocityYCap);
        }
    }

    //On death, reset power and move the player to the respawn location
    public void Death()
    {
        if(respawning == false) {
            respawning = true;
            StartCoroutine(Die());
        } 
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground") && OnRightWall && OnLeftWall && Grounded && Ceiling && this.power.LastPower == "Purple" ){
            Debug.Log("In the wall after a teleport ?");
            Vector3 pos = transform.localPosition;
            Vector3 newPos = new Vector3();
            if (FacingRight){
                newPos = new Vector3(pos.x-0.5f, pos.y, pos.z);
            } else {
                newPos = new Vector3(pos.x+0.5f, pos.y, pos.z);
            }
            transform.localPosition = newPos;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Monster")){
            Death();
        }
    }

    private void Coin(){
        coin++;
    }

    //Teleport the player at the opposite of the map on the axis x
    private void MoveOppositeX(bool right){
        if (col == true){
            if (right){
                this.transform.localPosition = new Vector3(this.transform.localPosition.x-OppositeX, this.transform.localPosition.y, this.transform.localPosition.z);
                col = false;
            } 
            if (!right){
                this.transform.localPosition = new Vector3(this.transform.localPosition.x+OppositeX, this.transform.localPosition.y, this.transform.localPosition.z);
                col = false;
        }
        }
    }
    
    //Teleport the player at the opposite of the map on the axis y 
    private void MoveOppositeY(bool up){
        if (col == true){
            if (up){
                this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y+OppositeY, this.transform.localPosition.z);
                col = false;
            } 
            if (!up){
                this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y-OppositeY, this.transform.localPosition.z);
                col = false;
            }
        }
    }

    //Coroutine called when the player die so play the animation, and teleport him to his checkpoint
    IEnumerator Die()
    {
        death++;
        GetComponent<SpriteRenderer>().enabled = false;
        DeadParticle.Play();
        yield return new WaitForSecondsRealtime(0.5f);
        power.ResetPowers();
        m_Rigidbody2D.velocity = Vector2.zero;
        ResetMovement();
        if (CurrentCheckpoint != null && CurrentCheckpoint.SavedPowers != "")
        {
            power.LastPower = CurrentCheckpoint.SavedPowers;
            SetColor();
        }
        else
        {
            SetColor(false);
        }
        if (reverse)
        {
            GravityReverse();
        }
        transform.localPosition = respawn;
        SpawnParticle.Play();
        yield return new WaitForSecondsRealtime(0.5f);
        GetComponent<SpriteRenderer>().enabled = true;
        respawning = false;
    }

    //Move on axis X the player
    public void MoveX(float move)
    {
        Vector3 vector = transform.localPosition;
        vector.x -= move;
        transform.localPosition = vector;
    }

    public void Win(){
        win = true;
    }

    //When the player pick up a power
    public void AddPower(Power newpower){
        power.AddPower(newpower);
        SetColor();
        Recharge(newpower.GetType().ToString());
    }

    //Get another dash/double jump when the player pick up a power
    public void Recharge(string power){
        switch(power){
            case "Green" : Djump = true; break;
            case "Red" : dash = true; break;
            case "Viridian" : gravityReverse = true; break;
            case "Purple" : teleport = true; break;
            case "Blue" : CurrentFlyFuel = FlyFuel ; break;
            //others powers
        }
    }

    //Set the color of the player depending on his current power
    private void SetColor(bool color = true){
        Renderer rend = gameObject.GetComponent<Renderer>();
        if (!color) {
            rend.material.color = Color.white;
            return;
        }
        switch(power.LastPower){
            case "Green" : rend.material.color = Green; break;
            case "Red" : rend.material.color = Red; break;
            case "Viridian" : rend.material.color = Viridian; break;
            case "Purple" : rend.material.color = Purple; break;
            case "Yellow" : rend.material.color = Yellow; break;
            case "Blue" : rend.material.color = Blue; break;
        }
    }

    //What happen during a dash
    private void HandleDash(){
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
               // ResetMovement();
            }
        }
        m_Anim.SetBool("Dashing",dashing);
    }

    //Check wall on the right of the player
    private void HandleRightCheck(){
        
        Collider2D[] collidersR = Physics2D.OverlapCircleAll(m_RightCheck.position, k_GroundedRadius*2, m_WhatIsGround);
        Collider2D[] collidersRL = Physics2D.OverlapCircleAll(m_RightCheckLow.position, k_GroundedRadius*2, m_WhatIsGround);
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
        for (int i = 0; i < collidersRL.Length; i++)
        {
            if (collidersRL[i].gameObject != gameObject)
            {
                if (FacingRight){
                    OnLeftWall = true;
                } else {
                    OnRightWall = true;
                }
            }
        }
    }

    //Check wall on the left of the player
    private void HandleLeftCheck(){
        Collider2D[] collidersL = Physics2D.OverlapCircleAll(m_LeftCheck.position, k_GroundedRadius*2, m_WhatIsGround);
        Collider2D[] collidersLL = Physics2D.OverlapCircleAll(m_LeftCheckLow.position, k_GroundedRadius*2, m_WhatIsGround);
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
        for (int i = 0; i < collidersLL.Length; i++)
        {
            if (collidersLL[i].gameObject != gameObject)
            {
                if (FacingRight){
                    OnRightWall = true;
                } else {
                    OnLeftWall = true;
                }
            }
        }
    }

    //Check ground under the player
    private void HandleGroundCheck(){
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                Grounded = true;
                CurrentFlyFuel = FlyFuel;
                Djump = true;
                Hjump = true;
                gravityReverse = true;
                dash = true;
            }
        }
        m_Anim.SetBool("Grounded",Grounded);
    }

    private void HandleCeilingCheck(){
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                Ceiling = true;
            }
        }
    }

    //Function used to know if the play should grab, ungrab, or walljump, depending on inputs
    private void HandleGrab(float move, bool jump){
        //Grab if not grounded and there is a wall
        if (OnLeftWall && !Grounded || OnRightWall && !Grounded){
            if (!grabbing && (
            ( ( (OnRightWall && move < 0) || (OnLeftWall && move > 0) ) && !reverse ) 
            || ( ( (OnRightWall && move > 0) || (OnLeftWall && move < 0)) && reverse ) )){
                grabbing = true;
                return;
            }
            if ( (jump || (((FacingRight && move < 0) || (!FacingRight && move > 0)) && !reverse) ) && ((Djump && power.HavePower("Green")) || power.HavePower("Yellow")) && grabbing  ){
                Grounded = false;
                Djump = false;
                grabbing = false;
                WallJump(FacingRight);
                return;
            }
        }
        //Ungrab if player move/jump, or if there is no wall/the ground
        if(grabbing && (Grounded ||  (!FacingRight && move > 0 && !reverse) || (FacingRight && move < 0 && !reverse) 
        || (!FacingRight && move < 0 && reverse) || (FacingRight && move > 0 && reverse) || !OnLeftWall && FacingRight || !OnRightWall && !FacingRight  )){
            grabbing = false;
        }
    }

    //Function called when the player press jump
    private void HandleJump(float move, bool jump){
        //High jump if jump is still down when at the max velocity of the low jump
        if ( !Grounded && !jump && Input.GetButton("Jump") && m_Rigidbody2D.velocity.y > 16 && m_Rigidbody2D.velocity.y < 18 && Hjump && Djump && !grabbing){
            Hjump = false;
            Jump(m_JumpForce*HighJumpFactor);
        }
        //Low jump when press jump
        else if ( Grounded && jump && !dashing && !power.HavePower("Viridian")  && !grabbing){
            Jump(m_JumpForce*LowJumpFactor);
        }
        //Double jump when press jump in air
        else if ( !Grounded && (Input.GetKey("space") || jump) && power.HavePower("Green") && Djump && !dashing  && !grabbing){
            Jump(m_JumpForce);
            Djump = false;
        }
    }

    //Function called when the player press space to use his power
    //Use a power according to what is the power saved
    private void HandlePowerAction(float move, bool jump){
        switch(this.power.LastPower){
            case "Red" : 
                //Dash
                if (!Grounded && dash && !grabbing ){
                    Dash(move);
                }
                break;
            case "Viridian" : 
                //Gravity reverse
                if (gravityReverse){
                    GravityReverse();
                    gravityReverse = false; 
                }
                break;
            case "Purple" : 
                //Teleport
                if (teleport){
                    Teleport();
                }
                break;
            case "Blue" : 
                //Teleport
                if (!Grounded && m_Rigidbody2D.velocity.y < FlySpeedCap && CurrentFlyFuel > 0){
                    if(m_Rigidbody2D.velocity.y <= 0) ResetMovement();
                    Fly(FlyForce);
                }
                break;
            //case "Yellow" : ...
            default : return;
        }
    }

    public void Fly(float force)
    {
        m_Anim.SetBool("Grounded", false);
        CurrentFlyFuel = CurrentFlyFuel - 1;
        m_Rigidbody2D.AddForce(new Vector2(0f, force));
    }

    //Open a door if the player own a key
    public void Open(GameObject Door){
        if(keys > 0){
            Key.keyUsed.Invoke(Door.transform);
            keys--;
            Keyblock.KeyBlockEvent.Invoke(Door.transform.position);
            Destroy(Door, 0.3f);
            
        }
    }

    public void IncreaseKeys(){
        keys++;
    }

    //Change the position of the player on the x axis
    public void Teleport(){
        teleport = false;
        Vector3 pos = transform.localPosition;
        Vector3 newPos = new Vector3();
        if (FacingRight){
            newPos = new Vector3(pos.x+2.5f, pos.y, pos.z);
        } else {
            newPos = new Vector3(pos.x-2.5f, pos.y, pos.z);
        }
        transform.localPosition = newPos;
    }

    //Action grab
    private void Grab(){
        m_Rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        if (m_Rigidbody2D.velocity.y > 0){
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_Rigidbody2D.velocity.y * GrabUpFactor);
        }else {
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_Rigidbody2D.velocity.y * GrabDownFactor);
        }
    }

    //Action to end the grab
    private void Ungrab(){
        m_Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    //Push the player on the axis Y
    public void MoveY(float move)
    {
        Vector3 vector = transform.localPosition;
        vector.y -= move;
        transform.localPosition = vector;
    }

    //Wall jump used when the player in on grab
    private void WallJump(bool FacingRight)
    {
        Ungrab();
        ResetMovement();
        if (FacingRight){
            m_Rigidbody2D.AddForce(new Vector2(-WallJumpPushForce, m_JumpForce));
        } else {
            m_Rigidbody2D.AddForce(new Vector2(WallJumpPushForce, m_JumpForce));
        }
        WalljumpTimer = 0.2f;
    }

    //Push the player up on y axis
    public void Jump(float force)
    {
        m_Anim.SetBool("Grounded", false);
        Grounded = false;
        ResetMovement();
        m_Rigidbody2D.AddForce(new Vector2(0f, force));
    }

    //Enable the dash for a short period, making the player moving fast
    private void Dash(float move){
        dash = false; 
        ResetMovement();
        dashTimer = DashTime;
        dashing = true;
    }

    //Push the player on x or y
    public void Push((float, float) xy)
    {
        m_Rigidbody2D.AddForce(new Vector2(xy.Item1, xy.Item2));
    }

    //Set the respawn location & checkpoint saved power when the player walk on a new checkpoint
    public void SetRespawn(Checkpoint checkpoint){
        //Disable old checkpoint
        if (CurrentCheckpoint != null){
            CurrentCheckpoint.Activation(false);
        }
        //Set new checkpoint or new color of same checkpoint
        if (power.LastPower != null){
            checkpoint.SavedPowers =  (string)power.LastPower.Clone();
        }
        StartCoroutine(Waiting(0.2f, checkpoint));

        //Set the respawn point of the player
        respawn = checkpoint.gameObject.transform.position;
        CurrentCheckpoint = checkpoint;
    }

    IEnumerator Waiting(float s, Checkpoint ch){
        ch.Activation(false);
        yield return new WaitForSecondsRealtime(s);
        ch.Activation(true);
    }

    //Flip the player sprite
    private void Flip()
    {
        FacingRight = !FacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    //Power to reverse the gravity affecting the player
    public void GravityReverse(){
        FacingRight = !FacingRight;
        Vector3 theScale = transform.localScale;
        gravity *= -1;
        theScale.y *= -1;
        transform.localScale = theScale;
        reverse = !reverse;
        ResetMovement();
    }

    //Reset the velocity of the rigidbody to stop the player movement
    public void ResetMovement(){
        m_Rigidbody2D.velocity = new Vector2(0f, 0f);
    }
}
