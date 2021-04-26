using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusShoot : MonoBehaviour
{
    //Members
    float speed;
    float addSpeed;
    public GameObject player;

    private void Awake()
    {
        Destroy(gameObject, 5);
    }
    // Update is called once per frame
    void Update()
    {
        addSpeed = 0;
        transform.Translate((speed+addSpeed) * Time.deltaTime, 0, 0);

        // when player is moving, its shoots are faster
        if(player.gameObject.GetComponent<PlayerEngine>().moveRight || player.gameObject.GetComponent<PlayerEngine>().moveLeft)
        {
            addSpeed = 5;
        }
       // transform.Rotate(0, 0, 100*Time.deltaTime); // virus shoot rotation
    }

    public void SetPlayer(GameObject obj)
    {
        player = obj;
        speed = 5;
        if (!player.GetComponent<PlayerEngine>().rightDirection)
        {
            speed = -speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<BaseEnemyEngine>().GetHit(player.gameObject.GetComponent<PlayerEngine>().strength);
        }

        if (collision.gameObject.tag == "syringe")
        {
            Destroy(collision.gameObject);
        }

    }

}
