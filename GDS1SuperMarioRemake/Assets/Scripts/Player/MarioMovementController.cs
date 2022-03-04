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
    // 4 pixels in the original game
    [SerializeField] float jumpImpulse; 
    // 5 pixels in the original game, though the actual behaviour is slightly tweaked for this project
    [SerializeField] float jumpImpulseRunning; 
    // The acceleration due to gravity when keeping the jump button held down. Around 2/16ths of a pixel in the original   
    [SerializeField] float jumpHoldGravityStrength; 

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        groundedTest = GetComponent<GroundedTest>();
    }

    // Using standard update to catch all input down/ups
    private void Update() {
        Movement();
    }

    private void Movement() {
        // Take raw input axes
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        // Set the speed based on if run is held
        float speed = (Input.GetButton("Run") ? maxRunSpeed : maxWalkSpeed);
        // Set X velocity
        velocity.x = speed * input.x;
        
        // Jumping/Falling
        if (IsGrounded()) {
            velocity.y = -gravityStrength * (1/Time.fixedDeltaTime);
            if (Input.GetButtonDown("Jump")) {
                Jump();
            }
        } else {
            // Apply acceleration due to gravity
            // TODO: terminal velocity
            float g = gravityStrength;
            if (Input.GetButton("Jump")) {
                g = jumpHoldGravityStrength;
            }

            // this looks dumb but trust me
            velocity.y -= g * Time.deltaTime * (1 / Time.fixedDeltaTime);

        }

        
        // Apply velocity to rigidbody
        rb.velocity = velocity;
    }

    private void Jump () {
        // Not super authentic, while running does increase your jump height, it's not just going off the run button being held down, but this approximates it well enough.  
        velocity.y = Input.GetButton("Jump") ? jumpImpulseRunning : jumpImpulse;
    }

    private bool IsGrounded () {
        return groundedTest.IsGrounded();
    }


}
