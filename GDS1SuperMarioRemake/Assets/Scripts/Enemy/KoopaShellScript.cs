using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoopaShellScript : MonoBehaviour
{
    public float ShellSpeed;
    private bool wallCollision = false;
    public bool InitiateMovement = false;
    public GameObject Mario;


    void Update()
    {
        if (InitiateMovement == true)
        {
            if (wallCollision == true)
            {
                transform.Translate(Vector2.left * ShellSpeed * Time.deltaTime);
            }
            else if (wallCollision == false)
            {
                transform.Translate(Vector2.right * ShellSpeed * Time.deltaTime);
            }
        }
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(4.5f);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (InitiateMovement == false)
        {
            if (collision.gameObject.tag == "Player")
            {
                InitiateMovement = true;
                Debug.Log("Shellz");
                StartCoroutine(SelfDestruct());
            }
        }
        else if (InitiateMovement == true)
        {
            if (collision.gameObject.tag == "Pipe")
            {
                wallCollision ^= true;
            }
            if (collision.gameObject.tag == "Enemy")
            {
                Destroy(collision.gameObject);
                Debug.Log("Hit with a shell");
            }

            if (collision.gameObject.tag == "Player")
            {
                Debug.Log("Touched shell");
            }
        }
    }
}


