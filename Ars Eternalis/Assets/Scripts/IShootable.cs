using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootable
{
    void GetShot(Vector3 hitPoint, Collider hitCollider, float damage);
}
