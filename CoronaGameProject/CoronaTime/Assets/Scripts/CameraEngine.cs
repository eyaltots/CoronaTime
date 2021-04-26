using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEngine : MonoBehaviour
{
    //members
    public GameObject player;
    float cameraPositionY;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (player.gameObject.GetComponent<PlayerEngine>().playerDied)
        {
            cameraPositionY = player.transform.position.y -1.5f;
        }
        else
        {
            cameraPositionY = player.transform.position.y;
        }
        transform.position = new Vector3(player.transform.position.x, cameraPositionY, -10);
    }
}
