using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponType
{
    NON_BULLPUP,
    BULLPUP
    
}
public class Weapon : MonoBehaviour
{
    public WeaponType type;

    public GripType gripType;
    [HideInInspector]
    public bool useStock = true;
    [HideInInspector]
    public bool useHandguard = true;
    [HideInInspector]
    public bool useBarrel = true;

    public Transform handguardSocket;
    //We dont use barrel and muzzle socket because its attached to handguard
    public Transform gripSocket;
    public Transform stockSocket;
    public Transform magazineSocket;
    public Transform scopeSocket;

}
