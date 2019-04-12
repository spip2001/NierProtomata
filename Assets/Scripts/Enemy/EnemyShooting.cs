using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour {

    public GameObject[] shots;
    public Transform[] shootSpawners;
    public float shootDelay = 0.2f;
    public float shotSpeed = 1f;

    private float sincelastFire;
    private int currentShot = 0;
    private GameObject player;

    // Use this for initialization
    void Start () {
        sincelastFire = -shootDelay;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player != null)
        {
            sincelastFire += Time.deltaTime;

            if (sincelastFire >= shootDelay)
            {
                sincelastFire = 0F;
                foreach (Transform shootSpawner in shootSpawners)
                {
                    GameObject newShot = Instantiate(shots[currentShot], shootSpawner.position, shootSpawner.rotation);
                    newShot.GetComponent<Rigidbody>().velocity = shootSpawner.forward * shotSpeed;
                }

                currentShot++;
                if (currentShot >= shots.Length)
                {
                    currentShot = 0;
                }
            }
        }

    }
}
