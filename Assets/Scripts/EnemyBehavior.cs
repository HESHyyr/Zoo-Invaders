using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// This class contains behavior for each individual enemy
public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private MyIntEvent collideBottom;

    [SerializeField] private GameObject ammoPrefab;
    [SerializeField] private float ammoSpeed;
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        var canvas = GameObject.Find("Canvas").GetComponent<UIManager>();
        collideBottom.AddListener(canvas.changeLife);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void shoot()
    {
        GameObject ammo = Instantiate(ammoPrefab, gameObject.transform.position, Quaternion.identity);
        ammo.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -ammoSpeed);
        Destroy(ammo, 5);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "BottomWall")
        {
            collideBottom.Invoke(-1);
        }
    }
}
