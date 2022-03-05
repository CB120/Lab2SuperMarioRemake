using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody2D rb;
    [SerializeField] private float originOffset = 0.5f;
    public float maxDistance = 0.5f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
    }

    void Jump()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 startingPos = new Vector2(gameObject.transform.position.x, transform.position.y + originOffset);
            RaycastHit2D hit = Physics2D.Raycast(startingPos, Vector2.up, maxDistance);

            if (hit.collider)
            {
                hit.collider.GetComponentInParent<BlockController>().ActivateBlock();
                Debug.DrawRay(startingPos, Vector2.up, Color.red, 3f);
            }
            
        }
    }
}