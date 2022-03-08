using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioAttackScript : MonoBehaviour
{

    // Reference to koopaShell
    public GameObject KoopaShell;

    // Reference to UI 
    public ScoreController Score;

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Head Hitbox")
        {
            Destroy(collision.transform.parent.gameObject);
            Debug.Log("Hit");
            Score.score += 100;
        }

        if (collision.gameObject.tag == "KoopaHeadHitbox")
        {
            Transform KoopaTransform = collision.gameObject.GetComponent<Transform>();
            Score.score += 100;
            Instantiate(KoopaShell, new Vector3(KoopaTransform.position.x, 2.39f, KoopaTransform.position.z), Quaternion.identity);
            Debug.Log("Hit");
            Destroy(collision.transform.parent.gameObject);
        }
    }
}
