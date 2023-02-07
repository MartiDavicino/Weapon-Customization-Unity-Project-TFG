using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponType
{
    NON_BULLPUP,
    BULLPUP
    
}
public enum WeaponPart
{
    STOCK,
    HANDGUARD,
    BARREL,
    MUZZLE,
    HANDGUARD_ATTACHMENT,
    BARREL_ATTACHMENT,
    SCOPE
}
public class Weapon : MonoBehaviour
{
    public WeaponType type;
    [HideInInspector]
    public GripType gripType;
    //[HideInInspector]
    public bool useStock, useHandguard, useBarrel,useMuzzle,useHandguardAttachment,useBarrelAttachment,useScope = false;

    //float stockProabability = 0.9f;
    //float handguardProbability=0.7f;
    //float barrelProbability = 0.6f;
    //float muzzleProbability = 0.4f;
    //float handguardAttachmentProbability = 0.15f;
    //float barrelAttachmnetProbability = 0.1f;
    //float scopeProbability = 0.75f;

    float stockProabability = .8f;
    float handguardProbability = 0.5f;
    float barrelProbability = 0.9f;
    float muzzleProbability = 0.5f;
    float handguardAttachmentProbability = 1f;
    float barrelAttachmnetProbability = 1f;
    float scopeProbability = 0.5f;


    public Transform handguardSocket;
    //We dont use barrel and muzzle socket because its attached to handguard
    public Transform gripSocket;
    public Transform stockSocket;
    public Transform magazineSocket;
    public Transform scopeSocket;

    [System.Obsolete]
    public bool UsePart(WeaponPart part)
    {
        bool ret = false;
        float ran = Random.RandomRange(0.0f, 1.0f);

        switch(part)
        {
            case WeaponPart.STOCK:
                if (ran <= stockProabability)
                {
                    useStock = true;
                    ret = true;
                }
                break;
           case WeaponPart.HANDGUARD:
                if (ran <= handguardProbability)
                {
                    useHandguard = true;
                    ret = true;
                }
                break;
            case WeaponPart.BARREL:
                if (ran <= barrelProbability)
                {
                    useBarrel = true;
                    ret = true;
                }
                break;
            case WeaponPart.MUZZLE:
                if (ran <= muzzleProbability)
                {
                    useMuzzle = true;
                    ret = true;
                }
                break;
            case WeaponPart.HANDGUARD_ATTACHMENT:
                if (ran <= handguardAttachmentProbability)
{
                    useHandguardAttachment = true;
                    ret = true;
                }                break;
            case WeaponPart.BARREL_ATTACHMENT:
                if (ran <= barrelAttachmnetProbability)
                {
                    useBarrelAttachment = true;
                    ret = true;
                }
                break;
            case WeaponPart.SCOPE:
                if (ran <= scopeProbability)
                {
                    useScope = true;
                    ret = true;
                }
                break;
        }
        
        return ret;
    }
}
