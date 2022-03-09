using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    private GameObject gameController;
    private ScoreController scoreController;
    [SerializeField] private GameObject pickup;
    public float spawnItemTime = 0.2f;
    public float destroyItemTime = 0.2f;
    public int loopCount = 5;
    public enum Blocks {
        QUESTIONBOX,
        BREAKABLEBOX,
        COINBRICK,
        LOOPCOINBRICK,
        EMPTY,
    };
    public Blocks blockType;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        scoreController = gameController.GetComponent<ScoreController>();
        if(blockType == Blocks.LOOPCOINBRICK)
        {
            GetComponentInChildren<Animator>().SetBool("isLoopBlock", true);
        }
    }

    public void ActivateBlock()
    {
        switch (blockType)
        {
            case Blocks.QUESTIONBOX:
                GetComponentInChildren<Animator>().SetTrigger("isOpen");
                Invoke("SpawnItem", spawnItemTime);
                blockType = Blocks.EMPTY;
                break;
            case Blocks.BREAKABLEBOX:
                if(GameObject.FindGameObjectWithTag("Player").GetComponent<MarioStateController>().GetStateAsString() != "small")
                {
                    Instantiate(pickup, gameObject.transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
                break;
            case Blocks.COINBRICK:
                GetComponentInChildren<Animator>().SetTrigger("isOpen");
                scoreController.IncreaseScore(200);
                scoreController.IncreaseCoin();
                SpawnAndDeleteItem();
                blockType = Blocks.EMPTY;
                break;
            case Blocks.LOOPCOINBRICK:
                LoopBrick();
                break;
            case Blocks.EMPTY:

                break;

        }
        
    }

    void LoopBrick()
    {
        GetComponentInChildren<Animator>().ResetTrigger("isLooping");
        if (loopCount != 0)
        {
            scoreController.IncreaseScore(200);
            scoreController.IncreaseCoin();
            loopCount--;
            GetComponentInChildren<Animator>().SetTrigger("isLooping");
            SpawnAndDeleteItem();
        }
        else
        {
            scoreController.IncreaseScore(200);
            SpawnAndDeleteItem();
            GetComponentInChildren<Animator>().ResetTrigger("isLooping");
            GetComponentInChildren<Animator>().SetTrigger("isOpen");
            blockType = Blocks.EMPTY;
        }
    }

    private void SpawnItem()
    {
        Instantiate(pickup, gameObject.transform.position, Quaternion.identity, gameObject.transform);
    }

    private void SpawnAndDeleteItem()
    {
        GameObject newPickup = Instantiate(pickup, gameObject.transform.position, Quaternion.identity);
        newPickup.transform.parent = gameObject.transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log(collision.gameObject.transform.position.y - gameObject.transform.position.y);
            if (collision.gameObject.transform.position.y - gameObject.transform.position.y < 0)
            {
                ActivateBlock();
            }
        }
    }

}
