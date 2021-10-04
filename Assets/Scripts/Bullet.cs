using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    
    private Rigidbody2D rigidbody;
    private PlayerController player;
    private ParticleSystem particles;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        rigidbody = GetComponent<Rigidbody2D>();
        particles = GetComponentInChildren<ParticleSystem>();
    }

    public void OnEnable()
    {
        rigidbody.AddForce(player.GetComponent<Rigidbody2D>().transform.up * bulletSpeed);
        StartCoroutine(BulletDeactivate());
    }

    IEnumerator BulletDeactivate()
    {
        yield return new WaitForSeconds(1.5f);
        GameObject bullet = player.bullets.FirstOrDefault();
        player.bullets.Remove(bullet);
        BulletObjectPool.Instance.ReturnBullet(bullet);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("PlayerArea") || collision.collider.gameObject.layer == LayerMask.NameToLayer("Asteroid"))
        {
            GameObject bullet = player.bullets.FirstOrDefault();
            player.bullets.Remove(bullet);
            BulletObjectPool.Instance.ReturnBullet(bullet);
        }
    }
}
