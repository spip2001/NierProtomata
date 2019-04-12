using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager instance = null;

    private Vector3 playerLastPositionInHub = new Vector3(0, 0.25f, 0);
    private Vector3 cameraLastPositionInHub = new Vector3(0, 13.5f, -8.81f);

    private List<string> completedLevel = new List<string>(50);

    private char lastResult = ' ';

    private bool foxMod;
    
    void Awake()
    {
        if (instance == null)
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

    }

    public static void DestroyInstance()
    {
        if (GameManager.instance != null)
        {
            Destroy(GameManager.instance.gameObject);
        }
    }

    public static int GetNbCompletedLevels()
    {
        return instance.completedLevel.Count;
    }
	
	public static void SetPlayerLastPositionInHub(Vector3 position)
    {
        instance.playerLastPositionInHub = position;
    }

    public static Vector3 GetPlayerLastPositionInHub()
    {
        return instance.playerLastPositionInHub;
    }

    public static void SetCameraLastPositionInHub(Vector3 position)
    {
        instance.cameraLastPositionInHub = position;
    }

    public static Vector3 GetCameraLastPositionInHub()
    {
        return instance.cameraLastPositionInHub;
    }

    public static void LevelCompleted(string levelNum)
    {
        if (!instance.completedLevel.Contains(levelNum))
        {
            instance.completedLevel.Add(levelNum);
        }
    }

    public static string GetLastCompletedLevel()
    {
        return instance.completedLevel[instance.completedLevel.Count - 1];
    }

    public static bool IsLevelCompleted(string levelNum)
    {
        return instance.completedLevel.Contains(levelNum);
    }

    public static void SetLastResultVictory()
    {
        instance.lastResult = 'V';
    }

    public static void SetLastResultFailure()
    {
        instance.lastResult = 'F';
    }

    public static bool WasVictory()
    {
        return instance.lastResult.Equals('V');
    }

    public static bool NeverPlayed()
    {
        return instance.lastResult.Equals(' ');
    }

    public static void ActivateFoxMode()
    {
        instance.foxMod = true;
    }

    public static bool IsFoxMode()
    {
        return instance.foxMod;
    }

    
}
