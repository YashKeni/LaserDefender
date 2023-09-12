using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [Header("Missile")]
    [SerializeField] GameObject missilePrefab;
    [SerializeField] float missileSpeed = 5f;

    public void LaunchMissile()
    {
        GameObject missile = Instantiate(missilePrefab, transform.position, Quaternion.identity) as GameObject;

        missile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, missileSpeed);
    }

}
