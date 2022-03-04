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
    [SerializeField] float maxWalkSpeed; // 1 and 9/16 pixels/s in the original game
    [SerializeField] float maxRunSpeed;  // 2 and 9/16 p/s in the original game
    [SerializeField] float gravityStrength; // Would usually use Unity's inbuilt gravity but given the simplicity that doesn't seem worthwhile. 6/16ths of a pixel in smb
    [SerializeField] float jumpImpulse; // 4 pixels in the original game
    [SerializeField] float jumpImpulseRunning; // 5 pixels in the original game, though the actual behaviour is slightly tweaked for this project

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
        
        // debug
        if (Input.GetButtonDown("Jump")) Debug.Log(IsGrounded() ? "Grounded" : "Not grounded");
        
        // Jumping/Falling
        if (IsGrounded()) {
            if (Input.GetButtonDown("Jump")) {
                Jump();
            }
        } else {
            // Apply acceleration due to gravity
            // TODO: terminal velocity
            velocity.y -= gravityStrength * Time.deltaTime;  
        }
        
        // Apply velocity to rigidbody
        rb.velocity = velocity;
    }

    private void Jump () {
        velocity.y = jumpImpulse;
    }

    private bool IsGrounded () {
        return groundedTest.IsGrounded();
    }


}
