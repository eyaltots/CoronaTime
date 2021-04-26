using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeEngine : MonoBehaviour
{
    //member
    public GameObject self;
    float fullLifeX;
    float fullLifeY;
    public float fullLife;

    // Start is called before the first frame update
    private void Awake()
    {
        fullLifeX = transform.localScale.x;
        fullLifeY = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector2(fullLifeX * (self.GetComponent<BaseEnemyEngine>().life / 100), fullLifeY);
    }
}
