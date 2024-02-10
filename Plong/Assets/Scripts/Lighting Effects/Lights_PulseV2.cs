using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

/*=====================================================================================================================================================
 * ------------------------------------------------------LIGHTS PULSE VERSION 2------------------------------------------------------------------------
 * 
 * This is a rework of the Lights Pulse script
 * Like its mama, it will cause a light to gradually swell and shrink in intensity
 * key differences between the V1 and V2 are that:
 * 1. Ver. 2 is more lightweight
 * 2. Ver. 2 lower intensity boundary is fixed at zero (this is the main trade off resulting from using Mathf.PingPong)
 * if you would like the intensity to always be at least some value greater than zero, you should use Lights Pulse V1
 */


public class Lights_PulseV2 : MonoBehaviour
{
    //-----------------------Component Vars-----------------------------
    private Light2D LightComponent = null;        //light component for URP lighting engine
    private bool ComponentWasFound = false;       //enables updates to occur, set true once found

    //-----------------------Adjustable Settings-------------------------
    [SerializeField] private float TransSpeed = 1f;                     //Transition Speed, greater than 1 = faster, less than 1 = slower
    [SerializeField] private float IntensityMax = 1f;                   //Max intensity the light will reach


    // Start is called before the first frame update
    void Start()
    {
        LightComponent = gameObject.GetComponent<Light2D>();
        if (LightComponent != null)
        {
            ComponentWasFound = true;
        }
        else
        {
            Debug.Log("Light Component was not found on object" + gameObject.name);
            Debug.Log("Are you sure you're using the URP lighting system?");
        }

    }

    private void Update()
    {
        //enables script if component was found
        //also checks to make sure that a valid transition speed value has been entered. Don't want to find out what happens if you try going into negative time on this
        if (ComponentWasFound & TransSpeed > 0)
        {
            LightComponent.intensity = Mathf.PingPong(Time.time*TransSpeed, IntensityMax);
        }
        else
        {
            Debug.Log("Check that TransSpeed is greater than 0 on" + gameObject.name);
        }

    }
}
