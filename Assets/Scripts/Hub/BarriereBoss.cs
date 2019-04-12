using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarriereBoss : MonoBehaviour {

    private DialogTextAppear dta;
    private float lastMsgTime = 0f;

    // Use this for initialization
    void Start () {
        if (GameManager.GetNbCompletedLevels() == GameObject.FindGameObjectsWithTag("File").Length)
        {
            transform.Find("Mur").gameObject.SetActive(false);
        }

        dta = GameObject.FindGameObjectWithTag("Dialog").GetComponent<DialogTextAppear>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Time.time - lastMsgTime > 30)
            {
                dta.text = "Passage bloqué.\r\nSupposition : réparer tous les blocs ouvrira la voie.";
                dta.ShowText();
                lastMsgTime = Time.time;
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
