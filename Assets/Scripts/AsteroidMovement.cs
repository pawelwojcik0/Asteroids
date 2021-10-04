using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private Vector3 mediumAsteroid;
    [SerializeField] private Vector3 smallAsteroid;
    [SerializeField] private float collisionTime;

    private Rigidbody2D rigidbody;
    private float timer;
    private bool isColliding; 

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = (Random.insideUnitCircle.normalized * movementSpeed);
    }

    private void Update()
    {
        if(isColliding == true)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = collisionTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            if (transform.localScale == mediumAsteroid)
            {
                for (int i = 0; i < 2; i++)
                {
                    GameObject Asteroid = AsteroidsObjectPool.Instance.GetAsteroid();
                    Asteroid.transform.position = transform.position * Random.Range(0.85f, 1.15f);
                    Asteroid.transform.localScale = smallAsteroid;
                }
                AsteroidsObjectPool.Instance.ReturnAsteroid(gameObject);
                GameManager.Instance.Points += 20;
            }
            else
            {
                transform.localScale = smallAsteroid;
                AsteroidsObjectPool.Instance.ReturnAsteroid(gameObject);
                GameManager.Instance.Points += 30;
            }
        }

        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Area"))
        {
            isColliding = true;
        }
        else
        {
            isColliding = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Area") && timer < 0)
        {
            AsteroidsObjectPool.Instance.ReturnAsteroid(gameObject);
        }
    }
}
