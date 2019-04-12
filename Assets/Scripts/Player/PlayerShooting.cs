using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

    public GameObject shot;
    public Transform shootSpawner;
    public float fireDelta = 0.5F;
    public float shotSpeed = 1f;
    public AudioClip shootClip;

    private float sincelastFire;
    private AudioSource audioSource;

    // Use this for initialization
    void Start () {
        sincelastFire = fireDelta;
        audioSource = GetComponent<AudioSource>();
    }
	
	
	void FixedUpdate () {
        sincelastFire += Time.deltaTime;

		if ((Input.GetButton("Fire1") || Input.GetAxis("Fire1") < -0.1)  && sincelastFire >= fireDelta)
        {
            sincelastFire = 0F;
            GameObject newShot = Instantiate(shot, shootSpawner.position, shootSpawner.rotation);
            newShot.GetComponent<Rigidbody>().velocity = shootSpawner.forward * shotSpeed;

            audioSource.clip = shootClip;
            audioSource.Play();
        } 

	}
}
