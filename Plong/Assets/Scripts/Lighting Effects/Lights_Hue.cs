using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Lights_Hue : MonoBehaviour
{
    //-----------------------Component Vars-----------------------------
    private Light2D LightComponent = null;        //light component for URP lighting engine
    private bool ComponentWasFound = false;       //enables updates to occur, set true once found

    //-----------------------Adjustable Settings-------------------------
    [SerializeField] private float TransSpeed = 1f;                     //Transition Speed, greater than 1 = faster, less than 1 = slower
    [SerializeField] private Color InitialColor = Color.white;          //Color at time=0
    [SerializeField] private Color TransitionaryColor = Color.white;    //Color to transition towards



    // Start is called before the first frame update
    void Start()
    {
        LightComponent = gameObject.GetComponent<Light2D>();
        if (LightComponent != null)
        {
            ComponentWasFound = true;
            LightComponent.color = InitialColor;
        }
        else
        {
            Debug.Log("Light Component was not found on object" + gameObject.name);
            Debug.Log("Are you sure you're using the URP lighting system?");
        }

        //check for if no transition has been set
        //no sense in wasting calculations on interpolating if the script isn't being used
        if (InitialColor == TransitionaryColor)
        {
            Debug.Log("Lights_Hue.cs: There is no transition set, if no color transition is desired please remove or disable Lights_Hue.cs on " + gameObject.name);
        }
    
    }

    private void Update()
    {
        //enables script if component was found
        //also checks to make sure that a valid transition speed value has been entered. Don't want to find out what happens if you try going into negative time on this
        if (ComponentWasFound & TransSpeed>0)
        {
                LightComponent.color = Color.Lerp(InitialColor, TransitionaryColor, Mathf.PingPong(Time.time*TransSpeed, 1));   
         }
        else
        {
            Debug.Log("Check that TransSpeed is greater than 0 on" + gameObject.name);
        }

    }


}
