using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private Transform BulletInstantiate;
    [SerializeField] private GameObject GameOver;

    private Rigidbody2D rigidbody;
    private EdgeCollider2D collider;
    private SpriteRenderer sprite;

    public List<GameObject> bullets = new List<GameObject>();
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<EdgeCollider2D>();
        sprite = GetComponent<SpriteRenderer>();

        StartCoroutine(Shooting());

        transform.position = new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 4f), 0f);
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rigidbody.AddForce(transform.up * movementSpeed);
        }

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            rigidbody.AddTorque(Input.GetAxisRaw("Horizontal") * turnSpeed);
        }
        else
        {
            rigidbody.AddTorque(0);
        }
    }

    IEnumerator Shooting()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                GameObject bullet = BulletObjectPool.Instance.GetBullet();
                bullet.transform.SetParent(null);
                bullets.Add(bullet);
                bullet.transform.position = BulletInstantiate.position;
                yield return new WaitForSeconds(0.15f);
            }
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Asteroid"))
        {
            GameManager.Instance.Lifes -= 1;

            if (GameManager.Instance.Lifes == 0)
            {
                Destroy(gameObject);
                GameOver.SetActive(true);
            }
            StartCoroutine(BlinkingAfterHit());
        }
    }

    IEnumerator BlinkingAfterHit()
    {
        for (int i = 0; i < 3; i++)
        {
            sprite.enabled = false;
            yield return new WaitForSeconds(0.5f);
            sprite.enabled = true;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
