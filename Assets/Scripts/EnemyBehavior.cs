using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [Header("Prefab References")]
    public GameObject                       enemyDeathEffect;

    [Header("Settings")]
    public float                            speed = 1;

    [Header("Data")]
    public int                              enemyID = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = new Vector3(0,transform.position.y,0);
        float step =  speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, target, step);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Bullet" && enemyID == other.transform.GetComponent<Bullet>().bulletID)
        {
            SpawnDeathEffect();
            Destroy(gameObject);
        }

        if (other.transform.tag == "Player")
        {
            SpawnDeathEffect();
            GameControl.Instance.DeductHealth(1f);

            if (null != other.GetComponent<PlayerBehavior>())
            {
                other.GetComponent<PlayerBehavior>().IndicateDamageTaken();
            }

            Destroy(gameObject);
        }
    }

    void SpawnDeathEffect()
    {
        GameObject deathEffect = Instantiate(enemyDeathEffect);
        deathEffect.transform.position = transform.position;
    }
}
