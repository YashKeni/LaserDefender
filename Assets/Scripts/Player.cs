using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Configure Parameters
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 200;

    [Header("Sound")]
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.5f;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.5f;

    Coroutine fireCorout;
    CameraShake cameraShake;
    [SerializeField] bool applyCamShake;
    GameObject shield;
    [SerializeField] int shieldHealth = 100;
    Vector3 touchPosition;
    Rigidbody2D rb;
    Vector3 direction;
    Projectile projectile;
    GameSession gameSession;


    float xMin;
    float xMax;
    float yMin;
    float yMax;

    void Awake() 
    {
        projectile = FindObjectOfType<Projectile>();
        cameraShake = Camera.main.GetComponent<CameraShake>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        shield = transform.Find("Shield").gameObject;
        DeactivateShield();
        SetBoundaries();
        gameSession = FindObjectOfType<GameSession>();
    }

    void Update()
    {
        // Move();
        TouchControl();
    }

    void TouchControl()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;

            float newXPos = Mathf.Clamp(transform.position.x, xMin, xMax);
            float newYPos = Mathf.Clamp(transform.position.y, yMin, yMax);

            transform.position = new Vector2(newXPos, newYPos);


            direction = (touchPosition - transform.position);
            rb.velocity = new Vector2(direction.x, direction.y) * moveSpeed;

            if(touch.phase == TouchPhase.Ended)
            {
                rb.velocity = Vector2.zero;
            }
            
        }
    }
    

    private void SetBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding - 3    ;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PowerUp powerUp = other.gameObject.GetComponent<PowerUp>();
        if(other.gameObject.tag == "PowerUp")
        {
            if(powerUp)
            {
                if(powerUp.activateShield)
                {
                    ActivateShield();
                    gameSession.AddToScore(100);
                    Destroy(other.gameObject);
                }

                if(powerUp.upgradeGun)
                {
                    FindObjectOfType<Projectile>().IncreaseRateOfFire();
                    gameSession.AddToScore(100);
                    Destroy(other.gameObject);
                }

                if(powerUp.healthUpgrade)
                {
                    if(health < 1900)
                    {
                        health = health + 200;
                    }
                    if(health == 1900)
                    {
                        health = health + 100;
                    }
                    gameSession.AddToScore(100);
                    Destroy(other.gameObject);
                }

            }
        }

        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);        

    }
 

    private void ProcessHit(DamageDealer damageDealer)
    {
        if(!HasShield())
        {
            health -= damageDealer.GetDamage();
            ShakeCamera();
        }
        damageDealer.Hit();
        if (HasShield())
        {
            ShieldDamage();
            // Debug.Log(shieldHealth);
        
            if(shieldHealth <= 0)
            {
                DeactivateShield();
            }
        }
        else if(health <= 0)
        {
            Die();
        }
    }

    

    private void Die()
    {
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
        //SceneManager.LoadScene("Game Over");
    }

    public int GetHealth()
    {
        return health;
    }

    // ------------------ SHIELD ------------------

    bool HasShield()
    {
        return shield.activeSelf;
    }

    public void ActivateShield()
    {
        if(shieldHealth < 100 && shield.activeSelf == true)
        {
            shieldHealth = shieldHealth + 10;
        }
        else
        {
            shieldHealth = 100;
            shield.SetActive(true);
        }
        
    }

    void DeactivateShield()
    {
        shield.SetActive(false);
    }

    void ShieldDamage()
    {
        shieldHealth = shieldHealth - 20;
    }
    // --------------------------------------------

    // --------------- CAMERA SHAKE ---------------
    void ShakeCamera()
    {
        if(cameraShake != null && applyCamShake)
        {
            cameraShake.Play();
        }
    }
    // --------------------------------------------
}
