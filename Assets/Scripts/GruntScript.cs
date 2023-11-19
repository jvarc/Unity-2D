using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntScript : MonoBehaviour
{
    public GameObject John;
    public GameObject bulletPrefab;

    private float lastShoot;
    private int health = 3;

    private void Update()
    {
        Vector3 direction = John.transform.position - transform.position;

        if(direction.x >= 0)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);

        float distance = Mathf.Abs(John.transform.position.x - transform.position.x);

        if (distance < 1 && Time.time > lastShoot + 0.50)
        {
            Shoot();
            lastShoot = Time.time;
        }

    }

    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1) direction = Vector3.right;
        else direction = Vector3.left;

        GameObject bullet = Instantiate(bulletPrefab, transform.position + (direction * 0.1f), Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    public void Hit()
    {
        health--;
        if (health == 0) Destroy(gameObject);
    }
}
