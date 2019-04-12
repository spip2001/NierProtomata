using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeFollowPlayer : MonoBehaviour {

    private Transform player;
    private Transform eyeL;
    private Transform eyeR;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        eyeL = transform.Find("Model3D/Oeil.L");
        eyeR = transform.Find("Model3D/Oeil.R");
    }
	
	// Update is called once per frame
	void Update () {
        // Rotation de la tête
        transform.rotation = Quaternion.Euler(new Vector3(0f, Mathf.Cos(Time.time / 2f) * 25, 0f));

        // Rotation des yeux 140/185
        eyeL.LookAt(player);
        eyeR.LookAt(player);

        if (eyeL.eulerAngles.y <= 140)
        {
            transform.Rotate(new Vector3(0, eyeL.eulerAngles.y - 140, 0));
        }
        else if (eyeL.eulerAngles.y >= 185)
        {
            transform.Rotate(new Vector3(0, eyeL.eulerAngles.y - 185, 0));
        }
        if (eyeL.eulerAngles.x >= 20 && eyeL.eulerAngles.x <= 90)
        {
            transform.Rotate(new Vector3(20 - eyeL.eulerAngles.x, 0));
        }

        if (eyeL.eulerAngles.x <= 350 && eyeL.eulerAngles.x >= 180)
        {
            transform.Rotate(new Vector3(350 - eyeL.eulerAngles.x, 0));
        }

        eyeL.LookAt(player);
        eyeR.LookAt(player);

    }
}
