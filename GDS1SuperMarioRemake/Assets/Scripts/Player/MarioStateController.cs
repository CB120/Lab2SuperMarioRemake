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
    [SerializeField] CapsuleCollider2D MainCollider;
    [SerializeField] BoxCollider2D TriggerColliderBottom;
    [SerializeField] BoxCollider2D TriggerColliderTop;

    GameObject Koopa;

    //Variables that control Mario's hitbox ~ David
    [SerializeField] private float smallColliderX;
    [SerializeField] private float smallColliderY;
    [SerializeField] private float bigColliderX;
    [SerializeField] private float bigColliderY;
    [SerializeField] private Vector2 smallColliderOffset;
    [SerializeField] private Vector2 bigColliderOffset;
    [SerializeField] private Vector2 bigTopTriggerOffset;
    [SerializeField] private Vector2 smallTopTriggerOffset;

    //iFrame variables
    private float invulnerabilityDuration;
    private int flashCount;

    [SerializeField] AudioClip shroomClip;
    [SerializeField] AudioClip oneUpClip;

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        for (int i = 0; i < flashCount; i++)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(invulnerabilityDuration / flashCount);
            GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(invulnerabilityDuration / flashCount);
        }
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        invulnerabilityDuration = 5.0f;


        //Initialise initial collider sizes
        smallColliderX = MainCollider.size.x;
        smallColliderY = MainCollider.size.y;
        smallColliderOffset = MainCollider.offset;
        //Determine the size of big/fire Mario's colliders based on small collider
        bigColliderX = smallColliderX;
        bigColliderY = smallColliderY * 2.0f;
        bigColliderOffset = smallColliderOffset;

        smallTopTriggerOffset = TriggerColliderTop.offset;
        bigTopTriggerOffset = new Vector2(0, 1.02f);
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
                SoundManager.PlaySound(shroomClip);
                Destroy(collision.gameObject);
                break;
            case "Starman":
                InvincibleMario();
                Destroy(collision.gameObject);
                break;
            case "1UP":
                //increase score
                SoundManager.PlaySound(oneUpClip);
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

                if (collision.gameObject.GetComponent<KoopaShellScript>().InitiateMovement == true)
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

            //MainCollider.size = new Vector2(0.8116932f, 1.549417f);
            //MainCollider.offset = new Vector2(9.536743e-07f, -0.02277434f);

            //TriggerColliderBottom.size = new Vector2(0.4951229f, 0.2364993f);
            //TriggerColliderBottom.offset = new Vector2(-0.05248165f, 0.8842032f);
            //transform.Translate(new Vector2(0, 3f));

            /*Created a method that handles growing and shrinking - not sure why but Mario
             won't take input once this is done*/
            UpdateMariosHitbox(false);
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
        GameObject.FindGameObjectWithTag("GameController").GetComponent<MusicController>().ChangeMusic("Star", 5f);

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
    public void OnEnemyCollision()
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
        UpdateMariosHitbox(true);
        StartCoroutine(Invulnerability());
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
        gameController.GetComponent<MusicController>().ChangeMusic("Death", 0);
        //GetComponent<BoxCollider2D>().enabled = false;
        Physics2D.IgnoreLayerCollision(6, 0, true);
    }

    private void RespawnMario()
    {

    }

    private void UpdateMariosHitbox(bool setToSmall)
    {
        if (!setToSmall)
        {
            if (GetComponent<GroundedTest>().IsGrounded())
            {
                transform.Translate(new Vector2(0, 1), Space.Self);
            }
            MainCollider.size = new Vector2(bigColliderX, bigColliderY);
            MainCollider.offset = bigColliderOffset;
            TriggerColliderBottom.offset = new Vector2(TriggerColliderBottom.offset.x, TriggerColliderBottom.offset.y * 1.5f);
            TriggerColliderTop.offset = bigTopTriggerOffset;

        }
        else
        {
            if (GetComponent<GroundedTest>().IsGrounded())
            {
                transform.Translate(new Vector2(0, 1), Space.Self);
            }
            MainCollider.size = new Vector2(smallColliderX, smallColliderY);
            MainCollider.offset = smallColliderOffset;
            TriggerColliderBottom.offset = new Vector2(TriggerColliderBottom.offset.x, TriggerColliderBottom.offset.y / 1.5f);
            TriggerColliderTop.offset = smallTopTriggerOffset;
        }

        GetComponent<GroundedTest>().UpdateHitboxSize(setToSmall);
    }

    //Added by Ethan bc he was too tired to work out how to reference an enum across classes
    public string GetStateAsString(){
        switch (marioState){
            case MarioState.large: return "large";
            case MarioState.fire: return "largeFire";
            case MarioState.invincible: return "invincible";
            case MarioState.dead: return "dead";
            default: return "small";
        }
    }
}
