using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon", order = 1)]
public class WeaponData : ScriptableObject
{
    
    [Header("General")]
    public new string name;
    public int damage;
    public float useRate;

}
