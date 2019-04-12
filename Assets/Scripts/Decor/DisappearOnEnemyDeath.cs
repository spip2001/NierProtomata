using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearOnEnemyDeath : MonoBehaviour {

    public int nbEnemyLeft = 1;
   

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == nbEnemyLeft)
        {
            Destroy(gameObject);
        }
    }
}
