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
    [SerializeField] private float ufoCD;
    private float lastShoot;
    private float gameTime;
    private float ufoTime;
    private float shootCD;
    private List<float> shootCDRange;

    // Distance where the enemy group need to make a direction change
    private float leftMost;
    private float rightMost;
    private bool canAdvance;

    [SerializeField] private MyIntEvent gameWin;

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
                else if (row == 4 || row == 3)
                    enemyPrefab = enemyPrefabs[6];
                else
                    enemyPrefab = enemyPrefabs[0];
                GameObject enemy = Instantiate(enemyPrefab, new Vector2(-10.5f + column * 2.0f, LevelInfo.enemyHeight - row * 2.0f), Quaternion.identity);
                enemy.transform.parent = gameObject.transform;
                newColumn.Add(enemy);
            }
            enemyGroup.Add(newColumn);
        }

        moveCD = LevelInfo.moveCD;
        moveSpeed = 1.0f;
        gameTime = Time.time;

        leftMost = -5.0f;
        rightMost = 6.0f;
        canAdvance = true;

        shootCDRange = new List<float>(LevelInfo.shootCDRange);
        shootCD = Random.Range(shootCDRange[0], shootCDRange[1]);

        lastShoot = Time.time;

        ufoTime = Time.time;
        ufoCD = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        // Enemies are all elimitated by player
        if (eliminatedAll())
        {
            gameWin.Invoke(1);
        }

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

        if (!eliminatedAll())
            changeSide();

        // Randomly find a column, and the lowest enemy start to shoot
        if (Time.time - lastShoot >= shootCD)
        {
            int col = enemyGroup.Count;
            int c_index = Random.Range(0, col);
            int row = enemyGroup[c_index].Count;

            for (int i = row-1; i >= 0; i--)
            {
                if (enemyGroup[c_index][i] != null)
                {
                    enemyGroup[c_index][i].gameObject.GetComponent<EnemyBehavior>().shoot();
                    lastShoot = Time.time;
                    break;
                }
            }
            shootCD = Random.Range(shootCDRange[0], shootCDRange[1]);
        }

        // Instantiate UFO
        if (Time.time - ufoTime >= ufoCD)
        {
            GameObject ufoPrefab = enemyPrefabs[Random.Range(1, 5)];
            List<Vector2> spawnPos = new List<Vector2> {new Vector2(15f, 7.5f), new Vector2(-15f, 7.5f)};
            GameObject ufo = Instantiate(ufoPrefab, spawnPos[Random.Range(0, 2)], Quaternion.identity);
            ufoTime = Time.time;
        }
    }

    // Function used for enemy group changing direction and moving down
    private void enemyAdvance()
    {
        moveSpeed = -moveSpeed;
        transform.Translate(new Vector2(0, -0.5f));
        moveCD = moveCD - 0.05f;
        shootCDRange[0] = shootCDRange[0] - 0.2f;
        shootCDRange[1] = shootCDRange[1] - 0.2f;
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
            leftMost = leftMost - 2.0f;
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
            rightMost = rightMost + 2.0f;
        }
    }

    private bool eliminatedAll()
    {
        foreach (List<GameObject> column in enemyGroup)
        {
            foreach (GameObject enemy in column)
            {
                if (enemy != null)
                    return false;
            }
        }

        return true;
    }
}
