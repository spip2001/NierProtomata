using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsDisplay : MonoBehaviour {

    public Material joystick;
    public Material keys;

	// Use this for initialization
	void Start () {
        if (Input.GetJoystickNames().Length > 0)
        {
            transform.Find("Img").gameObject.GetComponent<MeshRenderer>().material = joystick;
        }
        else
        {
            transform.Find("Img").gameObject.GetComponent<MeshRenderer>().material = keys;
        }

    }
	
	
}
