using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GroundedTest))]
public class MarioMovementController : MonoBehaviour
{
    Rigidbody2D rb;

    Vector2 velocity = new Vector2();
    GroundedTest groundedTest;

    // Properties
    // All are in units per second or units per second squared for acceleration
    // 1 and 9/16 pixels/s in the original game
    [SerializeField] float maxWalkSpeed; 
    // 2 and 9/16 p/s in the original game
    [SerializeField] float maxRunSpeed;  
    // Would usually use Unity's inbuilt gravity but given the simplicity that doesn't seem worthwhile. 6/16ths of a pixel in smb
    [SerializeField] float gravityStrength;
    // Downward velocity applied when grounded
    [SerializeField] float groundStickingVelocity;
    // Maximum fall velocity, 4/16ths in smb
    [SerializeField] float terminalVelocity;
    // 4 pixels in the original game
    [SerializeField] float jumpImpulse; 
    // 5 pixels in the original game, though the actual behaviour is slightly tweaked for this project
    [SerializeField] float jumpImpulseRunning; 
    // The acceleration due to gravity when keeping the jump button held down. Around 2/16ths of a pixel in the original   
    [SerializeField] float jumpHoldGravityStrength; 
    
    [SerializeField] float airControl;

    bool jumpedSinceJumpDown = false;
   


    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        groundedTest = GetComponent<GroundedTest>();
    }

    // Using standard update to catch all input down/ups
    private void FixedUpdate() {
        Movement();
    }

    private void Update() {
        // Prevent bunny-hopping
        if (Input.GetButtonUp("Jump")) {
            jumpedSinceJumpDown = false;
        }
    }

    private void Movement() {
        // Take raw input axes
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        bool grounded = IsGrounded();

        Debug.Log((grounded ? "Grounded" : "Not grounded") + ", " + (groundedTest.IsHittingCeiling() ? "Ceilinged" : "Not ceilinged"));

        // Jumping/Falling
        if (grounded) {
            velocity.x = 0; // Velocity should be directly based on input when grounded.
            velocity.y = -groundStickingVelocity;
            if (Input.GetButton("Jump") && !jumpedSinceJumpDown) {
                Jump();
                jumpedSinceJumpDown = true;
            }
        } else {
            // Apply acceleration due to gravity
            float g = gravityStrength;
            // If still rising, allow jump to boost height
            if (Input.GetButton("Jump") && velocity.y > 0) {
                g = jumpHoldGravityStrength;
            }

            velocity.y = Mathf.Clamp(velocity.y - g * Time.fixedDeltaTime, -terminalVelocity, terminalVelocity);
        }

        // Horizontal movement

        // Set the speed based on if run is held
        float speed = (Input.GetButton("Run") ? maxRunSpeed : maxWalkSpeed);
        if (!grounded) speed = airControl;
        // Apply X velocity. Note addition to allow air acceleration/deceleration. velocity.x is reset to zero when grounded above
        velocity.x += speed * input.x;
        

        
        // Apply velocity to rigidbody
        rb.velocity = velocity;
    }

    private void Jump () {
        // Not super authentic, while running does increase your jump height, it's not just going off the run button being held down, but this approximates it well enough.  
        velocity.y = jumpImpulse;
    }

    private bool IsGrounded () {
        return groundedTest.IsGrounded();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (groundedTest.IsHittingCeiling()) {
            velocity.y = 0;
        }
    }

  
}
