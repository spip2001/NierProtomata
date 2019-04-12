using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialGlow : MonoBehaviour {

    public Color startColor;
    public Color endColor;
    public float glowDuration = 1f;

    private MeshRenderer mr;
    private float curr = 0f;
    private int sens = 1;

	// Use this for initialization
	void Start () {
        mr = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        mr.material.color = Color.Lerp(startColor, endColor, curr / glowDuration);
        curr += sens * Time.deltaTime;
        if (curr > glowDuration && sens == 1)
        {
            sens = -1;
        } else if (curr <= 0 && sens == -1)
        {
            sens = 1;
        }
	}

}
