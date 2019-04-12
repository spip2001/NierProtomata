using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExplosion : MonoBehaviour {

    public float sphereMaxSize = 5f;
    public float animDuration = 0.5f;

    private Transform sphere;

    private float sinceStart = 0.0f;

	// Use this for initialization
	void Start () {
        sphere = transform.Find("Sphere");
        sphere.localScale = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
        float size = Mathf.Lerp(0, sphereMaxSize, sinceStart / animDuration);
        sphere.localScale = new Vector3(size, size, size);

        sinceStart += Time.deltaTime;

        if (sinceStart >= animDuration)
        {
            Destroy(gameObject);
        }
	}
}
