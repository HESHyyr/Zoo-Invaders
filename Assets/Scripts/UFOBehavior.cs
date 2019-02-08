using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOBehavior : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    public int score;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 1.0f;

        if (transform.position.x > 0)
            moveSpeed *= -1;
    }

    // Update is called once per frame
    void Update()
    {
        // UFO moving
        Vector2 target = new Vector2(transform.position.x + moveSpeed, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, target, 5f * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }
}
