using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotating : MonoBehaviour {

    public float rotationSpeed;

    private Rigidbody rb;
    private Vector3 angleVelocity;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        angleVelocity = new Vector3(0, rotationSpeed, 0);
	}
	
	// Update is called once per frame
	void Update () {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(angleVelocity * Time.deltaTime));
    }
}
