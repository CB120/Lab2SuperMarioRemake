using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomMovement : MonoBehaviour
{

    public float originOffset = 1f;
    public float maxDistance = 0.5f;
    public float speed = 0.5f;
    private bool isMovingRight = true;
    public BoxCollider2D trigger;
    

    void Start()
    {
        trigger.enabled = true;
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
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Pipe" || collision.gameObject.tag == "Enemy")
        {
            isMovingRight = !isMovingRight;
        }
    }
}

