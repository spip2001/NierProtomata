using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanHitPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerLife>().Hit();
            Destroy(gameObject);
        }
    }
}
