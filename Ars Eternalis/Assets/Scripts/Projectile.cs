using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    new Collider collider;
    new Rigidbody rigidbody;

    [SerializeField] float speed;
    [SerializeField] float damage;
    [SerializeField] float lifeTime;

    private Transform target;

    void Start()
    {
        collider = GetComponent<Collider>();
        rigidbody = GetComponent<Rigidbody>();
        StartCoroutine(DestroyAfterTime());

        //set the velocity of the projectile to the direction of the target
        transform.LookAt(target);
        rigidbody.velocity = transform.forward * speed;
    }

    private IEnumerator DestroyAfterTime() {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    public void SetTarget(Transform newTarget) {
        target = newTarget;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            other.GetComponent<IDamageable>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }


}
