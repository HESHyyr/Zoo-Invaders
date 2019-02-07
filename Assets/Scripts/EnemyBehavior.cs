using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class contains behavior for each individual enemy
public class EnemyBehavior : MonoBehaviour
{

    [SerializeField] private GameObject ammoPrefab;
    [SerializeField] private float ammoSpeed;
    [SerializeField] private float shootCD;
    private float lastShoot;

    // Start is called before the first frame update
    void Start()
    {
        lastShoot = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastShoot >= shootCD)
        {
            shoot();
            lastShoot = Time.time;
        }
    }

    private void shoot()
    {
        //GameObject ammo = Instantiate(ammoPrefab, gameObject.transform.position, Quaternion.identity);
        //ammo.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -ammoSpeed);
    }
}
