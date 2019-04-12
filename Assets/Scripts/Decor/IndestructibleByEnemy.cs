using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndestructibleByEnemy : MonoBehaviour {

    public GameObject impact;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyShot")
        {
            Destroy(other.gameObject);
        }
    }
}
