using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoingRound : MonoBehaviour {

    public Vector3 initialForce;
    public Vector3 perSecondForce;

    private Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(Push());
    }

    private IEnumerator Push()
    {
        yield return new WaitForSeconds(0.5f);
        rb.AddForce(initialForce);
    }

    private void FixedUpdate()
    {
        rb.AddForce(rb.rotation * perSecondForce * Time.deltaTime);
    }
}
