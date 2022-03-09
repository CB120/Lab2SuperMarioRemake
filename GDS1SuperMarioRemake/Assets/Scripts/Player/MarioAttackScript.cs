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

    [SerializeField] AudioClip hitClip;

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(gameObject.transform.position.y - collision.gameObject.transform.position.y > 0 && GetComponent<MarioStateController>().GetStateAsString() != "dead")
        {
            if (collision.gameObject.tag == "Head Hitbox")
            {
                //Destroy(collision.transform.parent.gameObject);
                /*The goomba will now instead play the death animation then destroy itself from the animator*/
                //Debug.Log(collision.gameObject.transform.parent.GetComponent<Animator>());
                //GetComponentInParent<Rigidbody2D>().AddForce(collision.transform.up * 10000);s
                if (collision.gameObject.transform.parent.GetComponent<Animator>().isActiveAndEnabled)
                {
                    GameObject goomba = collision.gameObject.transform.parent.gameObject;
                    goomba.GetComponent<BoxCollider2D>().size = new Vector2(goomba.GetComponent<BoxCollider2D>().size.x, goomba.GetComponent<BoxCollider2D>().size.y / 2);
                    goomba.GetComponent<Rigidbody2D>().simulated = false;
                    goomba.GetComponent<EnemyMovement>().enabled = false;
                    goomba.transform.Translate(new Vector2(0, -0.3f), Space.Self);
                    goomba.GetComponent<Animator>().SetBool("IsDead", true);
                    SoundManager.PlaySound(hitClip);
                    GetComponent<MarioMovementController>().QueueJump();
                }
                Debug.Log("Hit");
                Score.IncreaseScore(100);
            }

            if (collision.gameObject.tag == "KoopaHeadHitbox")
            {
                //GetComponentInParent<Rigidbody2D>().AddForce(collision.transform.up * 10000);
                Transform KoopaTransform = collision.gameObject.GetComponent<Transform>();
                Score.IncreaseScore(100);
                Instantiate(KoopaShell, new Vector3(KoopaTransform.position.x, 2.39f, KoopaTransform.position.z), Quaternion.identity);
                Debug.Log("Hit");
                Destroy(collision.transform.parent.gameObject);
                SoundManager.PlaySound(hitClip);
                GetComponent<MarioMovementController>().QueueJump();
            }
        }
        Debug.Log(collision.gameObject.tag);
    }
}
