using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawn : MonoBehaviour
{
    public float yIncrease = 1;
    public float timeToSpawn = 1.0f;
    // Start is called before the first frame update
    void Start()
    {

        yIncrease = gameObject.transform.position.y + 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = gameObject.transform.position;
        gameObject.transform.position = Vector2.Lerp(gameObject.transform.position, new Vector2(pos.x, yIncrease), 0.5f);
    }
}
