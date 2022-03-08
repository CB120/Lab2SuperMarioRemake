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

    public static MarioState marioState = MarioState.small;
    public static MarioState previousState = MarioState.small;

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
            //Make mario large
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


}
