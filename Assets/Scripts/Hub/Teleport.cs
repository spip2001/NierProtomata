using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{

    public string levelNum;

    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            GameManager.SetPlayerLastPositionInHub(player.transform.position - new Vector3(0,0,1));

            GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
            GameManager.SetCameraLastPositionInHub(cam.transform.position);

            SceneManager.LoadScene("Level_" + levelNum);
        }
    }
}
