using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBall : MonoBehaviour
{
    private Rigidbody2D m_Rigidbody2D;
    public float ForceX = 0.2f;
    public float ForceY = 0.2f;
    public float Gravity = 1;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
        m_Rigidbody2D.AddForce(new Vector2(ForceX,ForceY));

    }

    // Update is called once per frame
    void Update()
    {
        
        //m_Rigidbody2D.gravityScale += Gravity;
    }
}
