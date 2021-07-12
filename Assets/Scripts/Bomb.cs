using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionPrefab;
    public float radius = 7f;
    public float explosionForce = 50f;
    public AudioSource audioSource{ get{ return GetComponent<AudioSource>(); } }
    
    void Start()
    {
        Destroy(gameObject, 2f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(audioSource.clip);
        }
        Health health = collision.gameObject.GetComponent<Health>();
        if (health)
        {
            health.TakeDemage(10);
        }

        CreateExplosionEffect();
    }

    private void CreateExplosionEffect()
    {   
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, radius);

        foreach (var nearby in nearbyObjects)
        {
            Rigidbody rb = nearby.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.AddExplosionForce(explosionForce, transform.position, radius);
            }
        }
    }
}

