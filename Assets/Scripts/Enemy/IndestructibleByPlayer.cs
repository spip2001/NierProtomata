using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndestructibleByPlayer : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Shot")
        {
            Destroy(other.gameObject);
        }
    }
}
