using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    // SINGLETON PATTERN
    public static GameControl           Instance;

    [Header("Prefab References")]
    public GameObject                   enemyPrefab;
    public GameObject                   healthPrefab;
    [Header("Settings")]
    public float                        initialHealth = 3f;
    public float                        enemySpawnDist = 11f;
    public Color                        colorPlayer1;
    public Color                        colorPlayer2;
    public int                          healthSpawnRatio = 3;
    public float                        playerRad = 3f;
    [Header("Data")]
    public float                        health;

    private Vector3                     center;
    private float                       randomNumber;
    private float                       timer = 0f;
    private float                       timeSinceLastSpawn = 0f;
    private float                       interval = 2f;
    private int                         lastHealthSpawn;
    private static eGameState _GAME_STATE = eGameState.mainMenu;

    public delegate void CallbackDelegate(); // Set up a generic delegate type.
    static public event CallbackDelegate GAME_STATE_CHANGE_DELEGATE;
    static public event CallbackDelegate HEALTH_CHANGE_DELEGATE;

    [System.Flags]
    public enum eGameState
    {
        none = 0,
        mainMenu = 1,
        preLevel = 2,
        level = 4,
        levelCompleted = 8,
        gameOver = 16,
        levelCompleting = 32,
        all = 0xFFFFFFF
    }

    // THE CURRENT STATE OF THE GAME
    static public eGameState GAME_STATE
    {
        get
        {
            return _GAME_STATE;
        }
        set
        {
            if (value != _GAME_STATE)
            {
                _GAME_STATE = value;

                // Check that the delegate is not null
                if (GAME_STATE_CHANGE_DELEGATE != null)
                {
                    GAME_STATE_CHANGE_DELEGATE();
                }
            }
        }
    }


    private void Awake()
    {
        // SINGLETON PATTERN
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        GAME_STATE = eGameState.mainMenu;
    }

    // Start is called before the first frame update
    void Start()
    {
        health = initialHealth;

        center = new Vector3(0, 0, 0);
        colorPlayer1 = new Color32(64, 183, 209, 255);
        colorPlayer2 = new Color32(143, 37, 238, 255);
    }

    // Update is called once per frame
    void Update()
    {
        if (GAME_STATE != eGameState.level) return;

        // Increment timers
        timer += Time.deltaTime;
        timeSinceLastSpawn += Time.deltaTime;

        // Adjust interval
        AdjustSpawnInterval();

        // Spawn enemy each interval
        if (timeSinceLastSpawn > interval)
        {
            timeSinceLastSpawn = 0f;
            if(lastHealthSpawn == healthSpawnRatio){
                lastHealthSpawn = 0;
                SpawnHealth();
            }
            lastHealthSpawn++;
            SpawnEnemy();
        }
    }

    void SpawnHealth()
    {
        // Set pos and rot of new enemy
        float randomDist = Random.Range(playerRad, enemySpawnDist-4f);

        Vector3 pos = RandomCircle(center, randomDist);

        // Spawn new enemy
        GameObject newHealthObject = Instantiate(healthPrefab, pos, Random.rotation);


    }

    void SpawnEnemy()
    {
        // Set pos and rot of new enemy
        Vector3 pos = RandomCircle(center, enemySpawnDist);
        Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);

        // Spawn new enemy
        GameObject newEnemy = Instantiate(enemyPrefab, pos, rot);

        // Randomly which type of enemy to make the new enemy
        randomNumber = Random.Range(0f, 1f);

        if (randomNumber < 0.5f)
        {
            newEnemy.GetComponent<Renderer>().material.SetColor("_Color", colorPlayer1);
            newEnemy.GetComponent<EnemyBehavior>().enemyID = 1;
        }
        else
        {
            newEnemy.GetComponent<Renderer>().material.SetColor("_Color", colorPlayer2);
            newEnemy.GetComponent<EnemyBehavior>().enemyID = 2;
        }
    }

    void AdjustSpawnInterval()
    {
        if (timer > 20)
        {
            interval = 1f;
        }
        else if (timer > 40)
        {
            interval = 0.75f;
        }
        else if (timer > 60)
        {
            interval = 0.5f;
        }
    }

    Vector3 RandomCircle (Vector3 center, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = 0.65f;
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }

    public void DeductHealth(float healthDeducted)
    {
        health -= healthDeducted;

        if (HEALTH_CHANGE_DELEGATE != null) HEALTH_CHANGE_DELEGATE();

        if (health <= 0f)
        {
            GAME_STATE = eGameState.gameOver;
        }
    }
    public void AddHealth(float healthDeducted)
    {
        if(health < initialHealth){
            health += healthDeducted;
        }

        if (HEALTH_CHANGE_DELEGATE != null) HEALTH_CHANGE_DELEGATE();

    }
}
