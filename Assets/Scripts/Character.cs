using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;
    public float speed;
    public float jumpHeight;
    private float dirX;
    private bool facingRight = true;
    private Vector3 localScale;
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public LayerMask whatIsWall;
    private bool isSliding;
    public Transform wallCheck;
    public float wallJump;
    public float wallKick;
    public float maxSlidingSpeed;

    public int extraJumpsValue;
    private int extraJumps;

    public float dashSpeed;
    private float dashTime;
    public float startDashTime;


    //metaferw se game manager
    //public bool gameOver = false;
    //

    // Start is called before the first frame update
    void Start()
    {
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        localScale = transform.localScale;

        dashTime = startDashTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded)
            extraJumps = extraJumpsValue;

        dirX = CrossPlatformInputManager.GetAxis("Horizontal") * speed;

       
        

            if (Mathf.Abs(dirX) > 0 && rb.velocity.y == 0)
            anim.SetBool("isRunning", true);
        else
            anim.SetBool("isRunning", false);

        if (rb.velocity.y == 0 || isGrounded )
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", false);
        }

        if (rb.velocity.y > 0 && !isGrounded)
            anim.SetBool("isJumping", true);

        if (rb.velocity.y < 0 && !isGrounded)
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", true);
        }


        
    }

    void FixedUpdate()
    {
        
        rb.velocity = new Vector2(dirX, rb.velocity.y);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        isSliding = Physics2D.OverlapCircle(wallCheck.position, 0.05f, whatIsGround);



        if (isSliding && !isGrounded)
        {
            if (rb.velocity.y < -maxSlidingSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -maxSlidingSpeed * Time.deltaTime);
            }
            if (CrossPlatformInputManager.GetButtonDown("Jump"))
            {
                //Debug.DrawRay(ControllerColliderHit.point, ControllerColliderHit.normal, Color.red, 2.0f);
                rb.velocity = new Vector2(wallKick * Time.deltaTime , wallJump * Time.deltaTime);
            }
                

            /*
                {

                    //(Vector2.right * dashSpeed * Time.fixedDeltaTime);
                    //rb.velocity = new Vector2(rb.velocity.x * wallJump * Time.deltaTime, rb.velocity.y * jumpHeight *Time.deltaTime);
                }
            */
        }

        if (CrossPlatformInputManager.GetButtonDown("Jump") && isGrounded)
        {
            Debug.Log("Πηδάω απο το πάτωμα");
            rb.velocity = (Vector2.up * jumpHeight * Time.deltaTime);
        }
        // rb.AddForce(Vector2.up * jumpHeight);

        else if (CrossPlatformInputManager.GetButtonDown("Jump") && extraJumps > 0 && !isSliding)

        {
            Debug.Log("Πηδάω απο τον αερα");
            rb.velocity = (Vector2.up * jumpHeight * Time.deltaTime);
            //rb.AddForce(Vector2.up * jumpHeight);
            extraJumps--;
        }
        

        else if (CrossPlatformInputManager.GetButtonDown("Dash") && dashTime <= 0)
        {

            if (facingRight)
                rb.AddForce(Vector2.right * dashSpeed * Time.fixedDeltaTime);
            else
                rb.AddForce(Vector2.left * dashSpeed * Time.fixedDeltaTime);


            dashTime = startDashTime;
            rb.velocity = Vector2.zero;

        }

        else
        {
            dashTime -= Time.fixedDeltaTime;

        }
    }


    void LateUpdate()
    {
        if (dirX > 0)
            facingRight = true;
        else if (dirX < 0)
            facingRight = false;

        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
            localScale.x *= -1;

        transform.localScale = localScale;
    }

    /*void OnBecameInvisible()
    {
        if (!gameOver)
        {
            ReloadLevel();
            //Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
            Debug.Log("Gamiese");
        }
        //SceneManager.LoadScene(SceneManager.GetActiveScene);
        //SceneManager.LoadScene("Game");
        //Destroy(gameObject);
    }
  

    //metaferw se game manager
    public void ReloadLevel()
    {
        gameOver = true;
        StartCoroutine(RestarCurrentLevel());

    }

    IEnumerator RestarCurrentLevel()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    */
}
