using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissileButton : MonoBehaviour
{
    [SerializeField] public GameObject missileButton;
    [SerializeField] public float missileCooldown = 15f;
    public bool isPressed = false;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isPressed == true)
        {
            StartCoroutine(ButtonPressed());
        }
    }

    IEnumerator ButtonPressed()
    {
        isPressed = false;
        yield return new WaitForSeconds(missileCooldown);
        missileButton.GetComponent<Button>().interactable = true;         
    }

    public void IsButtonPressed()
    {
        isPressed = true;
        missileButton.GetComponent<Button>().interactable = false;
    }

}
