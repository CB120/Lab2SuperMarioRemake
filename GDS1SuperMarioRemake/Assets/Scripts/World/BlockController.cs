using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    [SerializeField] private GameObject pickup;
    public float spawnItemTime = 0.2f;
    public float destroyItemTime = 0.2f;
    public enum Blocks {
        QUESTIONBOX,
        BREAKABLEBOX,
        COINBRICK,
        LOOPCOINBRICK,
        EMPTY,

    };

    public Blocks blockType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

                break;
            case Blocks.COINBRICK:
                GetComponentInChildren<Animator>().SetTrigger("isOpen");
                SpawnAndDeleteItem();
                break;
            case Blocks.LOOPCOINBRICK:

                break;
            case Blocks.EMPTY:

                break;

        }
        
    }

    private void SpawnItem()
    {
        Instantiate(pickup, gameObject.transform.position, Quaternion.identity, gameObject.transform);
    }

    private void SpawnAndDeleteItem()
    {
        pickup = Instantiate(pickup, gameObject.transform.position, Quaternion.identity);
        pickup.transform.parent = gameObject.transform;
        Destroy(pickup, destroyItemTime);
    }
}
