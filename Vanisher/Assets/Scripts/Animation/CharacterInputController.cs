using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputController : MonoBehaviour {

    private float filteredForwardInput = 0f;
    private float filteredTurnInput = 0f;

    public bool InputMapToCircular = true;

    public float forwardInputFilter = 5f;
    public float turnInputFilter = 5f;

    private float forwardSpeedLimit = 1f;

	private Animator anim;

    public float Forward
    {
        get;
        private set;
    }

    public float Turn
    {
        get;
        private set;
    }

	public bool Throw
	{
		get;
		set;
	}

	public bool Sneak
	{
		get;
		private set;
	}
        

	void FixedUpdate () {
		
        //GetAxisRaw() so we can do filtering here instead of the InputManager
        float h = Input.GetAxisRaw("Horizontal");// setup h variable as our horizontal input axis
        float v = Input.GetAxisRaw("Vertical"); // setup v variables as our vertical input axis


        if (InputMapToCircular)
        {
            // make coordinates circular
            //based on http://mathproofs.blogspot.com/2005/07/mapping-square-to-circle.html
            h = h * Mathf.Sqrt(1f - 0.5f * v * v);
            v = v * Mathf.Sqrt(1f - 0.5f * h * h);

        }


        //BEGIN ANALOG ON KEYBOARD DEMO CODE
//		if (Input.GetKeyDown (KeyCode.LeftShift)) {
//			Sneak = true;
//			forwardSpeedLimit = 2.0f;
//		}
//		else if (Input.GetKeyUp (KeyCode.LeftShift)) {
//			Sneak = false;
//			forwardSpeedLimit = 1.0f;
//		}
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			Sneak = !Sneak;
			if (Sneak) forwardSpeedLimit = 2.0f;
			else forwardSpeedLimit = 1.0f;
		}

		if (Input.GetKeyUp (KeyCode.T)) Throw = true;
		else Throw = false;
        //END ANALOG ON KEYBOARD DEMO CODE  


        //do some filtering of our input as well as clamp to a speed limit
        filteredForwardInput = Mathf.Clamp(Mathf.Lerp(filteredForwardInput, v, 
            Time.deltaTime * forwardInputFilter), -forwardSpeedLimit, forwardSpeedLimit);

        filteredTurnInput = Mathf.Lerp(filteredTurnInput, h, 
            Time.deltaTime * turnInputFilter);

        Forward = filteredForwardInput;
        Turn = filteredTurnInput;
	}
}
