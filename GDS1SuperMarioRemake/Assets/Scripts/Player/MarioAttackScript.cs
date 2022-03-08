using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioAttackScript : MonoBehaviour
{
    /*IF THIS SCRIPT ISNT WORKING MAKE SURE ITS ATTACHED TO MARIO AND NOT THE MARIOSPRITE CHILD OBJECT*/

    // Reference to koopaShell
    public GameObject KoopaShell;

    // Reference to UI 
    public ScoreController Score;

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Head Hitbox")
        {
            //Destroy(collision.transform.parent.gameObject);
            /*The goomba will now instead play the death animation then destroy itself from the animator*/
            Debug.Log(collision.gameObject.transform.parent.GetComponent<Animator>());
            if (collision.gameObject.transform.parent.GetComponent<Animator>().isActiveAndEnabled)
            {
                GameObject goomba = collision.gameObject.transform.parent.gameObject;
                goomba.GetComponent<BoxCollider2D>().enabled = false;
                goomba.GetComponent<Animator>().SetBool("IsDead", true);
            }
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

        Debug.Log(collision.gameObject.tag);
    }
}
