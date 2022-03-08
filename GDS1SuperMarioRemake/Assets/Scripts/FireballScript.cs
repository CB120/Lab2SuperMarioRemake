using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public float HorizontalVelocity;
    private bool wallCollided = false;
    private Animator animator;
    public ScoreController Score;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        rb.velocity = new Vector2(HorizontalVelocity, rb.velocity.y);
        animator = GetComponent<Animator>();
        Score = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (wallCollided == false) rb.velocity = new Vector2(HorizontalVelocity, rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            OnContact();
            Destroy(collision.gameObject);
            Debug.Log("Hit");
            Score.score += 100;
        }
        else if (rb != null) rb.velocity = new Vector2(rb.velocity.x, 7f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        OnContact();
    }

    private void OnContact()
    {
        wallCollided = true;
        if (rb != null) Destroy(rb);
        animator.SetBool("Boom", true);
    }
}
