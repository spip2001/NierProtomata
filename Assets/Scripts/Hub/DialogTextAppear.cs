using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogTextAppear : MonoBehaviour {

    public string text = "";
    public float period = 0.2f;

    private Text t;
    private AudioSource source;
    private Coroutine currentCoroutine;

    // Use this for initialization
    void Awake () {
        t = transform.Find("Panel/InviteText").GetComponent<Text>();
        source = GetComponent<AudioSource>();
        ShowText();
    }

    public void ShowText()
    {
        if (currentCoroutine == null)
        {
            t.text = "";
            currentCoroutine = StartCoroutine(AnimateText(text));
        }
    }

    IEnumerator AnimateText(string strComplete)
    {
        int i = 0;
        string str = "";
        while (i < strComplete.Length)
        {
            str += strComplete[i++];
            t.text = str;

            float p = period;

            if (str[str.Length - 1].Equals('.'))
            {
                source.pitch = 1f;
                p = 0.5f;
            }
           
            else
            {
                source.pitch = Random.Range(0.8f, 1.2f);
            }

            source.Play();
            yield return new WaitForSeconds(p);
        }
        currentCoroutine = null;
    }


}
