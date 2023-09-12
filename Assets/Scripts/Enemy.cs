using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] float health = 100;
    [SerializeField] int scoreValue = 150;

    [Header("Projectile")]
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 10f;
    float shotCounter;

    [Header("Particles")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;

    [Header("Sound")]
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0,1)] float deathSoundVolume = 0.5f;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.5f;

    [Header("PowerUp Drop")]
    int powerUpRandom;
    [SerializeField] GameObject shieldPowerUpPrefab;
    [SerializeField] GameObject gunPowerUpPrefab;
    [SerializeField] GameObject healthUpgradePrefab;
    [SerializeField] float powerUpDropSpeed = 5f;

    TimeBar timebar;

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        timebar = FindObjectOfType<TimeBar>();
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
        AfterLevel();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if(shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(
            projectile,
            transform.position,
            Quaternion.identity
            ) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
        powerUpRandom = Random.Range(1, 9);
        if(powerUpRandom == 1)
        {
            DropShieldPowerUp();
        }
        else if(powerUpRandom == 2)
        {
            DropGunPowerUp();
        }
        else if(powerUpRandom == 3 || powerUpRandom == 4)
        {
            DropHealthPowerUp();
        }
        
    }

    void DropShieldPowerUp()
    {
        GameObject powerUp = Instantiate(
            shieldPowerUpPrefab,
            transform.position,
            Quaternion.identity
            ) as GameObject;
        powerUp.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -powerUpDropSpeed);
    }

    void DropGunPowerUp()
    {
        GameObject powerUp = Instantiate(
            gunPowerUpPrefab,
            transform.position,
            Quaternion.identity
            ) as GameObject;
        powerUp.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -powerUpDropSpeed);
    }

    void DropHealthPowerUp()
    {
        GameObject powerUp = Instantiate(
            healthUpgradePrefab,
            transform.position,
            Quaternion.identity
            ) as GameObject;
        powerUp.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -powerUpDropSpeed);
    }


    public void DestroyAfterLevel()
    {
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
    }

    void AfterLevel()
    {
        if(timebar.isPassed == true)
        {
            DestroyAfterLevel();
        }
    }

}
