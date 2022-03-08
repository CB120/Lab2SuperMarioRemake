using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShootScript : MonoBehaviour
{
    public GameObject Fireball;
    private bool isRight = true;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") > 0) isRight = true;
        else if (Input.GetAxis("Horizontal") < 0) isRight = false;
        if (Input.GetButtonDown("Run") )
        {
            if (isRight == true)
            {
                Fireball.GetComponent<FireballScript>().HorizontalVelocity = 15;
                Instantiate(Fireball, new Vector2(transform.position.x + 0.7f, transform.position.y), Quaternion.identity);
            }
            else
            {
                Fireball.GetComponent<FireballScript>().HorizontalVelocity = -15;
                Instantiate(Fireball, new Vector2(transform.position.x - 0.8f, transform.position.y), Quaternion.Euler(0f, 180f, 0f));
            }
        }
    }
}
