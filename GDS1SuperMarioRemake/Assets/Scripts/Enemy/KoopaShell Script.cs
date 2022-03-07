using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoopaShellScript : MonoBehaviour
{
    public float ShellSpeed;
    private bool wallCollision = false;
    private bool InitiateMovement = false;
    public GameObject Mario;
    public int Random;


    void Update()
    {

        if (InitiateMovement == true)
        {
            if (wallCollision == false)
            {
                transform.Translate(Vector2.left * ShellSpeed * Time.deltaTime);
            }
            else if (wallCollision == true)
            {
                transform.Translate(Vector2.right * ShellSpeed * Time.deltaTime);
            }
        }
    }
}
