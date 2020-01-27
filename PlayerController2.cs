using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{

    public bool canMove = true;
    public float speed = 4.0f;
    private Vector2 direction = Vector2.zero;
    public static bool buff = false;
    public static bool alive = true;
    public static float buffTime=5.0f;

    public Transform startingPosition;



   public static Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (canMove)
        {
            buffTimer();  //Timer is activated
            PlayerInput(); //arrows
        }
    }

    void PlayerInput() //Declares the direction
    {
        if (Input.GetKeyDown (KeyCode.RightArrow))
        {
            anim.SetInteger("Direction", 1);
            direction = Vector2.right;
        }

        else if (Input.GetKeyDown (KeyCode.LeftArrow))
        {
            anim.SetInteger("Direction", 2);
            direction = Vector2.left;
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            anim.SetInteger("Direction", 3);
            direction = Vector2.up;
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            anim.SetInteger("Direction", 4);
            direction = Vector2.down;
        }

    

    }

    void FixedUpdate()
    {

        if (canMove)
        {
            Move(); //speed
            Win(); //win condition
            powerUpReset(); //resets buff
        }
    }

    void Move() //movement function
    {
        transform.localPosition += (Vector3)(direction * speed) * Time.deltaTime;

        if(alive == false)
        {
            speed = 0f;
        }
    }

    void buffTimer()
    {
        if (buff ==true)
        {
            buffTime -= Time.deltaTime;
        }
    }//timer for buff
    void powerUpReset()
    {


        if (buffTime <= 0f)
        {
            buff = false;
            buffTime=5.0f;
            
        }
        
    } // Reset Time function for buff
    void Win()
    {
        if (GameController.win == true)
        {
            speed = 0.0f;
            GameController.level = 2;

        }
    }

    public void Restart()
    {
        canMove = true;
        anim.SetBool("Death", false);
        transform.position = startingPosition.transform.position;
    }
}
