using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioMovementController : MonoBehaviour
{
    Rigidbody2D rb;

    // Properties
    [SerializeField] float maxWalkSpeed; // 1 and 9/16 pixels/s in the original game
    [SerializeField] float maxRunSpeed;  // 2 and 9/16 p/s in the original game
    [SerializeField] float gravityStrength; // Would usually use Unity's inbuilt gravity but given the simplicity that doesn't seem worthwhile. 6/16ths of a pixel in smb

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        Movement();
    }

    private void Movement() {
        // Take raw input axes
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 velocity = new Vector2();

        // Set the speed based on if run is held
        float speed = Input.GetButton("Run") ? maxRunSpeed : maxWalkSpeed;
        // Set X velocity
        velocity.x = speed * input.x;
        
        // Jumping/Falling
        // TODO: IsGrounded() currently not properly implemented
        if (IsGrounded()) {
            velocity.y = 0;
            if (Input.GetButtonDown("Jump")) {
                Jump();
            }
        } else {
            // New velocity should be the old velocity plus acceleration due to gravity
            // TODO: terminal velocity
            velocity.y = rb.velocity.y - gravityStrength;
        }
        
        // Apply velocity to rigidbody
        rb.velocity = velocity;
    }

    private void Jump () {
        //TODO
    }

    private bool IsGrounded () {
        return false;
    }


}
