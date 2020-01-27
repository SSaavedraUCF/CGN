using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static int scoreValue;
    public static int coinCount;
    public static bool win;

    public Text scoreText;
    public Text livesText;
    public  Text overText1;
    public  Text overText2;
    public  Text overText3;

    public static int lives=3;
    public static float resetTimer=5.0f;
    public static int level = 1;

    public bool didStartDeath;
    public bool over=false;

    public GameObject controls;
    public GameObject player;





    // Start is called before the first frame update
    void Start()
    {
        overText1.text = "";
        overText2.text= "";
        overText3.text = "";
    }

    // Update is called once per frame
    void Update()
    {

        winCondition();
        loseCondition();
        Score();
        Lives();
 
    }



    void FixedUpdate()
    {
        if (Input.GetKeyDown("escape")) //exit function
        {
            Application.Quit();
        }

        

       
    }

    public void winCondition()
    {
        if (coinCount == 144 && level <=1)
        {
            win = true;
            controls.SetActive(false);
            StartCoroutine(WinGame(2.0f));//adjust later
        }

        else if (coinCount==144 & level >= 2)
        {
            win = true;
            controls.SetActive(false);
            overText1.text = "You Win!";
            overText2.text = "Press   Esc   to  Exit";
            overText3.text = "Press   R   to  Restart";
            if (Input.GetKeyDown(KeyCode.R)) //exit function
            {
                Application.LoadLevel("CGC");
                over = false;
                lives = 3;
                coinCount = 0;
                scoreValue = 0;
                level = 1;
            }
        }

        if (lives <= 0)
        {
            lives = 0;
            GameOver();
            controls.SetActive(false);
            GameObject player = GameObject.Find("Player");
            player.transform.GetComponent<PlayerController2>().speed = 0f;

        }
    }

    public void loseCondition()
    {
        if (over=true && lives==0)
        {
            GameOver();
        }
    }


    public void Score()
    {
        scoreText.text =  scoreValue.ToString();
    }
    public void Lives()
    {
        livesText.text = "Lives " + lives.ToString();
    }

    

    public void GameOver()
    {
        overText1.text = "Game Over!";
        overText2.text = "Press   Esc   to  Exit";
        overText3.text = "Press   R   to  Restart";

    if (Input.GetKeyDown(KeyCode.R)) //exit function
            {
            Application.LoadLevel("CGC");
            over = false;
            lives = 3;
            coinCount = 0;
            scoreValue = 0;
            level = 1;
            }
    }
    public void StartDeath()
    {
        if (!didStartDeath)
        {
            didStartDeath = true;
            GameObject[] o = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject slime in o)
            {
                slime.transform.GetComponent<SlimeController>().canMove = false;
                slime.transform.GetComponent<Animator>().enabled = false;
            }
            GameObject player = GameObject.Find("Player");
            player.transform.GetComponent<PlayerController2>().canMove = false;
            player.transform.GetComponent<Animator>().enabled = false;

            StartCoroutine(ProcessDeathAfter(2.0f));
        }
    }

    IEnumerator ProcessDeathAfter (float delay)
    {
        yield return new WaitForSeconds(delay);

        GameObject[] o = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject slime in o)
        {
            slime.transform.GetComponent < SpriteRenderer >().enabled = false;
        }

        StartCoroutine(ProcessDeathAnimation(1.9f));

    }
    IEnumerator ProcessDeathAnimation(float delay)
    {
        GameObject player = GameObject.Find("Player");
        player.transform.GetComponent<Animator>().enabled = true;
        PlayerController2.anim.SetBool("Death", true);
        yield return new WaitForSeconds(delay);
        StartCoroutine(ProcessRestart(2f));
    }
    IEnumerator ProcessRestart(float delay)
    {
        yield return new WaitForSeconds(delay);
        Restart();
        
    }


        public void Restart()
    {
        didStartDeath = false;
        GameObject player = GameObject.Find("Player");
        player.transform.GetComponent<PlayerController2>().Restart();

        GameObject[] o = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject slime in o)
        {
            slime.transform.GetComponent<SlimeController>().Restart();
        }

    }
    IEnumerator WinGame(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        {
            Application.LoadLevel("CGC");
            coinCount = 0;
            win = false;

            GameObject player = GameObject.Find("Player");
            player.transform.GetComponent<PlayerController2>().speed = 4.0f;
            PlayerController2.buff = false;
        }

    }
}
