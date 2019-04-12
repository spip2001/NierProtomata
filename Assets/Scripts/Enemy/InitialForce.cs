using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialForce : MonoBehaviour {

    public Vector3 force;

    private Rigidbody rb;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(Push());
	}

    private IEnumerator Push()
    {
        yield return new WaitForSeconds(0.5f);
        rb.AddForce(force);
    }
	
	
}
