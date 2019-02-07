using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Text life;

    // Start is called before the first frame update
    void Start()
    {
        life = gameObject.transform.Find("Life").GetComponent<Text>();
        changeLife(3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Change life number displayed to be the new number of life the player has
    public void changeLife(int newLife)
    {
        life.text = string.Format("Life: {0}", newLife);
    }
}
