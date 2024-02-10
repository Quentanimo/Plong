using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class PlayClipOnTrigger : MonoBehaviour
{
    /* ==========================================================================================================
     * -------------------------------------Play Clip on Collision-----------------------------------------------
     * 
     * Script that cues the SFXplayer under AudioMaster parent object to play a clip on collision with the ball
     * Script can be added into any game object with collider
     * Script needs audio clips to be dragged and dropped into the Clip array
     * Clipselect selects which clip should play on non-randomized play
     * for random play to work at least 2 clips must be loaded into the clip array
     * ==========================================================================================================
     */

    //-----------------------------------------Declarations------------------------------------------------------
    private GameObject _AudioMaster;   //Object that controls Audio settings
    private SFXClips SFXPlayer;        //Script that exists on SFX child of AudioMaster
    public AudioClip[] Clip;           //Array of Clips, add whatever audio clips you'd like to the array

    [SerializeField]
    private
    bool Filter = false;                //Choose whether or not to filter collisions by tag

    [SerializeField]
    private            //Add a tag so SFX is only triggered by collision with objects that share tag
    string FilterByTag = "MainCamera";          //leave "MainCamera" to disable filtering. Tried Null and unassigned Tags, Unity simply just won't let it fly, 

    [SerializeField]
    private            //for non-randomized plays, 
    int ClipSelect = 0;                 //select which clip should play by its number in the array

    [SerializeField]
    private            //check this box to enable script to randomly select a clip from the array
    bool RandomClip = false;            //to play each time a collision is triggered on this object
                                        //-----------------------------------------------------------------------------------------------------------

    //--------------------------------------------Start----------------------------------------------------------
    void Start()
    {
        //find script SFXClips nested under audiomaster
        //this script essentially handles actually playing the audio clips that we feed it
        //Currently SFXClips has limited functionality but will have more use later
        _AudioMaster = GameObject.FindWithTag("Audio");
        SFXPlayer = _AudioMaster.transform.Find("SFX").GetComponent<SFXClips>();

    }
    //----------------------------------------------------------------------------------------------------------

    //----------------------------------Collision Detection-----------------------------------------------------
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check to see if a tag has been given to filter by
        if (Filter)
        {
            //check to see if collision is with the object specified by FilterByTag
            if (collision.gameObject.CompareTag(FilterByTag))
            {
                PlaySFX(); //see function below
            }
        }
        //no tag, no filter, play that baby everytime
        else
        {
            PlaySFX(); //see function below
        }
    }
    //---------------------------------------------------------------------------------------------------------

    //---------------------------------------PlaySFX()---------------------------------------------------------
    private void PlaySFX()
    {
        //check to make sure that the array has enough clips to be randomized
        //and prevent Clip[1] being indexed with no element there
        if (Clip.Length > 1 & RandomClip)
        {
            SFXPlayer.PlaySFX(Clip[Random.Range(0, Clip.Length)]);
        }

        // make sure the manually entered clip select number is compatible with the array
        else if (ClipSelect <= Clip.Length)
        {
            SFXPlayer.PlaySFX(Clip[ClipSelect]);
            //send the clip at the index specified to Clip
        }

        else
        {
            Debug.Log("SFX Error: Clip[] index is outside of range of the array. Please check" + gameObject.name);
        }
    }
    //---------------------------------------------------------------------------------------------------------
}
