using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed;
    public AudioClip sound;

    private Rigidbody2D rigidbody2D;
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        Camera.main.GetComponent<AudioSource>().PlayOneShot(sound);
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody2D.velocity = direction * speed;
    }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GruntScript grunt = collision.GetComponent<GruntScript>();
        if (grunt != null) grunt.Hit();

        JohnMovement john = collision.GetComponent<JohnMovement>();
        if (john != null) john.Hit();

        DestroyBullet();
    }
}
