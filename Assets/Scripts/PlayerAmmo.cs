using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAmmo : MonoBehaviour
{
    private PlayerController player; 


    [SerializeField] private float speed;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            int score = collision.gameObject.GetComponent<EnemyBehavior>().score;
            Destroy(collision.gameObject);
            Destroy(gameObject);
            player.addScore(score);
            player.flipAmmoStatus();
        }
        else if (collision.gameObject.CompareTag("UFO"))
        {
            int score = collision.gameObject.GetComponent<UFOBehavior>().score;
            Destroy(collision.gameObject);
            Destroy(gameObject);
            player.addScore(score);
            player.flipAmmoStatus();
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
            player.flipAmmoStatus();
        }
    }
}
