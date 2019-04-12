using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour {

    public int life = 3;
    public float invulnerableDuration = 0.5f;
    public GameObject deathEffect;
    public GameObject particleEmit;


    private bool invulnerable;
    private float invulnerableSince = 0.0f;
    private BattleCameraController ctrl;
    private CameraShake cs;


    // Use this for initialization
    void Start () {
        ctrl = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BattleCameraController>();
        cs = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
    }

    public void Hit()
    {
        if (!invulnerable)
        {
            life--;

            if (ctrl != null)
            {
                ctrl.Shake();
            } else if (cs != null)
            {
                cs.shakeDuration = 0.5f;
            }

            if (life == 2)
            {
                transform.Find("Player/Vaisseau_L").transform.localScale = Vector3.zero;
            } else if (life == 1)
            {
                transform.Find("Player/Vaisseau_R").transform.localScale = Vector3.zero;
            }
            
            if (life <= 0)
            {
                Dead();
            }
            else
            {
                invulnerable = true;
                invulnerableSince = 0.0f;
            }
        }
    }

    private void Update()
    {
        if (invulnerable)
        {
            invulnerableSince += Time.deltaTime;
        }
        if (invulnerableSince >= invulnerableDuration)
        {
            invulnerable = false;
        }
    }

    void Dead()
    {
        Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
