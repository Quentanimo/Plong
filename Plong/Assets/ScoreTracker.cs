using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{

    [SerializeField] private int Player1Score = 0;
    [SerializeField] private int Player2Score = 0;
    public RoundReset _RoundReset;

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
        _RoundReset.Reset();
    }

    public void GoalPlayer2()
    {
        Player2Score++;
        Debug.Log("GOAL PLAYER 2!");
        _RoundReset.Reset();
    }

}
