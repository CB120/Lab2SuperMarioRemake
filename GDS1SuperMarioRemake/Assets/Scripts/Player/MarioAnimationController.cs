using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioAnimationController : MonoBehaviour
{
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
    }

    // Update is called once per frame
    void Update()
    {
        SetAnimatorOrientation();
        SetAnimatorParams();
        SetAnimationSpeed();
        //Debug.Log("Grounded: " + groundedTest.IsGrounded());
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
        //Update the main movement parameters
        animator.SetBool("isGrounded", groundedTest.IsGrounded());
        animator.SetFloat("Velocity", Mathf.Abs(Input.GetAxis("Horizontal")));

        //Update the throwing parameter based on if the player is throwing
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetBool("isThrowing", true);
        }
        else
        {
            animator.SetBool("isThrowing", false);
        }

        //Debug Inputs - will delete later
        if (Input.GetKeyDown(KeyCode.I))
        {
            GrowMario();
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            FireMario();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            ShrinkMario();
        }
    }

    //Update the animation speed dependent on user input
    void SetAnimationSpeed()
    {
        //Speed up Mario's animation if he is sprinting
        if (Input.GetKey(KeyCode.LeftShift) && animator.GetFloat("Velocity") > 0.1f)
        {
            animator.speed = 1.5f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.speed = 1.0f;
        }
    }

    //Update Mario's animation parameters to switch to big Mario
    public void GrowMario()
    {
        animator.SetBool("isBig", true);
        animator.SetBool("isFire", false);
    }

    //Update Mario's animation parameters to switch to fire Mario
    public void FireMario()
    {
        animator.SetBool("isBig", true);
        animator.SetBool("isFire", true);
    }

    //Update Mario's animation parameters to switch back to small Mario
    public void ShrinkMario()
    {
        animator.SetBool("isBig", false);
        animator.SetBool("isFire", false);
    }

    //Update Mario's animation parameters to switch to the dead animation
    public void KillMario()
    {
        animator.SetBool("isBig", false);
        animator.SetBool("isFire", false);
        animator.SetBool("isDead", true);
    }

    public void UpdateMariosHitbox(bool setSmall)
    {
        if (setSmall)
        {

        }
        else
        {

        }
    }
}
