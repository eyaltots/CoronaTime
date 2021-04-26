using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignEngine : MonoBehaviour
{
    //Members
    public int signNumber;
    [Header("Texts")]
    public Text moveText;
    public Text jumpText;
    public Text levelInfoText;
    public Text lifeText;
    public Text shootText;
    public Text enemiesCounterText;
    [Header("Sound")]
    public AudioSource sorce;
    public AudioClip signSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            sorce.PlayOneShot(signSound);
            if (signNumber == 1)
            {
                moveText.gameObject.SetActive(false);
                jumpText.gameObject.SetActive(true);
            }
            if (signNumber == 2)
            {
                jumpText.gameObject.SetActive(false);
                levelInfoText.gameObject.SetActive(true);
            }
            if (signNumber == 3)
            {
                levelInfoText.gameObject.SetActive(false);
                lifeText.gameObject.SetActive(true);
            }
            if (signNumber == 4)
            {
                lifeText.gameObject.SetActive(false);
                shootText.gameObject.SetActive(true);
            }
            if (signNumber == 5)
            {
                shootText.gameObject.SetActive(false);
                enemiesCounterText.gameObject.SetActive(true);
            }
        }
    }
}
