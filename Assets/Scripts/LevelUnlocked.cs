using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUnlocked : MonoBehaviour
{
    public void LevelUnlock(int nextLevel)
    {
        PlayerPrefs.SetInt("LevelUnlocked", nextLevel);
    }
}
