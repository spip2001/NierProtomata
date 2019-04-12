using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleByPlayer : MonoBehaviour {

    public GameObject objectToDestroy;

    public int lifePoints = 5;
    public Material effectMaterial;
    public float invulnerabilityDuration = 0.5f;
    public GameObject mesh;
    public GameObject impact;

    private float invulnerabilityTime;
    private Material defaultMaterial;

    void Start()
    {
        defaultMaterial = mesh.GetComponent<MeshRenderer>().material;
        invulnerabilityTime = invulnerabilityDuration;
    }

    private void Update()
    {
        invulnerabilityTime += Time.deltaTime;
        if (invulnerabilityTime >= invulnerabilityDuration)
        {
            mesh.GetComponent<MeshRenderer>().material = defaultMaterial;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Shot")
        {
            if (impact != null)
            {
                Quaternion r = Quaternion.Euler(new Vector3(0f, other.gameObject.transform.rotation.eulerAngles.y + 180, 0f));
                GameObject newShot = Instantiate(impact, other.gameObject.transform.position, r);
                Destroy(newShot, 1f);
            }
            Destroy(other.gameObject);

            if (invulnerabilityTime >= invulnerabilityDuration)
            {
                lifePoints--;
                mesh.GetComponent<MeshRenderer>().material = effectMaterial;
                invulnerabilityTime = 0;

                if (lifePoints <= 0)
                {
                    if (objectToDestroy == null)
                    {
                        Destroy(this.gameObject);
                    }
                    else
                    {
                        Destroy(objectToDestroy);
                    }
                }
            }
        }
        
    }
}
