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
    [SerializeField] private MyIntEvent scoreChanged;
    [SerializeField] private GameObject ammoPrefab;
    [SerializeField] private float speed;
    [SerializeField] private GameObject shieldPrefab;
    private Rigidbody2D rb;

    // Boolean to keep tracking whether the player has shooted and the taco ammo is not destroyed 
    private bool ammoAlive;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ammoAlive = false;

        // Generate shields
        Instantiate(shieldPrefab, new Vector2(-10.65f,-5.8f), Quaternion.identity);
        Instantiate(shieldPrefab, new Vector2(-3.65f, -5.8f), Quaternion.identity);
        Instantiate(shieldPrefab, new Vector2(3.65f, -5.8f), Quaternion.identity);
        Instantiate(shieldPrefab, new Vector2(10.65f, -5.8f), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

        // User input for moving and shooting
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, 0);
        if (Input.GetKeyDown("space") && !ammoAlive)
            shoot();
        //Debug.Log(ammoAlive);

    }

    private void shoot() {

        Instantiate(ammoPrefab, gameObject.transform.GetChild(0).transform.position, Quaternion.identity);
        ammoAlive = true;
    }

    // Function to flip the value of ammoAlive
    public void ammoReady()
    {
        ammoAlive = false;
    }

    // Function to add score
    public void addScore(int amount)
    {
        LevelInfo.score += amount;
        scoreChanged.Invoke(LevelInfo.score);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ammo"))
        {
            LevelInfo.life--;
            Destroy(collision.gameObject);
            StartCoroutine(fadeInAndOut(0.1f));
            playerKilled.Invoke(LevelInfo.life);
        }
    }

    IEnumerator fadeInAndOut(float time)
    {
        Time.timeScale = 0.3f;
        gameObject.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .3f);
        yield return new WaitForSeconds(time);
        gameObject.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
        Time.timeScale = 1;
    }
}
