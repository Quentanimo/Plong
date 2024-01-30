using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class PlayClipOnCollision : MonoBehaviour
{

   //plays a single audio clip whenever a collision with the ball occurs

    private AudioSource m_Audio;
    // Start is called before the first frame update
    void Start()
    {
        m_Audio = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Respawn"))
        {
            m_Audio.Play();
        }
    }
}
