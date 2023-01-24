using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalPipe : Weapon
{

    Collider hitbox;
    protected override bool CanUse()
    {
        Debug.Log("CanUse: " + (timeSinceLastUse >= weaponData.useRate));
        return timeSinceLastUse >= weaponData.useRate;
    }

    new void Start()
    {
        base.Start();
        hitbox = GetComponent<Collider>();
        hitbox.enabled = false;
    }

    protected override void Use()
    {
        animator.SetTrigger("Use");
        audioSource.Play();

        hitbox.enabled = true;
        Invoke("DisableHitbox", 0.5f);
    
        timeSinceLastUse = 0f;
    }

    void DisableHitbox()
    {
        hitbox.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<IDamageable>().TakeDamage(weaponData.damage);
        }
    }
}
