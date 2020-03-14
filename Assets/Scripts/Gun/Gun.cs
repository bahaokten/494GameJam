using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public bool GunID;
    public Bullet bullet;
    public float timeBetweenShoot = 100;
    public float bulletSpeed = 35;

    float nextTimeToShoot;

    public void Shoot()
    {
        if(Time.time > nextTimeToShoot)
        {
            nextTimeToShoot = Time.time + timeBetweenShoot / 1000;
            Bullet newBullet = Instantiate(bullet, this.transform.position, this.transform.rotation) as Bullet;
            if (GunID)
            {
                newBullet.SetBulletDirection(Vector3.up);
            }
            else
            {
                newBullet.SetBulletDirection(Vector3.down);
            }
            newBullet.SetSpeed(bulletSpeed);
        }
        
    }
}
