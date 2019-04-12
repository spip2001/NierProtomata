using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalBossManager : MonoBehaviour {

    public AudioClip music;
    public AudioClip intro;
    public AudioClip introFox;
    public AudioClip victory;
    public Vector3 bossOriginalPosition;
    public float bossAppearDur = 3f;
    private bool fox = false;
    public GameObject foxPlayer;
    public GameObject bossPrefab;

    private AudioSource audioSource;
    private GameObject boss;
    private Vector3 bossFinalPosition;
    private Vector3 playerOriginalPosition;
    private EnemyShooting bossShooting;
    private DestructibleByPlayer bossLife;
    private GameObject player;
    private GameObject ui;
    private Light mainLight;
    private float mainLightIntensity;
    private GameObject foxUI;

    private bool fightStarted = false;
    private float sinceStart = 0f;
    private int originalBossLife;

	// Use this for initialization
	void Start () {
        fightStarted = false;
        audioSource = GetComponent<AudioSource>();
        boss = GameObject.FindGameObjectWithTag("Boss");
        bossFinalPosition = boss.transform.position;
        bossShooting = boss.GetComponent<EnemyShooting>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerOriginalPosition = player.transform.position;
        ui = GameObject.FindGameObjectWithTag("UI");
        mainLight = GameObject.FindGameObjectWithTag("MainLight").GetComponent<Light>();
        bossLife = boss.GetComponent<DestructibleByPlayer>();
        originalBossLife = bossLife.lifePoints;
        foxUI = transform.Find("FoxUI").gameObject;

        foreach (GameObject shot in bossShooting.shots)
        {
            shot.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        foxUI.SetActive(false);

        boss.transform.position = bossOriginalPosition;
        bossShooting.enabled = false;
        player.GetComponent<PlayerShooting>().enabled = false;
        ui.SetActive(false);
        mainLightIntensity = mainLight.intensity;
        mainLight.intensity = 0f;

        try
        {
            fox = GameManager.IsFoxMode();
        }
        catch (NullReferenceException e)
        {
            fox = false;
        }

        audioSource.clip = fox ? introFox : intro;
        audioSource.Play();

        if (fox)
        {
            player.transform.Find("Player").transform.localScale = Vector3.zero;
            player.GetComponent<PlayerLife>().life = 50;
            player.GetComponent<PlayerLife>().invulnerableDuration = 2f;
            player.GetComponent<PlayerShooting>().fireDelta = 0.05f;
            player.GetComponent<PlayerShooting>().shotSpeed = 40f;
        }
        else
        {
            player.transform.Find("Fox").transform.localScale = Vector3.zero;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (fightStarted && !audioSource.isPlaying)
        {
            audioSource.clip = music;
            audioSource.Play();
        }

        if (player != null && !fightStarted && boss != null)
        {
            boss.transform.position = Vector3.Lerp(bossOriginalPosition, bossFinalPosition, sinceStart / bossAppearDur);
            mainLight.intensity = Mathf.Lerp(0, mainLightIntensity, sinceStart / bossAppearDur);

            if (sinceStart > bossAppearDur)
            {
                fightStarted = true;
                bossShooting.enabled = true;
                player.GetComponent<PlayerShooting>().enabled = true;
                ui.SetActive(true);
            }
        }

        if (fightStarted)
        {
            if (sinceStart <= 60 && !fox)
            {
                foreach (GameObject shot in bossShooting.shots)
                {
                    float s = Mathf.Clamp(((float)originalBossLife) / ((float)bossLife.lifePoints), 1f, 5f);
                    shot.transform.localScale = new Vector3(s, s, s);
                }
            }
            else if (!fox)
            {
                foreach (GameObject shot in bossShooting.shots)
                {
                    shot.transform.localScale = new Vector3(5f, 5f, 5f);
                }
            }
        }

        if (fightStarted && player == null && boss != null)
        {
            if (fox)
            {
                StartCoroutine(FoxDead());
            }
            else
            {
                PlayerDead();
            }
            fightStarted = false;
        }

        if (fightStarted && boss == null)
        {
            Victory();
        }
        

        sinceStart += Time.deltaTime;
	}

    private void Victory()
    {
        fightStarted = false;
        audioSource.Stop();
        audioSource.clip = victory;
        audioSource.Play();
        ui.SetActive(false);
        player.GetComponent<PlayerShooting>().enabled = false;
        player.GetComponent<FinalBossPlayerMoving>().enabled = false;
        Debug.Log(playerOriginalPosition);

        Rigidbody prb = player.GetComponent<Rigidbody>();
        prb.velocity = Vector3.zero;
        prb.MovePosition(playerOriginalPosition);
        mainLight.intensity = 1.2f;

        StartCoroutine(GoToVictoryScene());
    }

    private IEnumerator GoToVictoryScene()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Victory");
    }

    private void PlayerDead()
    {
        audioSource.Stop();
        fightStarted = false;
        ui.SetActive(false);
        foxUI.SetActive(true);
        foxUI.GetComponent<DisplayFoxDialog>().ShowText();
    }

    private IEnumerator FoxDead()
    {
        GameManager.SetLastResultFailure();
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Hub");
    }







}
