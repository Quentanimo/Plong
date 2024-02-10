using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class MoveBall : MonoBehaviour
{
    public float speed;
    public float ServeDelay = 1f; //Time Between ball being instantiated and it being served to players
    [SerializeField] private float TimeBeforeNextRound = 0f;
    [SerializeField]private float F;
    [SerializeField] private float KickoffForce = 300;
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
        //rb.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //I learned a new clean coding thing. Apparently it's a good idea for if statements where their conditional isn't immediately clear
        //to store that conditional in a bool with a more human-friendly name and use the bool in the if statement.
        bool CollisionIsWall = (collision.gameObject.CompareTag("Wall"));
        bool CollisionHasRigidBody = (collision.rigidbody != null);
        /*
        if (CollisionIsWall)
        {
            rb.AddForce(rb.velocity.normalized * F / 5f);
            //givees a boost in the direction  of the velocity when hitting the wall.
            //effectively this is just to ensure the ball doesn't end up in a Y-axis locked state.
            //since the Y-velocity drops to zero on impact, but some X (should) always be present.
            //this will give a little boost to X, and will always be in the direction of the balls current trajectory
            //divide by 5, because I really should make a different variable for this than using 'F', but eh
        }
        */
         
        if (CollisionHasRigidBody & !CollisionIsWall)
        {
            ContactPoint2D[] contact = new ContactPoint2D[collision.contactCount];
            //gets the contact point for the collision. This is used in the next step to find the normal vector of the collision
            rb.AddForce(collision.GetContact(0).normal * F, ForceMode2D.Force);
            //Add force in the direction of the collision normal. This means that hitting walls and sides of paddles will give a Y force.
            //while hitting the front of paddles give an X force
            rb.AddForce(collision.GetContact(0).rigidbody.velocity *F*0.60f, ForceMode2D.Force);
            //Gives force to ball porpotional and in the direction of the velocity of the object it is colliding with. 
            //so if the paddle is moving when the ball collides with it,the paddle will impart some of its velocity to the ball.
            //this allows for trickshots
            //reduced by 40% because 100% of F means the ball goes absolutely wild.
            if (collision.GetContact(0).normal.x == 0)
            {

                rb.AddForce(-rb.velocity * F*2);
            }
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //------------------Goal Conditions----------------------
        if (collision.gameObject.name == "LeftGoal")
        {
            Invoke("Player2Scores", TimeBeforeNextRound);
        }
        if (collision.gameObject.name == "RightGoal")
        {
            Invoke("Player1Scores", TimeBeforeNextRound);
        }
    }
    void WhoGetsBall()
    {
        int Negate = 1;
        //if P1 won last round serve ball to P1,d if not then serve towards P2
        if (_P1WonLastRound)
        {
            Negate = -1;
        }

        rb.AddForce(new Vector2(Negate * KickoffForce, Random.value*KickoffForce));
    }

    private void Player1Scores()
    {
        
        //call function GoalPlayer1 on GameMaster
        GameMaster.GetComponent<ScoreTracker>().GoalPlayer1();
        Destroy(gameObject);
    }

    private void Player2Scores()
    {
        
        //call function GoalPlayer2 on GameMaster
        GameMaster.GetComponent<ScoreTracker>().GoalPlayer2();
        Destroy(gameObject);
    }
}
