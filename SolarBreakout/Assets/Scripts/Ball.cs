
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

public class Ball : MonoBehaviour
{
    private float speed = 20f;
    Rigidbody rigidBody;
    Vector3 velocity;
    Renderer Renderer;

    private void Start()
    {
        this.rigidBody = GetComponent<Rigidbody>();
        this.Renderer = GetComponent<Renderer>();
        Invoke("Launch", 0.5f);
    }

    private void Launch()
    {
        this.rigidBody.velocity = Vector3.up * this.speed;
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        this.rigidBody.velocity = this.rigidBody.velocity.normalized * this.speed;
        this.velocity = this.rigidBody.velocity;
        if (!Renderer.isVisible)
        {
            GameManager.Instance.Balls--;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        this.rigidBody.velocity = Vector3.Reflect(this.velocity, collision.contacts[0].normal);
    }
}
