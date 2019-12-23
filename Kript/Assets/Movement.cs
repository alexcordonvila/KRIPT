using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [HideInInspector]
    private Collision coll;
    public Rigidbody2D rb;

    [Space]
    [Header("Stats")]
    [Range(1, 20)]
    [Tooltip("x movement speed value")]
    public float speed = 10;
    [Range(1, 10)]
    [Tooltip("Upside jump force")]
    public float jumpForce = 6;
    public float slideSpeed = 5;
    public float wallJumpLerp = 10;
    public float dashSpeed = 20;
    public float x, y, xRaw, yRaw;
    [Range(2, 5)]
    [Tooltip("Fall force when player is pressing jump button")]
    public float fallMultiplayer = 2.5f;
    [Range(1, 5)]
    [Tooltip("Fall force when player release jump button")]
    public float lowJumpMultiplayer = 2f;

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

    [Space]
    private bool hasDashed;
    private int side = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collision>();
    }

    // Update is called once per frame
    void Update()
    {
        this.x = Input.GetAxis("Horizontal");
        this.y = Input.GetAxis("Vertical");
        this.xRaw = Input.GetAxisRaw("Horizontal");
        this.yRaw = Input.GetAxisRaw("Vertical");
        //floor resets variables:
        if (coll.onGround)
        {
            wallJumped = false;
            canMove = true;
            groundTouch = true;
            jumpForce = 6;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (coll.onGround)
            {
                canMove = true;
                JumpEvent = true;
            }
            if (coll.onWall && !coll.onGround && !wallJumped)
            {         
                WallJump();
            }
        }

        if (coll.onWall && !coll.onGround && !wallJumped)
        {
            canMove = false;
        }
        if (!coll.onGround && groundTouch) //si estoy en el aire
        {
            groundTouch = false;
        }
        if (groundTouch && coll.onWall)
        {
            if (side != coll.wallSide && Input.GetAxis("Horizontal")>0)
            {
                this.x = 0;
                Debug.Log("Paret de la dreta");
            }else
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

    }
    void FixedUpdate()
    {
        Vector2 dir = new Vector2(x, y);
        Walk(dir);
        Jump(Vector2.up, false);
        SloWMotionMovement();
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.gameObject.tag == "Platforms" && isFalling)
        {
            GameObject cam = GameObject.Find("Main Camera");
            iTween.ShakePosition(cam, iTween.Hash("x", 0.1f, "y", 0.1f, "time", 0.1f));
            isFalling = false;
        }
        if (col.gameObject.tag == "MovingPlatforms")
        {
            GameObject cam = GameObject.Find("Main Camera");
            GameObject platform = GameObject.Find("MovingPlatforms");
            iTween.ShakePosition(cam, iTween.Hash("x", 0.1f, "y", 0.1f, "time", 0.1f));
            isFalling = false;
            this.transform.parent = platform.transform;
        }
        else
        {
            this.transform.parent = null;
        }
    }

    private void Walk(Vector2 dir)
    {

        Vector2 move;
        move = new Vector2(dir.x * speed, rb.velocity.y);

        if (!canMove)
            return;

        if (wallGrab)
         //  return;

        if (!wallJumped)
        {
            move = new Vector2(dir.x * speed, rb.velocity.y);
        }
        else
        {
           // move = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * speed, rb.velocity.y)), wallJumpLerp * Time.deltaTime);
        }
        rb.velocity = move;
    }
    private void Jump(Vector2 dir, bool wall)
    {
        
        if (JumpEvent)
        {
            if (groundTouch || wallJumped)
            {
                rb.AddForce(dir* jumpForce, ForceMode2D.Impulse);
            }
            JumpEvent = false;
        }
        if (rb.velocity.y< 0)
        {
            isFalling = true;
            rb.gravityScale = fallMultiplayer;
        }
        else if (rb.velocity.y > 0  && !Input.GetButton("Jump"))
        {
            rb.gravityScale = lowJumpMultiplayer;
        }
        else{
            rb.gravityScale = 1f;
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
       // Jump((Vector2.up * 1.5f + wallDir.normalized), true);
        
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
}
