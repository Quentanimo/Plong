using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundReset : MonoBehaviour
{
    [SerializeField] private GameObject Ball;
    public float SpawnYMaxValue = 4; // Sets what the max Y value will be used when generating a random value for ball start position. For example. If set to 4, ball will spawn between vector position -4 and +4

    private void Start()
    {
        
    }

    public void Reset()
    {
        //Ball = GameObject.FindGameObjectWithTag("Respawn");
        Instantiate(Ball, new Vector3(0f, Random.Range(-SpawnYMaxValue, SpawnYMaxValue),0),Quaternion.identity);

    }
}
