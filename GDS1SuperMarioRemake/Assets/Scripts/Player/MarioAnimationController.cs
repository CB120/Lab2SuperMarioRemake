using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioAnimationController : MonoBehaviour
{
    //Variables that determine which animation to play
    private float lastDirectionGiven = 0;

    //External References
    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private GroundedTest groundedTest;

    void Awake()
    {
        //Initialise external references
        animator = GetComponent<Animator>();
        rb = gameObject.transform.parent.GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        groundedTest = gameObject.transform.parent.GetComponent<GroundedTest>();
        lastDirectionGiven = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetAnimatorOrientation();
        SetAnimatorParams();
        SetAnimationSpeed();
    }

    void SetAnimatorOrientation()
    {
        //The sprite shouldn't flip if mario is airbourne
        if(groundedTest.IsGrounded() && rb.velocity.y <= 0.1)
        {
            //Flip Mario if the player is moving him left
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                spriteRenderer.flipX = true;
            }
            //Reset Mario's orientation if the player is moving him right
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                spriteRenderer.flipX = false;
            }
        }
    }

    void SetAnimatorParams()
    {
        animator.SetBool("isGrounded", groundedTest.IsGrounded());
        animator.SetFloat("Velocity", Mathf.Abs(Input.GetAxis("Horizontal")));
        if (Input.GetKey(KeyCode.I))
        {
            GrowMario();
        }
        else if (Input.GetKey(KeyCode.O))
        {
            FireMario();
        }
        else if (Input.GetKey(KeyCode.P))
        {
            ShrinkMario();
        }
    }

    void SetAnimationSpeed()
    {
        //Speed up Mario's animation if he is sprinting
        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.speed = 1.5f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.speed = 1.0f;
        }
    }

    public void GrowMario()
    {
        animator.SetBool("isBig", true);
    }

    public void FireMario()
    {
        animator.SetBool("isFire", true);
    }

    public void ShrinkMario()
    {
        animator.SetBool("isBig", false);
        animator.SetBool("isFire", false);
    }
}
