﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingPlayer : MonoBehaviour {

    public float speed = 4f;

    private Transform player;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
    }
	
	void Update () {
        if (player != null)
        {
            float angle = Mathf.Atan2(player.transform.position.x - transform.position.x, player.transform.position.z - transform.position.z) * Mathf.Rad2Deg;
            
            rb.MoveRotation(Quaternion.Lerp(rb.rotation, Quaternion.Euler(new Vector3(0f, angle, 0f)), 0.05f));
            rb.velocity = rb.rotation * Vector3.forward * speed;
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        }
    }
}

