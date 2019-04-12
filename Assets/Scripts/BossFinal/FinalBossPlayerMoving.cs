using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossPlayerMoving : MonoBehaviour {

    public float speed = 5f;

    private Transform at0;
    private Rigidbody rb;
    private GameObject particleEmit;
    private Transform target;
    private Transform cam;

	// Use this for initialization
	void Start () {
        at0 = GameObject.FindGameObjectWithTag("Boss").transform;
        rb = GetComponent<Rigidbody>();
        particleEmit = transform.Find("PlayerParticleEmitter").gameObject;
        target = at0.transform.Find("Target").transform;
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }
	
	// Update is called once per frame
	void Update () {

        // On vise toujours le boss
        if (target != null)
        {
            transform.LookAt(target);
        }
        else
        {
            transform.LookAt(cam);
        }

        // Déplacement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (moveHorizontal == 0 && moveVertical == 0)
        {
            rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            rb.velocity = Vector3.zero;
            particleEmit.SetActive(false);


        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ;
            Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
            rb.velocity = movement * speed;
            particleEmit.SetActive(true);
        }
    }
}
