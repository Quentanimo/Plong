using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundReset : MonoBehaviour
{
    [SerializeField] private GameObject Ball;

    private void Start()
    {
        
    }

    public void Reset()
    {
        //Ball = GameObject.FindGameObjectWithTag("Respawn");
        Instantiate(Ball);
    }
}
