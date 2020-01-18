using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    [HideInInspector]
    private Collision coll;
    private Rigidbody2D rb;

    [Space]
    [Header("Stats")]
    [Range(1, 20)]
    [Tooltip("x movement speed value")]
    public float speed = 10;
    [Range(1, 15)]
    [Tooltip("Upside jump force")]
    public float jumpForce = 12f;
    public float slideSpeed = 5;
    public float wallJumpLerp = 10;
    public float dashSpeed = 1;
    public float x, y, xRaw, yRaw;
    public float dashRadius = 1.5f;
    [Range(2, 5)]
    [Tooltip("Fall force when player is pressing jump button")]
    public float fallMultiplayer = 2.5f;
    [Range(1, 5)]
    [Tooltip("Fall force when player release jump button")]
    public float lowJumpMultiplayer = 2f;
    public int numMovements;
    [Space]
    [Header("Booleans")]
    public bool canMove;
    public bool wallGrab;
    public bool wallJumped;
    public bool wallSlide;
    public bool isDashing;
    public bool groundTouch;
    public bool isFalling;
    public bool JumpEvent;
    public bool DashEvent;

    [Space]
    private bool hasDashed;
    private int side = 1;
    private Vector2 SpawnPoint;
    private Animator anim;

    void Start()
    {
        SpawnPoint = new Vector2(this.transform.position.x, this.transform.position.y);
        lowJumpMultiplayer = 2f;
        fallMultiplayer = 2.5f;
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collision>();
        numMovements = 0;
        hasDashed = false;
        rb.drag = 0.3f;
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        anim.SetBool("Falling", isFalling);
        Restart();
        this.x = Input.GetAxis("Horizontal");
        this.y = Input.GetAxis("Vertical");
        this.xRaw = Input.GetAxisRaw("Horizontal");
        this.yRaw = Input.GetAxisRaw("Vertical");
        if ((x > 0 || x < 0) && y == 0)
        {
            anim.speed = x * x;
        }else
        {
            anim.speed = 1;
        }
        //floor resets variables:
        if (coll.onGround)
        {
            wallJumped = false;
            canMove = true;
            groundTouch = true;
            anim.SetBool("Jump", false);

        }
        if (coll.onDead)
        {
            Dead();
        }
        if (Input.GetButtonDown("Jump"))
        {
           
            if (coll.onGround)
            {
                iTween.Stop(this.gameObject);
                canMove = true;
                JumpEvent = true;
                GroundTouch();
                //  anim.SetBool("Jump", true);
                DashEvent = true;
            }
            if (coll.onWall && !coll.onGround && !wallJumped)
            {         
                //WallJump();
            }
        }
        if (coll.onCeiling)
        {
            iTween.Stop(this.gameObject);
            Restart();
        }
        if (coll.onWall && yRaw!=1)
        {
           // iTween.Stop(this.gameObject);

        }

        if(coll.onWall && isDashing && yRaw != 1) iTween.Stop(this.gameObject);
        if (isDashing)
        {
         
        }
        else if (!isDashing && !coll.onGround && !isFalling)
        {
            anim.SetBool("Jump", true);
        }
        else
        {

        }
        if ((coll.onGround || !isDashing) && coll.onWall) 
        {  
  
        }
        if (coll.onWall && !coll.onGround && !wallJumped) //En pared, en el aire y sin pulsar espacio
        {
            if (side != coll.wallSide && Input.GetAxis("Horizontal") > 0)
            {
                this.x = 0;
                Debug.Log("En aire y pared dcha");
            }
            else
            {
                this.x = 0;
                Debug.Log("En aire y pared izda");
            }
            if (side != coll.wallSide && Input.GetAxis("Horizontal") <= 0)
            {
                this.x = Input.GetAxis("Horizontal");
            }
            if (side == coll.wallSide && Input.GetAxis("Horizontal") >= 0)
            {
                this.x = Input.GetAxis("Horizontal");
            }

        }
        else if(coll.onWall && !coll.onGround) //No pared, en el aire y sin pulsar espacio y si movimiento
        {  
            canMove = true;
        }
        if (!coll.onGround && groundTouch) //si estoy en el aire
        {
            groundTouch = false;
            
        }
        if (coll.onGround && !groundTouch) //si estoy colisionando con el suelo pero groundTouch aun no esta a false
        {
           GroundTouch();
           groundTouch = true; 

        }
        if (groundTouch && coll.onWall) //si estoy en el suelo y toco pared
        {
            if (side != coll.wallSide && Input.GetAxis("Horizontal") > 0)
            {
                this.x = 0;
                Debug.Log("Paret de la dreta");
            }
            else
            {
                this.x = 0;
                Debug.Log("Paret de l'esquerra");
            }
            if (side != coll.wallSide && Input.GetAxis("Horizontal") <= 0)
            {
                this.x = Input.GetAxis("Horizontal");
            }
            if (side == coll.wallSide && Input.GetAxis("Horizontal") >= 0)
            {
                this.x = Input.GetAxis("Horizontal");
            }
        }
        if (Input.GetButtonDown("Fire1") && !hasDashed)
        {         
            if ((xRaw != 0 || yRaw != 0) && DashEvent)
            {
                if (!coll.onGround)
                {
                    Dash(xRaw, yRaw);
                }
                else if (coll.onGround)
                {
                    Dash(xRaw, 0);
                }
            } 
        }
    }
    void FixedUpdate()
    {
        Vector2 dir = new Vector2(x, y);
        Walk(dir);
        if (xRaw < 0) { 
        this.transform.localScale = new Vector3(-1, 1, 1);
        anim.SetBool("Run", xRaw < 0);
        }
        else if(xRaw>0)
        {
            //anim.SetBool("Run", xRaw > 0);
            this.transform.localScale = new Vector3(1, 1, 1);
        }
          anim.SetBool("Run", Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0);
        
        Jump(Vector2.up, false);
        SloWMotionMovement();
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.gameObject.tag == "Platforms" && isFalling)
        {
            GameObject cam = GameObject.Find("Main Camera");
           // iTween.ShakePosition(cam, iTween.Hash("x", 0.1f, "y", 0.1f, "time", 0.1f));
            isFalling = false;
            iTween.Stop(this.gameObject);
            canMove = true;
        }
        if (col.gameObject.tag == "MovingPlatforms")
        {
            GameObject cam = GameObject.Find("Main Camera");
            GameObject platform = GameObject.Find("MovingPlatforms");
          //  iTween.ShakePosition(cam, iTween.Hash("x", 0.1f, "y", 0.1f, "time", 0.1f));
            isFalling = false;
            canMove = true;
            this.transform.parent = platform.transform;
        }
        else
        {
           // this.transform.parent = null;
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        this.transform.parent = null;
    }
    private void GroundTouch()
    {
        hasDashed = false;
        isDashing = false;
        isFalling = false;
        //Activamos particulas del jump


    }
    private void Walk(Vector2 dir)
    {

        Vector2 move;
        move = new Vector2(dir.x * speed, rb.velocity.y );
       
        if (!canMove) { 
           // return;
        }else { 
        //if (wallGrab)
        //   return;

        if (!wallJumped && !isDashing)
        {
          //  move = new Vector2(dir.x * speed, rb.velocity.y);
        }
        else
        {
           // move = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * speed, rb.velocity.y)), wallJumpLerp * Time.deltaTime);
        }
        if (isDashing || hasDashed)
        {
            //move = new Vector2(dir.x * speed, dir.y * speed);
        }
        }
        rb.velocity = move;
    }
    private void Jump(Vector2 dir, bool wall)
    {
        if (JumpEvent)
        {
            if (groundTouch || wallJumped)
            {
                numMovements++;
                rb.AddForce(dir * jumpForce, ForceMode2D.Impulse);
                
            }
            //Devolvemos la variable de control de salto a falso
            JumpEvent = false;
            
        }
            if (rb.velocity.y < 0 )
            {
                //isFalling = true;
            anim.SetBool("Jump", false);
            anim.SetBool("Falling", isFalling);
                rb.gravityScale = fallMultiplayer;
            }
            else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                rb.gravityScale += lowJumpMultiplayer;
            }   
    }

    IEnumerator DisableMovement(float time)
    {
        canMove = false;
        Debug.Log("SpaceReleased");
        yield return new WaitForSeconds(time);
        canMove = true;    
    }

    private void WallJump()
    {
       wallJumped = true;
       StopCoroutine(DisableMovement(0));
       StartCoroutine(DisableMovement(3f));
       JumpEvent = true;
       Vector2 wallDir = coll.onRightWall ? Vector2.left : Vector2.right;
       Jump((Vector2.up / 1.5f + wallDir / 2f), true);        
    }
    private void Dash(float x, float y)
    {
        DashEvent = false;
        canMove = false;
        Debug.Log("DasH!");
        hasDashed = true;
        isDashing = true;
        rb.gravityScale = 3;
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(x, y);
        Debug.DrawRay(transform.position, dir * 7, Color.green);
        float posX = this.transform.position.x;
        float posY = this.transform.position.y;
        float endPosX = posX + (dashRadius * dir.x) ;
        float endPosY = posY + (dashRadius * dir.y);
        rb.velocity = dir.normalized * dashSpeed;
        
        iTween.MoveTo(this.gameObject, iTween.Hash("x", endPosX, "y", endPosY,
                   "islocal", true, "easetype", iTween.EaseType.easeOutCubic,
                  "time", 0.15f));
        StartCoroutine(DashWait());
    }
    IEnumerator DashWait()
    {
        StartCoroutine(GroundDash());  
        wallJumped = true;
        isDashing = true;
        isDashing = true;
        yield return new WaitForSeconds(0.1f);
        canMove = false;
        rb.gravityScale = 3;
        wallJumped = false;
        isDashing = false;
        hasDashed = false;
    }
    IEnumerator GroundDash()
    {
        yield return new WaitForSeconds(.15f);
        if (coll.onGround)
            hasDashed = false;
            isDashing = false;
    }
    private void SloWMotionMovement()
    {
        if (Input.GetAxis("Fire1") != 0)
        {
            Time.timeScale = 0.5f;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
        }
        else
        {
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02F;
        }
    }
    private void Dead()
    {
        //Before die we want to play an animation
        canMove = false;
        float posX = this.transform.position.x;
        float posY = this.transform.position.y;
        float endPosX = posX + (dashRadius * 0);
        float endPosY = posY + (dashRadius * 1);
        iTween.MoveTo(this.gameObject, iTween.Hash("y", endPosY,
                    "islocal", true, "easetype", iTween.EaseType.linear,
                   "time", 0.1f, "oncomplete", "DeadRestart", "oncompletetarget",this.gameObject));
        rb.drag = 100;
        //PlayDeadAnimation
        //PlayFadeScreenAnimation



    }
    //This is for the player input restart
    private void Restart()
    {
        if (Input.GetButtonDown("Restart"))
        {
            //Application.LoadLevel(Application.loadedLevel);
            DeadRestart();
        }
    }
    private void DeadRestart()
    {
        GetComponent<Linker>().ResetUI();
        /*SceneManager.LoadScene("Transition", LoadSceneMode.Additive);
        StartCoroutine(LoadYourAsyncScene());*/

        restartLevel();

    }
    IEnumerator LoadYourAsyncScene()
    {
        yield return new WaitForSeconds(0.3f);
        restartLevel();
    }
    public void restartLevel()
    {

        GetComponent<Linker>().ResetUI();

        //Respawn the player
        this.transform.position = SpawnPoint;
        canMove = true;
        //Reset the usual gravity game
        lowJumpMultiplayer = 2f;
        fallMultiplayer = 2.5f;
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collision>();
        numMovements = 0;
        hasDashed = false;
        rb.drag = 0.3f;

    }
    //This function recieve messages to call methods after animations
    public void AlertObservers(string message)
    {
        if (message.Equals("DeadAnimationEnd"))
        {
            
        }
    }
}
