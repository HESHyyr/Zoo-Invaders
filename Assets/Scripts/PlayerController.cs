using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class MyIntEvent : UnityEvent<int>
{
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] private MyIntEvent playerKilled;
    [SerializeField] private GameObject ammoPrefab;
    [SerializeField] private float speed;
    private Rigidbody2D rb;

    // Boolean to keep tracking whether the player has shooted and the taco ammo is not destroyed 
    private bool ammoAlive;

    // Player life
    private int life;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ammoAlive = false;
        life = 3;
    }

    // Update is called once per frame
    void Update()
    {

        // User input for moving and shooting
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, 0);
        if (Input.GetKeyDown("space") && !ammoAlive)
            shoot();

    }

    private void shoot() {

        Instantiate(ammoPrefab, gameObject.transform.GetChild(0).transform.position, Quaternion.identity);
        ammoAlive = true;
    }

    // Function to flip the value of ammoAlive
    public void flipAmmoStatus()
    {
        ammoAlive = !ammoAlive;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ammo"))
        {
            life--;
            playerKilled.Invoke(life);
        }
    }
}
