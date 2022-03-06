using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public float goombaSpeed;
    private bool wallCollision = false;
    public GameObject Mario;



    

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            if (wallCollision == false)
                {
                    transform.Translate(Vector2.left * goombaSpeed * Time.deltaTime);
                }
                else if (wallCollision == true)
                {
                    transform.Translate(Vector2.right * goombaSpeed * Time.deltaTime);
                }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pipe" || collision.gameObject.tag == "Enemy")
        {
            wallCollision ^= true; // Switch between true & false on collision (fuck me dead i just learnt about xor-equals)
        }

        if (collision.gameObject == Mario) // && Star != active)
        {
            // If mario is small, put death code
            // If mario is big, downgrade him to small
            // If mario has the flower. downgrade him to big

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == Mario)
        {
            Debug.Log("lets goooo");
            Destroy(this);
        }
    }



}
