using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Data")]
    public int                          bulletID;

    [Header("Settings")]
    public float                        speed = 10;

    private Vector3                     bulletDir;


    void Start()
    {

    }

    private void Update()
    {
        transform.Translate(bulletDir * Time.deltaTime * speed);
    }

    public void SetBulletDirection(Vector3 dir)
    {
        bulletDir = dir;
    }


    public void SetSpeed(float s)
    {
        speed = s;
    }


}
