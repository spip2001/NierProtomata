using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour
{

    public Material completedMaterial;

    public string levelNum;

    void Start()
    {
        transform.Find("Lbl").gameObject.GetComponent<TextMesh>().text = levelNum;

        if (GameManager.IsLevelCompleted(levelNum))
        {
            Destroy(transform.Find("FileBug").gameObject);
            transform.Find("File").gameObject.GetComponent<Renderer>().material = completedMaterial;
        }

       
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Shot")
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            GameManager.SetPlayerLastPositionInHub(player.transform.position);

            GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
            GameManager.SetCameraLastPositionInHub(cam.transform.position);


            SceneManager.LoadScene("Level_" + levelNum);
        }
    }
}
