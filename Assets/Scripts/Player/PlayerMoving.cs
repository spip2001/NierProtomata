using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour {

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
        Rotate();
    }

    private void Rotate()
    {
        if (Input.GetJoystickNames().Length > 0)
        {
            float aimHorizontal = Input.GetAxis("Horizontal_Aim");
            float aimVertical = Input.GetAxis("Vertical_Aim");
            float angle = Mathf.Atan2(aimHorizontal, aimVertical) * Mathf.Rad2Deg;
            rb.MoveRotation(Quaternion.Euler(new Vector3(0f, angle, 0f)));
        }
        else
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("Sol")))
            {
                Vector3 hitPoint = hit.point;
                float angle = Mathf.Atan2(hitPoint.x - transform.position.x, hitPoint.z - transform.position.z) * Mathf.Rad2Deg;
                rb.MoveRotation(Quaternion.Euler(new Vector3(0f, angle, 0f)));
            }
        }
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


        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            rb.velocity = movement * speed;
            particleEmit.SetActive(true);
        }
        
        
        
    }

}
