using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : Weapon
{
    Transform firePoint;
    protected override bool CanUse()
    {
        return timeSinceLastUse >= weaponData.useRate;
    }

    new void Start()
    {
        base.Start();
        firePoint = transform.Find("FirePoint");
        audioSource = GetComponent<AudioSource>();
    }

    protected override void Use()
    {
        Camera mainCamera = Camera.main;
        Vector3 targetPoint = mainCamera.ScreenToWorldPoint(new Vector3(0.5f, 0.5f, mainCamera.transform.position.y - transform.position.y));
        animator.SetTrigger("Shoot");

        Ray ray = new Ray(firePoint.position, targetPoint - firePoint.position);
        RaycastHit hit;
        Vector3 endPoint = new Vector3(0, 0, 0);
        if (Physics.Raycast(ray, out hit, 100f))
        {
            IShootable shootable = hit.collider.GetComponent<IShootable>();
            if (shootable != null)
            {
                shootable.GetShot(hit.point, hit.collider, weaponData.damage);
            }
            endPoint = hit.point;
        } else
        {
            endPoint = ray.GetPoint(100f);
        }
        DrawGunTrail(endPoint);
    }

    private void DrawGunTrail(Vector3 hitPoint)
    {
        GameObject trail = Instantiate(new GameObject(), firePoint.position, Quaternion.identity);
        LineRenderer lineRenderer = trail.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, hitPoint);
        Destroy(trail, 0.1f);
    }

}
