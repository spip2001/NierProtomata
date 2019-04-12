using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : MonoBehaviour {

    public Transform farEnd;
    private Vector3 frometh;
    private Vector3 untoeth;
    public float speed = 2f;

    private Rigidbody rb;

    void Start()
    {
        frometh = transform.position;
        untoeth = farEnd.position;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 v = untoeth - transform.position;
        if (v.magnitude < 1f)
        {
            Vector3 t = untoeth;
            untoeth = frometh;
            frometh = t;
            v = (untoeth - transform.position);
        }
        rb.velocity = v.normalized * speed;
    }
}
