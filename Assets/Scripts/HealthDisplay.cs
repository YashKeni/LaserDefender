using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    Text healthText;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        healthText = GetComponent<Text>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()   
    {
        int playerHealth = player.GetHealth();
        if (playerHealth <= 0)
        {
            healthText.text = 0f.ToString();
        }
        else
        {
            healthText.text = playerHealth.ToString();
        }
        //healthText.text = player.GetHealth().ToString();
    }
}
