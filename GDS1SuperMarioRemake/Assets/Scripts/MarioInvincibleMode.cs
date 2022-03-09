using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioInvincibleMode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ChangeColor", 0.5f, 0.5f);
    }

    void ChangeColor()
    {
        GetComponent<SpriteRenderer>().color = Random.ColorHSV();
    }
}
