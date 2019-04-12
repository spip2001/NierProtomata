using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoEffect : MonoBehaviour {

    private Outline outline;

	// Use this for initialization
	void Start () {
        outline = GetComponent<Outline>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 newVal = new Vector2(Mathf.Cos(Time.fixedTime) * 3f, Mathf.Sin(Time.fixedTime) * 3f);
        outline.effectDistance = newVal;

        float c = Mathf.Abs(Mathf.Cos(Time.fixedTime / 5));
        outline.effectColor = new Color(c, c, c);
	}
}
