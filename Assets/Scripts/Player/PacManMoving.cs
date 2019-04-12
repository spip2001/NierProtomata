using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacManMoving : MonoBehaviour {

    public GameObject particleEmit;

    private Rigidbody rb;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }

    public float speed;

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (moveHorizontal == 0 && moveVertical == 0)
        {
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX |RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            rb.velocity = Vector3.zero;
            particleEmit.SetActive(false);
            rb.MoveRotation(Quaternion.Euler(Vector3.forward));
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            Vector3 movement = new Vector3(
                Mathf.Abs(moveHorizontal) >= Mathf.Abs(moveVertical) ? moveHorizontal : 0, 
                0.0f,
                Mathf.Abs(moveVertical) > Mathf.Abs(moveHorizontal) ? moveVertical : 0);
            rb.velocity = movement * speed;

            rb.MoveRotation(Quaternion.FromToRotation(Vector3.forward, rb.velocity.normalized));

            particleEmit.SetActive(true);
        }
        
        
        
    }

}
