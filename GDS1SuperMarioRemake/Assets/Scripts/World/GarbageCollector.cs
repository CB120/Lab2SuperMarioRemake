using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCollector : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<MarioStateController>().OnEnemyCollision();
        }
        if (collision.gameObject.tag != "Player")
        {
            Destroy(collision.gameObject);
        }


    }

}
