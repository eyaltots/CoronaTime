using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    //members
    public float pScale;
    float startPosX;

    // Start is called before the first frame update
    void Awake()
    {
        //players scale
        if (pScale < 0)
        {
            transform.localScale = new Vector2(transform.localScale.x, -transform.localScale.y);
        }
        startPosX = transform.position.x;
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 50));
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        //direction
        if (pScale < 0)
        {
            transform.Translate(0, 0.1f, 0);
            //end of flight **maybe?
            //if (transform.position.x <= (startPosX - 4f))
            //{
            //    Destroy(gameObject);
            //}
            
        }
        else if (pScale > 0)
        {
            transform.Translate(0, -0.1f, 0);
            //end of flight **maybe?
            //if (transform.position.x >= (startPosX + 4f))
            //{
            //    Destroy(gameObject);
            //}
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerEngine>().life -= 20;
        }
        if(collision.gameObject.tag != "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
