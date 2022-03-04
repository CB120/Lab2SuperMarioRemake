using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private Rigidbody rb;
    void Start()
    {
        speed = 2.0f;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            speed += 2f;
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            speed -= 2f;
        }
        speed *= 0.9f;
        if (Mathf.RoundToInt(speed) == 0) speed = 0f;
        rb.velocity = new Vector3(speed, 0);
    }
}
