using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGenerator : MonoBehaviour
{
    Weapon currentWeapon = null;

    GameObject previousWeapon = null;
    private Handguard currentHandguard = null;
    private Barrel currentBarrel = null;

    public List<GameObject> bodyParts;

    public List<GameObject> stockParts;
    public List<GameObject> magazineParts;
    public List<GameObject> gripParts;
    public List<GameObject> foregripParts;
    public List<GameObject> barrelParts;
    public List<GameObject> handguardParts;
    public List<GameObject> muzzleParts;
    public List<GameObject> scopeParts;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            DestroyCurrentWeapon();
            GenerateNewWeapon();
        }
    }

    private void GenerateNewWeapon()
    {
        
        //Body
        GameObject randomBody = GetRandomPart(bodyParts);
        GameObject instantiatedBody=Instantiate(randomBody);
        currentWeapon=instantiatedBody.GetComponent<Weapon>();

        SpawnPart(magazineParts, currentWeapon.magazineSocket);
        SpawnPart(scopeParts, currentWeapon.scopeSocket);

        SpawnGrip(gripParts, currentWeapon.gripSocket,currentWeapon.type);
        if(currentWeapon.useStock)SpawnPart(stockParts, currentWeapon.stockSocket);

        SpawnHandguard(handguardParts, currentWeapon.handguardSocket);

        SpawnBarrel(barrelParts);
        SpawnMuzzle(muzzleParts);


        previousWeapon = instantiatedBody;
    }

    private void DestroyCurrentWeapon()
    {
        if(previousWeapon != null)
        {
            Destroy(previousWeapon);
        }
    }

    void SpawnPart(List<GameObject> parts,Transform socket)
    {
        GameObject randomPart=GetRandomPart(parts);
        GameObject instantiatedPart=Instantiate(randomPart,socket.position,socket.rotation);
        instantiatedPart.transform.parent = socket;
    }

    void SpawnGrip(List<GameObject> parts, Transform socket, WeaponType type)
    {
        GameObject randomPart = GetRandomPart(parts);

        if (type == WeaponType.BULLPUP)
        {
            Debug.Log("Should use simple grip");
            //only simple grips

            while (randomPart.GetComponent<Grip>().type != GripType.SIMPLE)
            {
                Debug.Log("Searching for simple grip");

                randomPart = GetRandomPart(parts);
            }

        }


        GameObject instantiatedPart = Instantiate(randomPart, socket.position, socket.rotation);
        instantiatedPart.transform.parent = socket;

        if (randomPart.GetComponent<Grip>().type == GripType.STOCK || randomPart.GetComponent<Grip>().type == GripType.INTEGRED)
            currentWeapon.useStock = false;

    }

    void SpawnHandguard(List<GameObject> parts, Transform socket)
    {
        GameObject randomPart = GetRandomPart(parts);

        GameObject instantiatedPart = Instantiate(randomPart, socket.position, socket.rotation);
        instantiatedPart.transform.parent = socket;

        currentHandguard = instantiatedPart.GetComponent<Handguard>();

    }

    void SpawnBarrel(List<GameObject> parts)
    {
        GameObject randomPart = GetRandomPart(parts);
        Transform socket = currentHandguard.barrelSocket;

        GameObject instantiatedPart = Instantiate(randomPart, socket.position, socket.rotation);
        instantiatedPart.transform.parent = currentWeapon.transform;

        currentBarrel = instantiatedPart.GetComponent<Barrel>();

    }

    void SpawnMuzzle(List<GameObject> parts)
    {
        GameObject randomPart = GetRandomPart(parts);
        Transform socket = currentBarrel.muzzleSocket;

        GameObject instantiatedPart = Instantiate(randomPart, socket.position, socket.rotation);
        instantiatedPart.transform.parent = currentWeapon.transform;
    }
    GameObject GetRandomPart(List <GameObject> partsList)
    {
        int randomNumber= UnityEngine.Random.Range(0,partsList.Count);
        return partsList[randomNumber];
    }
}
