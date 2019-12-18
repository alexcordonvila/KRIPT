using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerBehaviour : MonoBehaviour
{
    public LayerMask whatIsGround;
 
    Rigidbody2D rb;
    [Range(1,20)]
    [Tooltip("x movement speed value")]
    public float speed;
    [SerializeField]
    [Tooltip("Ground flag by 2 down Ray Cast")]
    private bool isGrounded;
    [SerializeField]
    [Tooltip("Ground flag by Box collider 2D")]
    private bool isCollisionGrounded; 
    [SerializeField]
    public int numMovements;
    public float soundlimit;
    private float x,y;
    private Vector2 dir;
    private Vector3 footChecker;
    [SerializeField]
    private bool wall_R, wall_L;
    //Check if the player has jumped
    private bool JumpEvent;
    //Check if the player is falling
    private bool FallEvent;
    //Check if the player is falling of an edge
    private bool EdgeFallEvent;
    private bool dashEvent;
    [Range(1, 3)]
    [Tooltip("If player is not on the ground speed is divides by onAirMovementLimit")]
    public float onAirMovementLimit = 1.5f;
    [Range(3, 15)]
    [Tooltip("Fall force when player is pressing jump button")]
    public float fallMultiplayer = 2.5f;
    [Range(1, 15)]
    [Tooltip("Fall force when player release jump button")]
    public float lowJumpMultiplayer = 2f;
    [Range(1, 10)]
    [Tooltip("Jump force")]
    public float jumpVelocity;
    public GameObject rayOrigin;
    [Range(0, 1)]
    public float rayCheckDistance;
    [Range(0, 1)]
    public float rayCheckDistance_two;
    public RaycastHit2D hit_wall_R;
    public RaycastHit2D hit_wall_L;

    //AIRTIME VARIABLES
    float minutes = 0;
    float seconds = 4;
    float miliseconds = 0;

    // Start is called before the first frame update
    void Start()
    {
        JumpEvent = false;
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02F;
        numMovements = 0;
        wall_R = false;
        wall_L = false;
      
        
        rayCheckDistance_two = 0.45f;
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        dir = new Vector2(x,y);
        AirTimeCountDown();
        CharacterRayCast();
        Restart();
        if (Input.GetButtonDown("Jump"))
        {
            JumpEvent = true;
        }
            if (rb.velocity.y == 0) { 
        }
        if (Input.GetButton("Dash"))
        {
            dashEvent = true;
            Dash(x,y);
        }
        if (Input.GetButtonUp("Dash")) {

            dashEvent = false;
        }
    }

    void FixedUpdate()
    {
        Move(dir);
        Jump();
        SloWMotionMovement();

        if (rb.velocity.y < -15) SloWMotionMovement_Vel();

        if (rb.velocity.y < -13)
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Platforms")
        {
            GameObject cam = GameObject.Find("Main Camera");
            iTween.ShakePosition(cam, iTween.Hash("x", 0.1f, "y", 0.1f, "time", 0.1f));
            isGrounded = true;
           // isCollisionGrounded = true;
            //   SoundManager.PlaySound(SoundManager.Sound.playerLand);
        }
        if (col.gameObject.tag == "MovingPlatforms")
        {
            GameObject platform = GameObject.Find("MovingPlatforms");
            this.transform.parent = platform.transform;
        }else
        {
            this.transform.parent = null;
        }
        if (col.gameObject.name == "Door")
        {
            GameObject cam = GameObject.Find("Main Camera");
            Application.LoadLevel(Application.loadedLevel);
         //   SoundManager.PlaySound(SoundManager.Sound.playerLand);
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "MovingPlatforms")
        {
            this.transform.parent = null;
        }
        if (col.gameObject.name == "Platforms_Tilemap")
        {
            //GameObject cam = GameObject.Find("Main Camera");
            //iTween.ShakePosition(cam, iTween.Hash("x", 0.1f, "y", 0.1f, "time", 0.1f));
            isGrounded = false;
        //    isCollisionGrounded = false;
            //   SoundManager.PlaySound(SoundManager.Sound.playerLand);
        }
    }
    private void Move(Vector2 dir)
    {
        Vector2 move = new Vector2(dir.x * speed, rb.velocity.y);
        if (dashEvent) {
             move = new Vector2(rb.velocity.x, rb.velocity.y);
        }
         
        if (!isGrounded)
        {
        //    move = new Vector3(x * speed / onAirMovementLimit, rb.velocity.y, 0f);
        }
        rb.velocity = move;
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
    private void SloWMotionMovement_Vel()
    {
            Time.timeScale = 0.1f;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
    }
    private void Jump()
    {
        
        if (JumpEvent)
        {
            if (isGrounded /*|| isCollisionGrounded*/) {
                //rb.velocity = new Vector2(rb.velocity.x, 0f);
                //rb.velocity = Vector2.up * jumpVelocity ;
                rb.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
           // SoundManager.PlaySound(SoundManager.Sound.playerJump);
            numMovements++;
           }
            JumpEvent = false;
        }
        if (rb.velocity.y < 0)
        {
            //rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplayer - 1) * Time.deltaTime;
            rb.gravityScale = fallMultiplayer;
            FallEvent = true;
        }
      
        else if (rb.velocity.y > 0  && !Input.GetButton("Jump"))
        {
            Debug.Log("ButtonReleased");
            //rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplayer - 1) * Time.deltaTime;
            rb.gravityScale = lowJumpMultiplayer;
        }
        else{
            rb.gravityScale = 1f;
        }
    }
    private void Dash(float x, float y)
    {
        rb.velocity = Vector2.zero;
        rb.velocity += new Vector2(x, y).normalized * 10;
    }
    private void CharacterRayCast()
    {
        float foot = 0.3f;
        
        RaycastHit2D hit_down = Physics2D.Raycast(rayOrigin.transform.position, Vector2.down, rayCheckDistance_two);
        RaycastHit2D hit_downR = Physics2D.Raycast(rayOrigin.transform.position + new Vector3(foot, 0, 0), Vector2.down, rayCheckDistance_two);
        RaycastHit2D hit_downL = Physics2D.Raycast(rayOrigin.transform.position - new Vector3(foot, 0 ,0), Vector2.down, rayCheckDistance_two);
        hit_wall_R = Physics2D.Raycast(transform.position + new Vector3(0, 0.3f, 0), Vector2.right , rayCheckDistance);
        hit_wall_L = Physics2D.Raycast(transform.position + new Vector3(0, 0.3f, 0), Vector2.left , rayCheckDistance);
        if ( hit_wall_R.collider != null ) {  wall_R = true;  } else { wall_R = false; }
        if (hit_wall_L.collider != null) { wall_L = true; } else { wall_L= false; }
        if (hit_down.collider != null || hit_downR.collider != null || hit_downL.collider != null) { isGrounded = true; } else { isGrounded = false; }

        Debug.Log(hit_downL.collider);
        Debug.DrawRay(transform.position , Vector2.down * rayCheckDistance_two, Color.red);
        Debug.DrawRay(transform.position - new Vector3(foot, 0, 0), Vector2.down * rayCheckDistance_two, Color.red);
        Debug.DrawRay(transform.position + new Vector3(foot, 0, 0), Vector2.down * rayCheckDistance_two, Color.red);
        Debug.DrawRay(transform.position + new Vector3(0, 0.3f, 0), Vector2.right * rayCheckDistance, Color.green);
        Debug.DrawRay(transform.position + new Vector3(0, 0.3f, 0), Vector2.left * rayCheckDistance, Color.green);
    }
    private void Restart()
    {
        if (Input.GetButtonDown("Restart"))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
    void AirTimeCountDown()
    {
        if (!isGrounded /*&& !isCollisionGrounded*/)
        {
            //if (JumpEvent)
            //{
                if (miliseconds <= 0)
                {
                    if (seconds <= 0)
                    {
                        minutes--;
                        seconds = 59;
                    }
                    else if (seconds >= 0)
                    {
                        seconds--;
                   
                    }

                    miliseconds = 100;
                }

                miliseconds -= Time.deltaTime * 100;

            if (seconds<=0 && miliseconds<=0)
            {
                Application.LoadLevel(Application.loadedLevel);

            }
            
            TextMeshProUGUI textmeshPro = this.gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            //textmeshPro.SetText(seconds.ToString()+""+ miliseconds.ToString());
            textmeshPro.SetText("{0}."+ miliseconds.ToString("F0"), seconds );
            Debug.Log(this.gameObject.transform.GetChild(0).transform.GetChild(0).name);
            // timer.text = string.Format("{0}:{1}:{2}", minutes, seconds, (int)miliseconds);
            
        }
            if (FallEvent)
            {

            }
    }
    void AirTimeCounter()
    {
        if (!isGrounded)
        {
            //if (JumpEvent)
            //{
            if (miliseconds >= 60)
            {
                if (seconds >= 60)
                {
                    minutes++;
                    seconds = 0;
                }
                else if (seconds <= 60)
                {
                    seconds++;
                }

                miliseconds = 100;
            }

            miliseconds -= Time.fixedDeltaTime * 100;
            

            Debug.Log(string.Format("{0}:{1}:{2}", minutes, seconds, (int)miliseconds));
            
            
            // timer.text = string.Format("{0}:{1}:{2}", minutes, seconds, (int)miliseconds);
            //}
        }
        if (FallEvent)
        {

        }
    }

}
