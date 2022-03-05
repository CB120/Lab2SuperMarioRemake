using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomMovement : MonoBehaviour
{

    public float originOffset = 1f;
    public float maxDistance = 0.5f;
    public float speed = 0.5f;
    private enum Direction {
        right,
        left,
    }
    public BoxCollider2D trigger;
    Direction direction = Direction.right;

    void Start()
    {
        trigger.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (direction == Direction.right)
        {
            Vector2 startingPos = new Vector2(gameObject.transform.position.x + originOffset, transform.position.y);
            RaycastHit2D hit = Physics2D.Raycast(startingPos, Vector2.right, maxDistance);
            Debug.DrawRay(startingPos, Vector2.right, Color.red, 3f);
            transform.position += transform.right * speed * Time.deltaTime;
            if (hit.collider)
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    Destroy(gameObject);
                }
                else
                {
                    direction = Direction.left;
                }

            }
        }
        else
        {
            Vector2 startingPos = new Vector2(gameObject.transform.position.x - originOffset, transform.position.y);
            RaycastHit2D hit = Physics2D.Raycast(startingPos, Vector2.left, maxDistance);
            Debug.DrawRay(startingPos, Vector2.left, Color.red, 3f);
            transform.position += -transform.right * speed * Time.deltaTime;
            if (hit.collider)
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    Destroy(gameObject);
                }
                else
                {
                    direction = Direction.right;
                }
                
            }
        }

        if (gameObject.transform.position.y < 0)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}

