using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastManStanding : MonoBehaviour {

    public GameObject shieldPrefab;
    public int nbEnemyLeft = 1;

    private GameObject shieldInstance;
    private bool invicible = true;

	// Use this for initialization
	void Start () {
        GetComponent<DestructibleByPlayer>().enabled = false;
        shieldInstance = Instantiate(shieldPrefab, transform);
	}
	
	// Update is called once per frame
	void Update () {
		if (invicible && GameObject.FindGameObjectsWithTag("Enemy").Length == nbEnemyLeft)
        {
            invicible = false;
            GetComponent<DestructibleByPlayer>().enabled = true;
            Destroy(shieldInstance);
        }
	}
}
