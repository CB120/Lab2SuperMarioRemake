using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
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

    public void ActivateBlock(ScoreController score)
    {
        switch (blockType)
        {
            case Blocks.QUESTIONBOX:
                GetComponentInChildren<Animator>().SetTrigger("isOpen");
                Invoke("SpawnItem", spawnItemTime);
                blockType = Blocks.EMPTY;
                break;
            case Blocks.BREAKABLEBOX:
                Instantiate(pickup, gameObject.transform.position, Quaternion.identity);
                Destroy(gameObject);
                break;
            case Blocks.COINBRICK:
                GetComponentInChildren<Animator>().SetTrigger("isOpen");
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
            loopCount--;
            GetComponentInChildren<Animator>().SetTrigger("isLooping");
            SpawnAndDeleteItem();
        }
        else
        {
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
        ScoreController newScore = collision.gameObject.GetComponent<ScoreController>();
        if (newScore)
        {
            ActivateBlock(newScore);
        }
    }

}
