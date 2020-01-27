using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    public bool isCoin;
    public bool isPowerUp;
    public bool isGem;
    public float waitTime=0.0f;
    public float gemTimer = 20.0f;

    public AudioSource soundSource;
    public AudioClip coinSound;
    public AudioClip buffSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.tag == "Player") && (isCoin == true))
        {
            soundSource.clip = coinSound;
            soundSource.Play(); GameController.coinCount += 1;
            GameController.scoreValue += 10;
            StartCoroutine("destroy", waitTime);
        }

        if ((other.gameObject.tag == "Player") && (isPowerUp == true))
        {
            soundSource.clip = buffSound;
            soundSource.Play();
            GameController.coinCount += 1;
            GameController.scoreValue += 50;
            StartCoroutine("buff", waitTime);
            PlayerController2.buffTime = 5.0f; //resets timer on collision
            PlayerController2.buff = true;
        }

    }

    IEnumerator destroy(float time)
    {
        yield return new WaitForSeconds(0.1f); //Count is the amount of time in seconds that you want to wait.
        gameObject.SetActive(false);                                   //And here goes your method of resetting the game...
        yield return null;
    }

    IEnumerator buff(float time)
    {
        yield return new WaitForSeconds(0.19f); //Count is the amount of time in seconds that you want to wait.
        gameObject.SetActive(false);                                   //And here goes your method of resetting the game...
        yield return null;
    }


}

