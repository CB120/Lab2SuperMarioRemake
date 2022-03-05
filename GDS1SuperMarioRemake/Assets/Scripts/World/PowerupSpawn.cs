using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawn : MonoBehaviour
{
    public float yIncrease = 1;
    public float timeToSpawn = 1.0f;
    private bool bIsSpawning = true;
    private float endPosition = 0;
    // Start is called before the first frame update
    void Start()
    {

        endPosition = gameObject.transform.position.y + yIncrease;
        Invoke("FinishedSpawning", timeToSpawn + 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (bIsSpawning)
        {
            Vector2 pos = gameObject.transform.position;
            gameObject.transform.position = Vector2.Lerp(gameObject.transform.position, new Vector2(pos.x, endPosition), timeToSpawn);
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
        
    }
}
