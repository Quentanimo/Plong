using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;
using UnityEngine.Rendering.Universal;

/*=====================================================================================================================================================
 * ------------------------------------------------------LIGHTS PULSE VERSION 1------------------------------------------------------------------------
 * Ver. 1
 * This is the original, bulkier version of the Lights Pulse script
 * it will cause a light to gradually swell and shrink in intensity
 * key differences between the V1 and V2 are that:
 * 1. Ver. 2 is more lightweight
 * 2. Ver. 2 lower intensity boundary is fixed at zero (this is the main trade off resulting from using Mathf.PingPong)
 * if you would like the lights intensity to drop to 0 on the lower bound, you should use Lights Pulse V2
 */

public class Lights_Pulse : MonoBehaviour
{
    private Light2D LightComponent = null;                          //light component for URP lighting engine
   
    [SerializeField] private float IntensityIncrement = 0.005f;     //essentially how fast it will transition on each frame
    [SerializeField] private float IntensityUpperbound = 2;         //max intensity before the light begins dimming
    [SerializeField] private float IntensityLowerbound = 0.01f;     //min intensity before light begins brightenning

    private bool Waxing = true;                     //used by script to determine if it should be in a waxing or waning state
    private bool ComponentWasFound = false;
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
        if (ComponentWasFound)
        {

            //check to see if the script should transisition from a brightening state to a dimming state
            if (LightComponent.intensity > IntensityUpperbound)
            {
                Waxing = false;
            }
            else if (LightComponent.intensity < IntensityLowerbound)
            {
                Waxing = true;
            }

            //if in a waxing state, brighten. If in a waning state, dim. At a rate deteremined by intensityIncrement
            if (Waxing)
            {
                LightComponent.intensity += IntensityIncrement;
            }
            else
            {
                LightComponent.intensity -= IntensityIncrement;
            }

        }
        
    }

    
}
