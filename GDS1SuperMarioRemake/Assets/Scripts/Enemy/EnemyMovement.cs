using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public float enemySpeed;
    private bool wallCollision = false;
    public GameObject Mario;
    private SpriteRenderer sp;

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //Vector3 screenPoint = cam.WorldToScreenPoint(Mario.transform.position);
        
        if (wallCollision == false)
                {
                    transform.Translate(Vector2.left * enemySpeed * Time.deltaTime);
                    
                }
                else if (wallCollision == true)
                {
                    transform.Translate(Vector2.right * enemySpeed * Time.deltaTime);
                sp.flipX = true;
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

   



}
