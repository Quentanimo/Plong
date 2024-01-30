using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickoffBall : MonoBehaviour
{

    public float speed;
    public float ServeDelay = 1f; //Time Between ball being instantiated and it being served to players
    Vector2 direction; //(x,y)
    Rigidbody2D rb;
    private GameObject GameMaster = null;
    private bool _P1WonLastRound = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //find the game master object via the tag "GameController"
        //Name this game object as "GameMaster" for this script
        GameMaster = GameObject.FindGameObjectWithTag("GameController");
        //Check that the GameMaster Object was Found
        if (GameMaster == null)
        {
            Debug.Log("Error: Ball was unable to find Game Master. Check that Tag has not changed");
        }

        //check to see which player won last round in order to determine direction to serve ball
        _P1WonLastRound = GameMaster.GetComponent<ScoreTracker>().P1WonLastRound;
    }
    
     private void Start()
    {

        //delays ball being served to players by seconds as specified in ServeDelay
        //calls WhoGetsBall() which determines which direction the ball should be served
        Invoke("WhoGetsBall", ServeDelay);

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rb.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            direction.y = -direction.y;
        }
        else if (collision.gameObject.CompareTag("Paddle"))
        {
            speed += Random.Range(1.5f, 2.5f);
            direction.x = -direction.x;
        }

        //------------------Goal Conditions----------------------
        if (collision.gameObject.name == "LeftGoal")
        {
            //call function GoalPlayer2 on GameMaster
            GameMaster.GetComponent<ScoreTracker>().GoalPlayer2();
            Destroy(gameObject);
        }
        if (collision.gameObject.name == "RightGoal")
        {
            //call function GoalPlayer1 on GameMaster
            GameMaster.GetComponent<ScoreTracker>().GoalPlayer1();
            Destroy(gameObject);
        }
    }
    void WhoGetsBall()
    {
        //if P1 won last round serve ball to P1, if not then serve towards P2
        if (_P1WonLastRound == true)
        {
            direction = new Vector2(-1, Random.value);//(1,1)
        }
        else
        {
            direction = new Vector2(1, Random.value);//(1,1)
        }

    }
}

