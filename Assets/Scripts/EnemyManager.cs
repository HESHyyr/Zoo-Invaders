using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // List that holds enemy prefabs. Order is important cause generating the enemy group assumes that the order is fixed
    [SerializeField] private List<GameObject> enemyPrefabs;

    // Enemy is a column based array, which means the first index is the column number while the second indes is the row number
    private List<List<GameObject>> enemyGroup;

    [SerializeField] private float moveCD;
    [SerializeField] private float moveSpeed;
    private float gameTime;

    // Distance where the enemy group need to make a direction change
    private float leftMost;
    private float rightMost;
    private bool canAdvance;

    // Start is called before the first frame update
    void Start()
    {
        enemyGroup = new List<List<GameObject>>();

        // Generating enemy groups
        for (int column = 0; column < 11; column++)
        {
            List<GameObject> newColumn = new List<GameObject>();
            for(int row = 0; row < 5; row++)
            {
                GameObject enemyPrefab;
                if (row == 0)
                    enemyPrefab = enemyPrefabs[5];
                else if (row == 4)
                    enemyPrefab = enemyPrefabs[6];
                else
                    enemyPrefab = enemyPrefabs[Random.Range(0, 5)];
                GameObject enemy = Instantiate(enemyPrefab, new Vector2(-10.5f + column * 2, 8.5f - row * 2), Quaternion.identity);
                enemy.transform.parent = gameObject.transform;
                newColumn.Add(enemy);
            }
            enemyGroup.Add(newColumn);
        }

        moveCD = 1.0f;
        moveSpeed = 1.0f;
        gameTime = Time.time;

        leftMost = -5.0f;
        rightMost = 6.0f;
        canAdvance = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        // Enemy group moving
        if (Time.time - gameTime >= moveCD)
        {
            transform.Translate(new Vector2(moveSpeed, 0));
            gameTime = Time.time;
        }

        // Enemy group advancing when hitting screen side
        if (transform.position.x <= leftMost || transform.position.x >= rightMost)
        {
            if (Time.time - gameTime >= moveCD / 2 && canAdvance)
            {
                canAdvance = false;
                enemyAdvance();
            }
        }

        // When enemy group pass the center again, the group can advance again
        if (transform.position.x == 0)
            canAdvance = true;

        changeSide();
    }

    // Function used for enemy group changing direction and moving down
    private void enemyAdvance()
    {
        moveSpeed = -moveSpeed;
        transform.Translate(new Vector2(0, -0.5f));
    }

    // Function used for deleting side columns that are empty and change the side edge
    private void changeSide()
    {
        bool leftEmpty = true;
        foreach(GameObject enemy in enemyGroup[0])
        {
            if(enemy != null)
            {
                leftEmpty = false;
                break;
            }
        }

        if (leftEmpty)
        {
            enemyGroup.RemoveAt(0);
            leftMost = leftMost - 2;
        }

        bool rightEmpty = true;
        foreach (GameObject enemy in enemyGroup[enemyGroup.Count - 1])
        {
            if (enemy != null)
            {
                rightEmpty = false;
                break;
            }
        }

        if (rightEmpty)
        {
            enemyGroup.RemoveAt(enemyGroup.Count - 1);
            rightMost = rightMost + 2;
        }
    }
}
