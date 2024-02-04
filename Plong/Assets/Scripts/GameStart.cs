using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{

    public RoundReset _RoundReset;
    // Start is called before the first frame update
    void Start()
    {
        _RoundReset.Reset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
