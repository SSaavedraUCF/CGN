using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    public Transform teleport;
    public GameObject Player;
    public GameObject Slime1;
    public GameObject Slime2;
    public GameObject Slime3;
    public GameObject Slime4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other) //teleports Players and ghosts
    {
        if ((other.gameObject.tag=="Player")|| (other.gameObject.tag == "Enemy"))
        {
            Player.transform.position = teleport.transform.position;
        }
    }
}
