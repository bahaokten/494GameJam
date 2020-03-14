using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public GameObject prefab;
    private GameObject enemy;
    private Vector3 center;
    private Color32 colorPlayer1;
    private Color32 colorPlayer2;
    private float randomNumber;
    private float timer = 0.0f;
    private float interval = 2;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
        center = new Vector3(0, 0, 0);
        colorPlayer1 = new Color32(64, 183, 209, 255);
        colorPlayer2 = new Color32(143, 37, 238, 255);
        randomNumber = Random.Range(1,3);
        if (randomNumber == 1)
        {
            enemy.GetComponent<Renderer>().material.SetColor("_Color", colorPlayer1);
        }
        else 
        {
            enemy.GetComponent<Renderer>().material.SetColor("_Color", colorPlayer2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
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

    IEnumerator SpawnEnemies()
    {
        while(true)
        {
            randomNumber = Random.Range(1,3);
            Vector3 pos = RandomCircle(center, 11.0f);
            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center-pos);
            enemy = Instantiate(prefab, pos, rot);
            if (randomNumber == 1)
            {
                enemy.GetComponent<Renderer>().material.SetColor("_Color", colorPlayer1);
            }
            else 
            {
                enemy.GetComponent<Renderer>().material.SetColor("_Color", colorPlayer2);
            }
            yield return new WaitForSeconds(interval);
        }
    }

    Vector3 RandomCircle (Vector3 center, float radius){
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = 0.65f;
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }
}
