using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawn : MonoBehaviour
{
    public float yIncrease = 1;
    public float timeToSpawn = 1.0f;
    public float timeToActivateMovement = 1.0f;
    private bool bIsSpawning = true;
    private float endPosition = 0;
    public enum Powerup
    {
        mushroom,
        fireFlower,
        starMan,
    }
    public Powerup powerupType;

    // Start is called before the first frame update
    void Start()
    {

        endPosition = gameObject.transform.position.y + yIncrease;
        Invoke("FinishedSpawning", timeToActivateMovement);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (bIsSpawning)
        {
            Vector2 pos = gameObject.transform.position;
            gameObject.transform.position = Vector2.Lerp(gameObject.transform.position, new Vector2(pos.x, endPosition), timeToSpawn * Time.deltaTime);
        }
    }

    void FinishedSpawning()
    {
        bIsSpawning = false;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb)
        {
            rb.gravityScale = 1;
        }


        switch (powerupType)
        {
            case Powerup.mushroom:
                GetComponent<InteractablesMovement>().enabled = true;
                GetComponent<BoxCollider2D>().enabled = true;
                break;
            case Powerup.fireFlower:

                break;
            case Powerup.starMan:
                GetComponent<InteractablesMovement>().enabled = true;
                break;
            default:

                break;
        }
    }
}
