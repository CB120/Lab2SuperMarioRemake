using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablesMovement : MonoBehaviour
    {    
    public float speed = 0.5f;
    private bool isMovingRight = true;
    // BoxCollider2D trigger;
    

    void Start()
    {
        //trigger.enabled = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (isMovingRight)
        {
            transform.position += transform.right * speed * Time.deltaTime; ;
        }
        else
        {
            transform.position += -transform.right * speed * Time.deltaTime;
        }

        if (gameObject.transform.position.y < 0)
        {
            Destroy(gameObject);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Untagged")
        {
            isMovingRight = !isMovingRight;
        }
    }
}

