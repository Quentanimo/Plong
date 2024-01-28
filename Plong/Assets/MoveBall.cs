using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveBall : MonoBehaviour
{
    private Rigidbody2D m_Rigidbody2D;
    public float InitForceX = 300f;             //Kickoff Force X component
    public float InitForceY = 300f;             //Kickoff Force Y component
    public float GenericForce = 300f;           //Force from colliding with wall
    public float Gravity = 1f;
    private GameObject GameMaster = null;       //Container Variable for GameMaster Object
    private Vector2 CurrentVelocity = Vector2.zero;
    public float ForceMultiplier = 1f;
    public float FM_Increment = 0.1f;
    private void Awake()
    {
        //Get Rigidbody Component from Object
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
        //find the game master object via the tag "GameController"
        //Name this game object as "GameMaster" for this script
        GameMaster = GameObject.FindGameObjectWithTag("GameController");
        //Check that the GameMaster Object was Found
            if (GameMaster == null)
            {
                Debug.Log("Error: Ball was unable to find Game Master. Check that Tag has not changed");
            }
        



        //Add initially 'kickoff' force to the pong ball
        m_Rigidbody2D.AddForce(new Vector2(InitForceX,InitForceY));
    }


    void OnTriggerEnter2D(Collider2D collsion)
    {
        CurrentVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        //if (collsion.gameObject.name == "TopWall")
       

        m_Rigidbody2D.velocity= new Vector2(CurrentVelocity.x, -1*CurrentVelocity.y);
        Debug.Log("hit wall");
 
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        CurrentVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        Vector2 NormVector = CurrentVelocity.normalized;
        /*
        //---------------------Bounce Conditions--------------------
        if(collision.gameObject.name == "TopWall")
        {
            m_Rigidbody2D.AddForce(new Vector2(NormVector.x*ForceMultiplier, -GenericForce));

        }
        if (collision.gameObject.name == "BottomWall")
        {
            m_Rigidbody2D.AddForce(new Vector2(NormVector.x * ForceMultiplier, GenericForce));

        }
        */

        if (collision.gameObject.name == "Player1")
        {
            m_Rigidbody2D.AddForce(new Vector2(-NormVector.x * ForceMultiplier*GenericForce, GenericForce));
            //ForceMultiplier += FM_Increment;

        }
        if (collision.gameObject.name == "Player2")
        {
            m_Rigidbody2D.AddForce(new Vector2(NormVector.x * ForceMultiplier * GenericForce, GenericForce));
            //ForceMultiplier += FM_Increment;
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


    private void FixedUpdate()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        //m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
        //m_Rigidbody2D.gravityScale += Gravity;
    }
}
