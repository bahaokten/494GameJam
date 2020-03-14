using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed = 10;
    //private Rigidbody rigidbody;
    Vector3 bulletDir;


    void Start()
    {
        //rigidbody = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        this.transform.Translate(bulletDir * Time.deltaTime * speed);
    }

    public void SetBulletDirection(Vector3 dir)
    {
        bulletDir = dir;
    }

    public void SetSpeed(float s)
    {
        speed = s;
    }



    /*
    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + Vector3.forward * Time.fixedDeltaTime);
    }*/

}
