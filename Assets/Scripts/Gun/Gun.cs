using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("References")]
    public Bullet                   bullet;

    [Header("Settings")]
    public int                      gunID;

    public float                    timeBetweenShoot = 100;
    public float                    bulletSpeed = 35;

    float nextTimeToShoot;

    public void Shoot()
    {
        if(Time.time > nextTimeToShoot)
        {
            nextTimeToShoot = Time.time + timeBetweenShoot / 1000;
            Bullet newBullet = Instantiate(bullet, this.transform.position, this.transform.rotation) as Bullet;
            if (gunID == 1)
            {
                newBullet.SetBulletDirection(Vector3.down);
                newBullet.bulletID = gunID;

            }
            else
            {
                newBullet.SetBulletDirection(Vector3.up);
                newBullet.bulletID = gunID;
            }

            newBullet.SetSpeed(bulletSpeed);
            StartCoroutine(AnimateShoot());
        }
        
    }

    IEnumerator AnimateShoot()
    {
        for(int i = 0; i < 20; i++)
        {
            transform.localScale *= 1.04f;
            yield return null;
        }
        for (int i = 0; i < 20; i++)
        {
            transform.localScale /= 1.04f;
            yield return null;
        }
    }
}
