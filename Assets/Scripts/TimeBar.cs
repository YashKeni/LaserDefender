using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;
using UnityEngine.SceneManagement;

public class TimeBar : MonoBehaviour
{
    [Header("Time Bar")]
    [SerializeField] public GameObject timeBar;
    [SerializeField] public int time;

    [Header("Spaceship on Time Bar")]
    [SerializeField] public GameObject spaceShipImage;
    [SerializeField] public int spaceShipImageSpeed;

    [Header("Level Settings")]
    [SerializeField] public float delayToLoadNextLevel = 5f;

    [Header("Enemy Spawner to stop working")]
    [SerializeField] public GameObject enemySpawner;
    [SerializeField] public GameObject levelPassed;
    
    public bool isPassed = false;

    void Start()
    {
        StartTimeBar();
        levelPassed.SetActive(false);

    }

    void Update()
    {
        
    }

    public void StartTimeBar()
    {
        LeanTween.scaleX(timeBar, 1, time).setOnComplete(ActivateNextLevel);
        LeanTween.moveLocalX(spaceShipImage, 530, spaceShipImageSpeed);
    }

    void ActivateNextLevel()
    {
        isPassed = true;
        StartCoroutine(WaitAndLoad());
    }

    IEnumerator WaitAndLoad()
    {
        enemySpawner.SetActive(false);
        yield return new WaitForSeconds(delayToLoadNextLevel);
        
        // --------- Level direct load -------------------------
        // int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // SceneManager.LoadScene(currentSceneIndex + 1);
        // Debug.Log("Next Level");   
        // -----------------------------------------------------

        // -------- Level Passed Popup -------------------------
        levelPassed.SetActive(true);

    }
}
