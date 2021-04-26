using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEngine : MonoBehaviour
{
    //Members
    float speed;
    bool onGround;

    [HideInInspector]
    public bool moveRight;
    public bool moveLeft;
    public bool rightDirection; //looking to the right.

    [Header("Life & Strength")]
    public float life;
    public float strength;
    public float strengthPowerUp;
    public Text lifeScreen;
    public Text strengthScreen;

    [Header("Shooting")]
    public Transform shootingPoint;
    public GameObject virusShoot;

    [Header("Animation")]
    public Animator anm;
    GameObject playerSprite;
    public bool playerDied;

    [Header("Sound")]
    public AudioSource sorce;
    public AudioClip shootSound;
    public AudioClip vaccined;
    public AudioClip powerUp;

    // Start is called before the first frame update
    void Start()
    {
        life = 100;
        moveRight = false;
        moveLeft = false;
        rightDirection = true; // default
        speed = 5;
        playerSprite = anm.gameObject;
        strength = 15;
        playerDied = false;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        lifeScreen.text = life.ToString();
        strengthScreen.text = strength.ToString();

        if (life <= 0 && !playerDied) // death
        {
            KillPlayer();
        }
        if (moveRight) // walking right
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
            playerSprite.transform.Rotate(0, 0, -130 * Time.deltaTime);

        }

        if (moveLeft) // walking left
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
            playerSprite.transform.Rotate(0, 0, 130 * Time.deltaTime);
        }

    }

    //
    // called at ButtonManager
    //
    public void MoveRight()
    {
        if (!playerDied)
        {
            moveRight = true;
            anm.SetBool("isWalking", true);
            rightDirection = true;
            shootingPoint.localPosition = new Vector2(1.7f, 0);
        }
    }

    //
    // called at ButtonManager
    //
    public void MoveLeft()
    {
        if (!playerDied)
        {
            moveLeft = true;
            anm.SetBool("isWalking", true);
            rightDirection = false;
            shootingPoint.localPosition = new Vector2(-1.7f, 0);
        }
    }

    //
    // called at ButtonManager
    //
    public void Stop()
    {
        moveRight = false;
        moveLeft = false;
        anm.SetBool("isWalking", false);
    }

    //
    // called at ButtonManager
    //
    public void Jump()
    {
        if (onGround && !playerDied)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 400));
            onGround = false;
            anm.SetTrigger("jumping");
        }
    }

    //
    // called at ButtonManager
    //
    public void Shoot()
    {
        if (!playerDied)
        {
            GameObject virusClone = Instantiate(virusShoot, shootingPoint.position, virusShoot.transform.localRotation);
            virusClone.GetComponent<VirusShoot>().SetPlayer(gameObject);
            anm.SetTrigger("shoot");
            sorce.PlayOneShot(shootSound);
        }
    }

    public void KillPlayer()
    {
        anm.SetTrigger("dead");
        playerDied = true;
        Stop();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (playerDied) return;
        if(collision.gameObject.tag == "Floor")
        {
            onGround = true;
        }

        if (collision.gameObject.tag == "Enemy" || (collision.gameObject.tag == "syringe"))
        {
            sorce.PlayOneShot(vaccined);
            anm.SetTrigger("gotHit");
        }

        if (collision.gameObject.tag == "Upgrade")
        {
            sorce.PlayOneShot(powerUp);
            Destroy(collision.gameObject);
            strength += strengthPowerUp;
        }

        if (collision.gameObject.tag == "edge")
        {
            KillPlayer();
        }

        // Tutorial
        if(collision.gameObject.name == "Sign1")
        {

        }
    }
}
