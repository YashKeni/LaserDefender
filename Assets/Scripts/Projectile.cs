using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] public float rateOfFire = 0.3f;
    [SerializeField] float highRateOfFireCooldown = 2;
    
    Coroutine fireCoroutine;

    private void Update() 
    {
        if(rateOfFire <= 0.2f)
        {
            Invoke("DecreaseROF", highRateOfFireCooldown);   
        }
    }

    public void DecreaseROF()
    {
        rateOfFire = 0.3f;
    }

    public void StartFire()
    {
        fireCoroutine = StartCoroutine(AutoFire());
    }

    public void StopFire()
    {
        StopCoroutine(fireCoroutine);
    }

    IEnumerator AutoFire()
    {
        
        while(true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;

            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

            yield return new WaitForSeconds(rateOfFire);
        } 
    }

    public void IncreaseRateOfFire()
    {
        rateOfFire = rateOfFire - 0.1f;
        if(rateOfFire <= 0.1f)
        {
            rateOfFire = 0.1f;
        }


    }
}
