using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossLifeBar : MonoBehaviour {

    private Slider slider;
    private DestructibleByPlayer boss;

    // Use this for initialization
    void Start () {
        slider = transform.Find("Slider").GetComponent<Slider>();
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<DestructibleByPlayer>();

        slider.maxValue = boss.lifePoints;
        slider.value = boss.lifePoints;
	}
	
	// Update is called once per frame
	void Update () {
        slider.value = boss.lifePoints;
    }
}
