using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    private Rigidbody bulletRigidbody;
    [SerializeField] private Transform vfxHitTarget;
    [SerializeField] private Transform vfxHitCollider;
    private AudioSource audioSource;

    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        float speed = 30f;
        bulletRigidbody.velocity = transform.forward * speed;
        audioSource.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Instantiate(vfxHitTarget, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Collide")
        {
            Instantiate(vfxHitCollider, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }

    /*private void OnCollisionEnter(Collision collision)
    {
        
    }*/
}
