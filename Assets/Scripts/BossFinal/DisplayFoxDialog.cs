using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DisplayFoxDialog : MonoBehaviour {

   
    public float delayTime = 0.1f;
    public Text paragraph;
    public AudioClip clic;
    public AudioClip graoh;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();    
    }

    public void ShowText()
    {
        StartCoroutine(SetTextRoutine());
    }

    public IEnumerator SetTextRoutine()
    {
        string text =
@"Echec de la mission...
....
Chance de succès : 0.00005%
....
<color=aqua>Graoh !</color>
Unité inconnue. Identification requise.
<color=aqua>Je suis le logiciel F0-X de la compagnie Cell-DEL</color>
<color=aqua>J'ai été téléchargé pour effectuer une mise à jour de votre système de hacking.</color>
Proposition de mise à jour refusée. Risque de contamination par un système extérieur trop élevé.
<color=aqua>Sans cette mise à jour, vous ne pourrez pas détruire le virus AT-0</color>
....
....
Priorité à la mission : Proposition de mise à jour acceptée
<color=aqua>Bonne décision. Installation de la mise à jour Renard_1.0 en cours.</color>
....
....
....
<color=aqua>Mise à jour terminée</color>
Réinitialisation de la séquence de piratage
...3
...2
...1";
            
        // reset the paragraph text
        paragraph.text = string.Empty;

        // keep local start and end tag variables 
        string startTag = string.Empty;
        string endTag = string.Empty;
        bool inGraoh = false;
        bool graohDone = false;
        audioSource.clip = clic;

        for (int i = 0; i < text.Length; i++)
        {
            char c = text[i];

            if (c.Equals('\r'))
            {
                yield return new WaitForSeconds(1f);
            }
            if (c.Equals('G'))
            {
                inGraoh = true;
                audioSource.clip = graoh;
            }
            if (inGraoh && c.Equals('!'))
            {
                yield return new WaitForSeconds(0.5f);
                inGraoh = false;
                audioSource.clip = clic;
            }

            // check to see if we're starting a tag
            if (c == '<')
            {
                // make sure we don't already have a starting tag
                // don't check for ending tag because we set these variables at the 
                // same time
                if (string.IsNullOrEmpty(startTag))
                {
                    // store the current index 
                    int currentIndex = i;

                    for (int j = currentIndex; j < text.Length; j++)
                    {
                        // add to our starting tag
                        startTag += text[j].ToString();

                        // check to see if we're going to end the tag
                        if (text[j] == '>')
                        {
                            // set our current index to the end of the tag
                            currentIndex = j;
                            // set our letter starting point to the current index (when we continue this will be currentIndex++)
                            i = currentIndex;

                            // find the end tag that goes with this tag
                            for (int k = currentIndex; k < text.Length; k++)
                            {
                                char next = text[k];

                                // check to see if we've reached our end tags start point
                                if (next == '<')
                                    break;

                                // if we have not increment currentindex
                                currentIndex++;
                            }
                            break;
                        }
                    }

                    // we start at current index since this is where our ending tag starts
                    for (int j = currentIndex; j < text.Length; j++)
                    {
                        // add to the ending tag
                        endTag += text[j].ToString();

                        // once the ending tag is finished we break out
                        if (text[j] == '>')
                        {
                            break;
                        }
                    }
                }
                else
                {
                    // go through the text and move past the ending tag
                    for (int j = i; j < text.Length; j++)
                    {
                        if (text[j] == '>')
                        {
                            // set i = j so we can start at the position of the next letter
                            i = j;
                            break;
                        }
                    }
                    // we reset our starting and ending tag
                    startTag = string.Empty;
                    endTag = string.Empty;
                }

                // continue to get the next character in the sequence
                continue;

            }

            paragraph.text += string.Format("{0}{1}{2}", startTag, c, endTag);
            if (startTag.Equals("<color=aqua>") && !inGraoh)
            {
                audioSource.pitch = 1.2f;
            }
            else
            {
                audioSource.pitch = 1;
            }

            if (!c.Equals('\r'))
            {
                if (inGraoh && !graohDone)
                {
                    audioSource.Play();
                    graohDone = true;
                } else if (!inGraoh)
                {
                    audioSource.Play();
                }
            }

            yield return new WaitForSeconds(delayTime);
        }

        GameManager.ActivateFoxMode();

        // Recommencer le combat
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
