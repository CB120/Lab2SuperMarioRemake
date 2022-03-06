using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public bool FollowPlayer = true;
    public float CameraYAxis;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > transform.position.x && FollowPlayer)
        {
            transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        }
    }
}
