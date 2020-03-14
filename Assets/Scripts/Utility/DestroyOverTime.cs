using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    public float time;

    void Start()
    {
        Invoke("DestroySelf", time);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
