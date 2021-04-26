using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyEngine : MonoBehaviour
{
    //facing + walk direction
    [Header("walk")]
    GameObject player;
    bool direction;
    float localSpeed;
    Vector2 startpos;
    public float border;
    public float speed;

    //death
    [Header("death")]
    public float life = 100;
    bool isDead;
    public GameManager gameManager;

    //shoot
    [Header("shoot")]
    public GameObject arrow;
    public Transform firePoint;
    int fire;

    //animation
    Animator anm;

    //armored player
    [Header("is base enemy")]
    public bool isBase;
    public GameObject baseEnemy;

    //drop player upgrade
    [Header("drop upgrade")]
    public GameObject upgrade;
    public Transform dropUpgrade;

    //sounds
    [Header("Sounds")]
    public AudioSource source;
    public AudioClip cough;

    // Start is called before the first frame update
    void Start()
    {
        //walking
        player = GameObject.Find("Player");
        direction = true;
        localSpeed = Time.deltaTime * speed;
        startpos = transform.position;
        
        //death
        isDead = false;

        //animation
        anm = gameObject.GetComponent<Animator>();

        //shoot
        fire = 0;

        source = GameObject.Find("AudioManager").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //walk direction
        //if player in border
        if (player.transform.position.x >= (-border + startpos.x) && player.transform.position.x <= (border + startpos.x) && player.transform.position.y >= startpos.y - 1 && player.transform.position.y <= startpos.y + 1)
        {
            if (player.transform.localScale.x > 0 || transform.position.x >= (border + startpos.x))
            {
                direction = false;
            }
            else if (player.transform.localScale.x < 0 || transform.position.x <= (-border + startpos.x))
            {
                direction = true;
            }
        }
        //if player is not in border
        else
        {
            if (transform.position.x >= (border + startpos.x))
            {
                direction = false;
            }
            else if (transform.position.x <= (-border + startpos.x))
            {
                direction = true;
            }
        }
        //walking
        if (direction && !isDead)
        {
            transform.Translate(localSpeed, 0, 0);
            //facing
            transform.localScale = new Vector2(1f, 1f);
        }
        else if (!isDead)
        {
            transform.Translate(-localSpeed, 0, 0);
            //facing
            transform.localScale = new Vector2(-1f, 1f);
        }
        //shoot        
        if (fire == 300)
        {
            //direction
            arrow.GetComponent<EnemyFire>().pScale = transform.localScale.x;// - change engine!!!
            //fire
            Instantiate(arrow, firePoint.position, arrow.transform.localRotation);
            //fire delay
            fire = 0;
        }
        //fire delay
        fire++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //base enemy death
        if (life <= 0 && isBase)
        {
            //drop upgrade
            Instantiate(upgrade, firePoint.position, upgrade.transform.localRotation);
            //sounds
            source.PlayOneShot(cough);
            //death
            isDead = true;
            Destroy(gameObject);
            gameManager.deadEnemiesCounter++;
        }
        //replace armored with base
        else if (life <= 0 && !isBase)
        {
            //sounds
            source.PlayOneShot(cough);
            //create base enemy
            BaseEnemyEngine newBase = Instantiate(baseEnemy, transform.position, transform.localRotation).GetComponent<BaseEnemyEngine>();
            newBase.border = this.border;
            newBase.speed = this.speed;
            newBase.arrow = this.arrow;
            newBase.upgrade = this.upgrade;
            newBase.dropUpgrade = this.dropUpgrade;
            newBase.isBase = true;
            newBase.firePoint = newBase.GetComponentInChildren<Transform>().transform;
            //destroy armored enemy
            Destroy(gameObject);               
        }

        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerEngine>().life -= 20;
        }
    }

    //called by player's "dart"
    //gets enemy's strength
    public void GetHit(float down)
    {
        life -= down;
    }
}
