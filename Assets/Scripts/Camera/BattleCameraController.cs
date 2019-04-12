using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCameraController : MonoBehaviour {

    public float minFieldOfView = 40f;
    public float maxFieldOfView = 60f;
    public float computeFoVStep = 5f;
    public float fovDeltaPerFrame = 1f;

    private GameObject player;
    private GameObject[] enemies;
    private Camera cam;
    private Vector3 offset;

    private float targetFieldOfView;
    private float lastFieldOfView;

    public float shakeDuration = 0.3f;
    public float shakeAmount = 1f;
    private float shakeFor = 0f;
    private bool shaking;
    

    // Use this for initialization
    void Start () {
        cam = GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        offset = transform.position - player.transform.position;
        targetFieldOfView = minFieldOfView;
        lastFieldOfView = minFieldOfView;
    }

    public void Shake()
    {
        shaking = true;
        shakeFor = 0;
    }

    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        if (player != null)
        {
            transform.position = player.transform.position + offset;
            if (shaking)
            {
                transform.position += Random.insideUnitSphere * shakeAmount;
                shakeFor += Time.deltaTime;

                if (shakeFor >= shakeDuration)
                {
                    shakeFor = 0;
                    shaking = false;
                }
            }
        }

        if (enemies != null && enemies.Length > 0)
        {
            lastFieldOfView = cam.fieldOfView;

            bool zoomOut = false;
            foreach (GameObject enemy in enemies)
            {
                zoomOut |= ZoomOutForEnemy(enemy);
            }
            if (!zoomOut)
            {
                ZoomInForEnemies();
            }
            if (cam.fieldOfView != lastFieldOfView)
            {
                cam.fieldOfView += 10f;
            }

            targetFieldOfView = cam.fieldOfView;
            cam.fieldOfView = lastFieldOfView;
        }

        if (targetFieldOfView > maxFieldOfView)
        {
            targetFieldOfView = maxFieldOfView;
        }

        if (cam.fieldOfView < targetFieldOfView)
        {
            cam.fieldOfView += fovDeltaPerFrame;
        } else if (cam.fieldOfView > targetFieldOfView)
        {
            cam.fieldOfView -= fovDeltaPerFrame;
        }

    }

    /**
     * Fais un zoom out pour voir l'ennemi. Retourne true si un zoom a effectivement été nécessaire. 
     */
    private bool ZoomOutForEnemy(GameObject enemy)
    {
        if (enemy == null)
        {
            return false;
        }
        Vector3 screenPoint = cam.WorldToViewportPoint(enemy.transform.position);
        bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        if (onScreen)
        {
            return false;
        }
        while (!onScreen)
        {
            cam.fieldOfView += computeFoVStep;
            screenPoint = cam.WorldToViewportPoint(enemy.transform.position);
            onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        }
        return true;
    }

    private void ZoomInForEnemies()
    {
        while (true)
        {
            if (cam.fieldOfView > minFieldOfView)
            {
                cam.fieldOfView -= computeFoVStep;
                foreach (GameObject enemy in enemies)
                {
                    if (enemy != null)
                    {
                        Vector3 screenPoint = cam.WorldToViewportPoint(enemy.transform.position);
                        bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
                        if (!onScreen)
                        {
                            cam.fieldOfView += computeFoVStep;
                            return;
                        }
                    }
                }
            }
            else
            {
                break;
            }
        }
    }
}
