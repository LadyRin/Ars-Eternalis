using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected AudioSource audioSource;
    protected Animator animator;

    [SerializeField] protected WeaponData weaponData;

    protected float timeSinceLastUse = 0f;

    protected void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    public void TryUse()
    {
        if (CanUse())
        {
            Use();
        }
    }

    protected abstract void Use();
    protected abstract bool CanUse();

    // Update is called once per frame
    void Update()
    {
        timeSinceLastUse += Time.deltaTime;
    }
}
