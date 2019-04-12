using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortelAuContact : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerLife>().Hit();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerLife>().Hit();
        }
    }
}
