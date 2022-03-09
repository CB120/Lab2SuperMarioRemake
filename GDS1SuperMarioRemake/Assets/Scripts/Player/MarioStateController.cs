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

    //======================================
    //              COLLISIONS
    //======================================
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
                if(marioState == MarioState.invincible)
                {
                    //collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
                    //collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    //collision.gameObject.GetComponent<EnemyMovement>().enabled = false;
                    Destroy(collision.gameObject);
                    break;
                }
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

    //======================================
    //              ABILITY STATES
    //======================================
    private void GrowMario()
    {
        if (marioState != MarioState.large || marioState != MarioState.fire || marioState != MarioState.invincible)
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
        GetComponent<FireShootScript>().enabled = true;
        if (marioState != MarioState.invincible)
        {
            marioState = MarioState.fire;
        }
        
        
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

    //======================================
    //              NON ABILITY STATES
    //======================================
    private void PipeCinematic()
    {
        marioState = MarioState.inPipe;
    }

    private void FlagCinematic()
    {
        marioState = MarioState.onFlag;
    }

    //======================================
    //              ENEMY COLLISIONS
    //======================================
    private void OnEnemyCollision()
    {
        if(marioState == MarioState.small)
        {
            MarioIsDead();
        }
        else if (marioState == MarioState.large || marioState == MarioState.fire)
        {
            marioState = MarioState.small;
            TakeDamage();//Do animation stuff here to make mario small again

        }
    }

    private void TakeDamage()
    {
        GetComponent<FireShootScript>().enabled = false;
        //Do animation shit here n that to make mario small
        transform.GetChild(0).GetComponent<MarioAnimationController>().ShrinkMario();
    }

    public void MarioIsDead()
    {
        marioState = MarioState.dead;
        GetComponent<MarioMovementController>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        transform.GetChild(0).GetComponent<MarioAnimationController>().KillMario();
        rb.gravityScale = 1;
        rb.AddForce(transform.up * 600);
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        gameController.GetComponent<GameController>().PlayerIsDead();
    }

    private void RespawnMario()
    {
        
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
