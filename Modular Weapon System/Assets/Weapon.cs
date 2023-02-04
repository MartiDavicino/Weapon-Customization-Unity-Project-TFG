using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponType
{
    BULLPUP,
    NON_BULLPUP
}
public class Weapon : MonoBehaviour
{
    public WeaponType type;
    public Transform handguardSocket;
    //We dont use barrel and muzzle socket because its attached to handguard
    public Transform gripSocket;
    public Transform stockSocket;
    public Transform magazineSocket;
    public Transform scopeSocket;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
