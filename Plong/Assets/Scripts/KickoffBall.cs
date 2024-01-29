using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickoffBall : MonoBehaviour
{

    public float speed;
    Vector2 direction; //(x,y)
    Rigidbody2D rb;
    private GameObject GameMaster = null;

    // Start is called before the first frame update
    void Start()
    {
        //kickoff
        rb = GetComponent<Rigidbody2D>();
        direction = Vector2.one.normalized;//(1,1)


        //find the game master object via the tag "GameController"
        //Name this game object as "GameMaster" for this script
        GameMaster = GameObject.FindGameObjectWithTag("GameController");
        //Check that the GameMaster Object was Found
        if (GameMaster == null)
        {
            Debug.Log("Error: Ball was unable to find Game Master. Check that Tag has not changed");
        }
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
}
