using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public bool canMove = true;
    public float speed = 3.9f;
    public List<Transform> waypoints = new List<Transform>();
    private Transform targetWaypoint;
    private int targetWaypointIndex = 0;
    private float minDistance = 0.1f;
    private int lastWaypointIndex;
    public Transform startingPosition;

    private bool eaten;

    private float respawnTimer;

    public bool isWimpy;
    public bool isCutie;   
    public bool isBubbly;    
    public bool isJeff;

    public bool resetWimpy;
    public bool resetCutie;
    public bool resetBubbly;
    public bool resetJeff;

    private float ghostTimer= 10.0f;
 

    Animator anim;



    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        lastWaypointIndex = waypoints.Count - 1;
        targetWaypoint = waypoints[targetWaypointIndex];
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            respawnBehavior();
            fright();
            startBehaviour();
            timer();
           


            float movementStep = speed * Time.deltaTime;

            float distance = Vector3.Distance(transform.position, targetWaypoint.position);
            CheckDistanceToWayPoint(distance);


            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, movementStep);
        }
    }

    void CheckDistanceToWayPoint(float currentDistance)
    {
        if (currentDistance <= minDistance)
        {
            targetWaypointIndex++;
            UpdateTargetWaypoint();
        }
    }

    void UpdateTargetWaypoint()
    {
        if (targetWaypointIndex > lastWaypointIndex)
        {
            targetWaypointIndex = 0;
        }
        targetWaypoint = waypoints[targetWaypointIndex];
    }

    void fright() {
        if (PlayerController2.buff == true && eaten==false)
        {

            anim.SetBool("Boo", true);
            speed = 2.0f;
            
        }
        else if (PlayerController2.buff == false)
        {
            anim.SetBool("Boo", false);
            if (eaten == false)
            {
                speed = 3.9f;
            }
        }
    } // Displays animation based on buff
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && (PlayerController2.buff == true))
        {
            GameController.scoreValue += 100;
            eaten = true;
            anim.SetBool("Boo", false);
            transform.position = new Vector2(-0.47f, -0.44f);
            respawnTimer = 2.5f;
            targetWaypointIndex = 0;
            targetWaypoint = waypoints[targetWaypointIndex];

        }



        else if (other.gameObject.CompareTag("Player") && (PlayerController2.buff == false))
        {
            if (GameController.lives > 0)
            {
                GameObject.Find("Game").transform.GetComponent<GameController>().StartDeath(); //calls StartDeath
                GameController.lives -= 1;
            }


        }
    } //Collision

    void startBehaviour()
    {


        if (isCutie == true)
        {
            speed = 0.0f;
        }
        else if (ghostTimer == 7.0f && isCutie==false)
        {
            speed = 3.9f;
        }

        if (isBubbly == true)
        {
            speed = 0.0f;
        }

        else if (ghostTimer == 4.0f && isBubbly==false)
        {
            speed = 3.9f;
        }

        if (isJeff == true)
        {
            speed = 0.0f;
        }

        else if (ghostTimer == 1.0f && isJeff==false)
        {
            speed = 3.9f;
        }
    }

    void respawnBehavior()
    {
        if (respawnTimer > 0)
        {
            anim.SetBool("Eaten", true);
            speed = 0.0f;
        }
        if (respawnTimer <= 0)
        {
            respawnTimer = 0f;
            speed = 3.9f;
            eaten = false;
            anim.SetBool("Eaten", false);
            
        }


    }


    void timer()
    {
        ghostTimer -= Time.deltaTime;

        if (ghostTimer<=7.0f)
        {
            isCutie = false;
        }
        if (ghostTimer <= 4.0f)
        {
            isBubbly = false;
        }
        if (ghostTimer <= 1.0f)
        {
            isJeff = false;
        }
        if (ghostTimer <= 0.0f)
        {
            ghostTimer = 0.0f;
        }

        if (respawnTimer > 0)
        {
            if (PlayerController2.buffTime > respawnTimer && PlayerController2.buffTime<=3)
            {
                respawnTimer = 2.5f;
            }
            respawnTimer -= Time.deltaTime;
        }
    } // countdown

    public void Restart()
    {
        canMove = true;
        transform.GetComponent<SpriteRenderer>().enabled = true;
        transform.GetComponent<Animator>().enabled = true;
        transform.position = startingPosition.transform.position;
        targetWaypointIndex = 0;
        targetWaypoint = waypoints[targetWaypointIndex];
        ghostTimer = 10.0f;
        
        if (resetWimpy == true)
        {
            isWimpy = true;
        }
        if (resetCutie == true)
        {
            isCutie = true;
        }
        if (resetBubbly == true)
        {
            isBubbly = true;
        }
        if (resetJeff == true)
        {
            isJeff = true;
        }
    }
}
