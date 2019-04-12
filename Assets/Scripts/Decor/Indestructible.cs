using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indestructible : MonoBehaviour {

    public GameObject impact;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Shot" || other.gameObject.tag == "EnemyShot")
        {
            if (other.gameObject.tag == "Shot")
            {
                if (impact != null)
                {
                    Quaternion r = Quaternion.Euler(new Vector3(0f, other.gameObject.transform.rotation.eulerAngles.y + 180, 0f));
                    GameObject newShot = Instantiate(impact, other.gameObject.transform.position, r);
                    Destroy(newShot, 1f);
                }
            }
            Destroy(other.gameObject);
        }
    }
}
