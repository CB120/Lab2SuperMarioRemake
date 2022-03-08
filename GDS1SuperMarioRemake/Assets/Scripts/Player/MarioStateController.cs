using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioStateController : MonoBehaviour
{
    public enum MarioState
    {
        small,
        large,
        fire,
        invincible,
        dead,
        inPipe,
        onFlag,

    }

    public  MarioState marioState = MarioState.small;
    public  MarioState previousState = MarioState.small;
    Rigidbody2D rb;
    [SerializeField] BoxCollider2D MainCollider;
    [SerializeField] BoxCollider2D TriggerCollider;

    [SerializeField] KoopaShellScript Shell;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Mushroom":
                GrowMario();
                Destroy(collision.gameObject);
                break;
            case "Starman":
                InvincibleMario();
                Destroy(collision.gameObject);
                break;
            case "1UP":
                //increase score
                Destroy(collision.gameObject);
                break;
            case "Enemy":
                OnEnemyCollision();
                break;
            case "KoopaShell":
                Debug.Log(Shell.InitiateMovement);
                if (Shell.InitiateMovement == true)
                {
                    OnEnemyCollision();
                }
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)// Might change others to trigger we will see
    {
        if(collision.gameObject.tag == "Flower")
        {
            FireMario();
            Destroy(collision.gameObject);
        }
    }

    private void GrowMario()
    {
        if (marioState != MarioState.large)
        {
            previousState = marioState;
            marioState = MarioState.large;
            transform.GetChild(0).GetComponent<MarioAnimationController>().GrowMario();
            //Make mario large

            // Please comment out the below code if you want the game to work.. this is still a work in progress ~~ Christian

            MainCollider.size = new Vector2(0.8116932f, 1.549417f);
            MainCollider.offset = new Vector2(9.536743e-07f, -0.02277434f);

            TriggerCollider.size = new Vector2(0.4951229f, 0.2364993f);
            TriggerCollider.offset = new Vector2(-0.05248165f, 0.8842032f);
            transform.Translate(new Vector2(0, 3f));
        }
        else
        {
            //give mario the reccomended amount of points
        }
    }

    private void FireMario()
    {
        previousState = marioState;
        marioState = MarioState.fire;
        transform.GetChild(0).GetComponent<MarioAnimationController>().FireMario();
        //enable fire mario
    }

    private void InvincibleMario()
    {
        previousState = marioState;
        marioState = MarioState.invincible;
        Invoke("ExitInvincible", 5f);


    }

    private void ExitInvincible()
    {
        marioState = previousState;
    }

    private void PipeCinematic()
    {
        marioState = MarioState.inPipe;
    }

    private void FlagCinematic()
    {
        marioState = MarioState.onFlag;
    }

    private void OnEnemyCollision()
    {
        if(marioState == MarioState.small)
        {
            marioState = MarioState.dead;
            GetComponent<MarioMovementController>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            
            rb.gravityScale = 1;
            rb.AddForce(transform.up * 200);
            Invoke("MarioIsDead", 5f);
        }
        else if (marioState == MarioState.large || marioState == MarioState.fire)
        {
            marioState = MarioState.small;
            TakeDamage();//Do animation stuff here to make mario small again

        }
    }

    private void TakeDamage()
    {
        //Do animation shit here n that to make mario small
        transform.GetChild(0).GetComponent<MarioAnimationController>().ShrinkMario();
    }

    private void MarioIsDead()
    {
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        //gameController.GetComponent<GameController>().Respawn();
    }


    //Added by Ethan bc he was too tired to work out how to reference an enum across classes
    public string GetStateAsString(){
        switch (marioState){
            case MarioState.large: return "large";
            case MarioState.fire: return "fire";
            case MarioState.invincible: return "invincible";
            default: return "small";
        }
    }
}
