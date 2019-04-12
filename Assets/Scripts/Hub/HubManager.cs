using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HubManager : MonoBehaviour {

    
    private DialogTextAppear dta;
    private int nbFiles;
   
    

	// Use this for initialization
	void Start () {
        GameObject.FindGameObjectWithTag("Player").transform.position = GameManager.GetPlayerLastPositionInHub();
        GameObject.FindGameObjectWithTag("MainCamera").transform.position = GameManager.GetCameraLastPositionInHub();

        nbFiles = GameObject.FindGameObjectsWithTag("File").Length;

        dta = GameObject.FindGameObjectWithTag("Dialog").GetComponent<DialogTextAppear>();

        if (GameManager.NeverPlayed())
        {
            dta.text = "-- Logiciel de piratage et de réparation M0-GL1 initialisé\r\nMission : recherche de la cause du dysfonctionnement du serveur WILLY-01\r\n......\r\nSecteur défectueux détecté à l'adresse 0x05BDFF6E\r\n.....\r\n" + nbFiles + " blocs à analyser\r\nDébut de procédure";
            dta.ShowText();
        }
        else if (GameManager.WasVictory())
        {
            dta.text = "Bloc " + GameManager.GetLastCompletedLevel() + " réparé";
            dta.text += "\r\n" + (nbFiles - GameManager.GetNbCompletedLevels()) + " bloc(s) restant(s)";
            dta.ShowText();
        }
        else if (GameManager.IsFoxMode())
        {
            dta.text = "Destruction de RENARD_1.0 par virus AT-0\r\n";
            dta.text += "\r\nMessage personnel au joueur : t'es vraiment mauvais...";
            dta.ShowText();
        }
        else
        {
            dta.text = "Réparation du bloc : Echec";
            dta.ShowText();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
        


}
