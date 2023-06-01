using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponType
{
    NON_BULLPUP,
    BULLPUP,
    LMG
    
}
public enum WeaponPart
{
    GRIP,
    STOCK,
    MAGAZINE,
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

    //float stockProabability = 0.9f;
    //float handguardProbability=0.7f;
    //float barrelProbability = 0.6f;
    //float muzzleProbability = 0.4f;
    //float handguardAttachmentProbability = 0.15f;
    //float barrelAttachmnetProbability = 0.1f;
    //float scopeProbability = 0.75f;

    //float stockProabability = .8f;
    //float handguardProbability = 0.5f;
    //float barrelProbability = .7f;
    //float muzzleProbability = .5f;
    //float handguardAttachmentProbability = 1f;
    //float barrelAttachmnetProbability = 1f;
    //float scopeProbability = 0.65f;

    float stockProabability = 1f;
    float handguardProbability = 1f;
    float barrelProbability = 1f;
    float muzzleProbability = 1f;
    float handguardAttachmentProbability = 1f;
    float barrelAttachmnetProbability = 1f;
    float scopeProbability = 1f;
    float complexGripProbabilkity = .5f;


    public Transform handguardSocket;
    //We dont use barrel and muzzle socket because its attached to handguard
    public Transform gripSocket;
    public Transform stockSocket;
    public Transform magazineSocket;
    public Transform scopeSocket;

}
