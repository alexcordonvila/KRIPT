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
      
        rayCheckDistance = 0.5f;
        rayCheckDistance_two = 3f;
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = Vector2.up * jumpVelocity;
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

    void FixedUpdate()
    {
        
        RaycastHit2D hit_down = Physics2D.Raycast(rayOrigin.transform.position, Vector2.down, rayCheckDistance);
        hit_wall_R = Physics2D.Raycast(transform.position, Vector2.right * 3f, rayCheckDistance);
        hit_wall_L = Physics2D.Raycast(transform.position, Vector2.left * 3f, rayCheckDistance);
        if (hit_down.collider != null || hit_wall_R.collider != null || hit_wall_L.collider != null) { isGrounded = true; } else { isGrounded = false; }
 
        Debug.DrawRay(transform.position, Vector2.down * rayCheckDistance, Color.red);
        Debug.DrawRay(transform.position, Vector2.right * rayCheckDistance, Color.green);
        Debug.DrawRay(transform.position, Vector2.left  * rayCheckDistance, Color.green);
        

        float x = Input.GetAxis("Horizontal");
        Vector3 move = new Vector3( x * speed * Time.fixedDeltaTime, rb.velocity.y, 0f);
        if (!isGrounded) {
           // move = new Vector3(x * speed/1.8f * Time.fixedDeltaTime, rb.velocity.y, 0f); 
        }
        if ( hit_wall_R.collider != null)
        {
            wall_R = true;
            if (x >0)
            {
               
              //  move = new Vector3(0, rb.velocity.y, 0f);
            }
        }else
        {
            wall_R = false;
        }
        if (hit_wall_L.collider != null)
        {
            wall_L = true;
            if (x < 0)
            {
                

             //   move = new Vector3(0, rb.velocity.y, 0f);
            }
        }
        else
        {
            wall_L = false;
        }

        rb.velocity = move;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(col.gameObject.name);
        if (col.gameObject.name == "Platforms")
        {
           // SoundManager.PlaySound(SoundManager.Sound.playerLand);
        }
    }
}
