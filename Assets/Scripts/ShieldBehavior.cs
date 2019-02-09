using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehavior : MonoBehaviour
{
    private PlayerController player;
    private List<Color> colorList = new List<Color>();
    private int colorIndex;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        colorIndex = 0;
        colorList.Add(Color.green);
        colorList.Add(Color.yellow);
        colorList.Add(Color.red);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ammo")) {
            if (collision.name.Contains("PlayerAmmo")) {
                
                player.ammoReady();
            }
                
            if (colorIndex <= 2)
            {
                GetComponent<SpriteRenderer>().color = colorList[colorIndex];
                colorIndex++;
            }
            else
                Destroy(gameObject);
            Destroy(collision.gameObject);
        }

    }
}
