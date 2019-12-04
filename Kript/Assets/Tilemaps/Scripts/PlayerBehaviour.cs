using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public LayerMask whatIsGround;
 
    Rigidbody2D rb;
    public float speed;
    public bool isGrounded;
    public int numMovements;
    public float soundlimit;


    public bool wall_R;
    public bool wall_L;
    public float fallMultiplayer = 2.5f;
    public float lowJumpMultiplayer = 2f;
    public float jumpVelocity;
    public GameObject rayOrigin;
    public float rayCheckDistance;
    public float rayCheckDistance_two;
    public RaycastHit2D hit_wall_R;
    public RaycastHit2D hit_wall_L;
    // Start is called before the first frame update
    void Start()
    {
        numMovements = 0;
        wall_R = false;
        wall_L = false;
      
        
        rayCheckDistance_two = 0.45f;
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        float foot = 0.2f;
        Restart();
        RaycastHit2D hit_down = Physics2D.Raycast(rayOrigin.transform.position, Vector2.down, rayCheckDistance_two);
        RaycastHit2D hit_downR = Physics2D.Raycast(rayOrigin.transform.position + new Vector3(foot, 0,0), Vector2.down, rayCheckDistance_two);
        RaycastHit2D hit_downL = Physics2D.Raycast(rayOrigin.transform.position - new Vector3(foot, 0, 0), Vector2.down, rayCheckDistance_two);
        hit_wall_R = Physics2D.Raycast(transform.position, Vector2.right * 3f, rayCheckDistance);
        hit_wall_L = Physics2D.Raycast(transform.position, Vector2.left * 3f, rayCheckDistance);
        // if (hit_down.collider != null || hit_wall_R.collider != null || hit_wall_L.collider != null) { isGrounded = true; } else { isGrounded = false; }
        if (hit_downR.collider != null || hit_downL.collider != null || hit_down.collider != null) { isGrounded = true; } else { isGrounded = false; }

        Debug.DrawRay(transform.position + new Vector3(foot, 0, 0), Vector2.down * rayCheckDistance_two, Color.red);
        Debug.DrawRay(transform.position - new Vector3(foot, 0, 0), Vector2.down * rayCheckDistance_two, Color.red);
        Debug.DrawRay(transform.position, Vector2.down * rayCheckDistance_two, Color.red);
        Debug.DrawRay(transform.position, Vector2.right * rayCheckDistance, Color.green);
        Debug.DrawRay(transform.position, Vector2.left * rayCheckDistance, Color.green);


    }

    void FixedUpdate()
    {
        Jump();
        SloWMotionMovement();
       

        float x = Input.GetAxis("Horizontal");
        Vector3 move = new Vector3( x * speed , rb.velocity.y, 0f);
        if (!isGrounded)
        {
             move = new Vector3(x * speed/1.3f , rb.velocity.y, 0f); 
        }
        //if (hit_wall_R.collider != null)
        //{
        //    wall_R = true;
        //    if (x > 0)
        //    {
        //           move = new Vector3(0, rb.velocity.y, 0f);
        //    }
        //}
        //else
        //{
        //    wall_R = false;
        //}
        //if (hit_wall_L.collider != null)
        //{
        //    wall_L = true;
        //    if (x < 0)
        //    {
        //          move = new Vector3(0, rb.velocity.y, 0f);
        //    }
        //}
        //else
        //{
        //    wall_L = false;
        //}
      //  Debug.Log(rb.velocity.y);
        if (rb.velocity.y < -30 )
        {
            Application.LoadLevel(Application.loadedLevel);
        }
        rb.velocity = move;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
       
        if (col.gameObject.name == "Platforms_Tilemap")
        {
            GameObject cam = GameObject.Find("Main Camera");
            iTween.ShakePosition(cam, iTween.Hash("x", 0.1f, "y", 0.1f, "time", 0.1f));
         //   SoundManager.PlaySound(SoundManager.Sound.playerLand);

        }
        if (col.gameObject.name == "Door")
        {
            GameObject cam = GameObject.Find("Main Camera");
            Application.LoadLevel(Application.loadedLevel);
         //   SoundManager.PlaySound(SoundManager.Sound.playerLand);
        }
    }
    void SloWMotionMovement()
    {
        Debug.Log(Input.GetAxisRaw("Fire1"));
        if (Input.GetAxis("Fire1") != 0)
        {
            Debug.Log("Fire1" + Input.GetAxis("Fire1"));
            Time.timeScale = 0.5f;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
        }
        else
        {
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02F;
        }

    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = Vector2.up * jumpVelocity ;
           // SoundManager.PlaySound(SoundManager.Sound.playerJump);
            numMovements++;

        }
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics.gravity.y * (fallMultiplayer - 1) * Time.fixedDeltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButtonDown("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplayer - 1) * Time.fixedDeltaTime;

        }
    }
    void DebugPosition()
    {

    }
    void Restart()
    {
        if (Input.GetButtonDown("Restart"))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
