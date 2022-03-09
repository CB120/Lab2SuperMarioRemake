using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public bool FollowPlayer = true;
    public float CameraYAxis;
    public GameObject Goomba;
    public GameObject Koopa;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Goomba = GameObject.FindGameObjectWithTag("Enemy");
       
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > transform.position.x && FollowPlayer)
        {
            transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
            //Goomba.GetComponent<EnemyMovement>().enabled = true;
            //Koopa.GetComponent<EnemyMovement>().enabled = true;
        }

        /* Trying to get the camera to work where it will update the movement / scripts of the enemies
        if (Goomba != null)
        {
            if (Goomba.GetComponent<SpriteRenderer>().isVisible)
            {
                Goomba.GetComponent<EnemyMovement>().enabled = true;
            }
            else
            {
                Goomba.GetComponent<EnemyMovement>().enabled = false;
            }
        }
        */
    }
}
