using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnMovement : MonoBehaviour
{
    public float jumpForce;
    public float speed;
    public GameObject bulletPrefab;

    private Rigidbody2D rigidbody2D;
    private float horizontal;
    private bool grounded;
    private Animator animator;
    private float lastShoot;
    private int health = 5;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal") * speed; // -1, 0, 1

        if(horizontal < 0) transform.localScale = new Vector3(-1, 1);
        else if(horizontal >0)transform.localScale = new Vector3(1, 1);

        if(horizontal != 0) animator.SetBool("running", true);
        else animator.SetBool("running", false);

        if(Physics2D.Raycast(transform.position, Vector3.down, 0.1f)) grounded = true;
        else grounded = false;

        if (Input.GetKeyDown(KeyCode.UpArrow) && grounded) Jump();

        if (Input.GetKey(KeyCode.Space) && Time.time > lastShoot + 0.25)
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

    private void Jump()
    {
        rigidbody2D.AddForce(Vector3.up * jumpForce);
    }

    private void FixedUpdate()
    {
        rigidbody2D.velocity = new Vector3(horizontal, rigidbody2D.velocity.y);
    }

    public void Hit()
    {
        health--;
        if (health == 0) Destroy(gameObject);
    }
}
