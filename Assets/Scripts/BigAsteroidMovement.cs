using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigAsteroidMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private Transform startposition;
    [SerializeField] private Vector2 force;

    private Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        transform.position = startposition.position;
    }

    private void Start()
    {
        rigidbody.AddForce(force * movementSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            for (int i = 0; i < 2; i++)
            {
                GameObject Asteroid = AsteroidsObjectPool.Instance.GetAsteroid();
                Asteroid.transform.position = transform.position;
            }
            transform.position = startposition.position;
            GameManager.Instance.Points += 10;
        }
    }
}
