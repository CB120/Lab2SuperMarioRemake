using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseWhenHIdden : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private EnemyMovement enemyMovement;
    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnBecameInvisible()
    {
        enemyMovement.enabled = false;
    }
    private void OnBecameVisible()
    {
        enemyMovement.enabled = true;
    }
}
