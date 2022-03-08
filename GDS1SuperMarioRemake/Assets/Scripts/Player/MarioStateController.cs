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
        marioState = MarioState.fire;
        //enable fire mario
    }

    private void InvincibleMario()
    {
        marioState = MarioState.invincible;
        //enable starman mario

    }

    private void PipeCinematic()
    {
        marioState = MarioState.inPipe;
    }

    private void FlagCinematic()
    {
        marioState = MarioState.onFlag;
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
