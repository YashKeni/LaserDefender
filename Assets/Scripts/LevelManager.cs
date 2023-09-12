using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] public GameObject[] levelButton;

    private void Awake() 
    {
        if(PlayerPrefs.GetInt("LevelUnlocked") == 0)
        {
            PlayerPrefs.SetInt("LevelUnlocked", 1);
        }    
    }
    
    void Start()
    {
        for (int i = 0; i < levelButton.Length; i++)
        {
            levelButton[i].GetComponent<Button>().interactable = false;
        }

        for (int i = 1; i <= PlayerPrefs.GetInt("LevelUnlocked"); i++)
        {
            levelButton[i-1].GetComponent<Button>().interactable = true;
        }    
    }

    
}
