using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public float enemySpeed;
    private bool wallCollision = false;
    public GameObject Mario;
    private MarioStateController marioStateController;

    private void Start()
    {
        marioStateController = Mario.GetComponent<MarioStateController>();
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
            if(marioStateController.marioState == MarioStateController.MarioState.invincible)
            {

            }

        }
    }

   
   



}
