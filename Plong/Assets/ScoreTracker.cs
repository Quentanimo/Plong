using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{

    public int Player1Score = 0;
    public int Player2Score = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GoalPlayer1()
    {
        Player1Score++;
        Debug.Log("GOAL PLAYER 1!");
    }

    public void GoalPlayer2()
    {
        Player2Score++;
        Debug.Log("GOAL PLAYER 2!");
    }

}
