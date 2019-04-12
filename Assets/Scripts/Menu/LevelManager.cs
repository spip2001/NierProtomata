using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public string levelNum;

    private GameObject player;

    private bool finished = false;


	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

        if (!finished)
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                StartCoroutine(Victory());
                finished = true;
            }

            if (player == null)
            {
                StartCoroutine(Dead());
                finished = true;
            }
        }
	}

    IEnumerator Dead()
    {
        GameManager.SetLastResultFailure();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Hub");
    }

    IEnumerator Victory()
    {
        GameManager.LevelCompleted(levelNum);
        GameManager.SetLastResultVictory();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Hub");
    }

   
}
