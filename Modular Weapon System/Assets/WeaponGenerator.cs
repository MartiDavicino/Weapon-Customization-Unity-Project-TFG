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
        //GenerateNewWeapon();
        GenerateCollection();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GenerateCollection();
        }
    }

    public void GenerateNewWeapon()
    {
        DestroyCurrentWeapon();
        CreateWeapon(Vector2.zero);
    }

    

    [Obsolete]
    public void GenerateCollection()
    {
        GameObject collection=new GameObject();
        collection.name = "Weapon Collection";
        int rows, columns;
        rows = 10;
        columns = 10;

        //Vector3 pos=new Vector2(0,0);
        Vector2 increment=new Vector2(40,15);

        for(int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                
                Vector2 newPos = new Vector2((increment.x * i), (increment.y * j));
                GameObject newWeapon=CreateWeapon(newPos);
                newWeapon.transform.parent = collection.transform;
            }
        }
        Vector3 collectionPos = new Vector3(-(increment.x * rows) / 2, -(increment.y * columns) / 2, -40);
        collection.transform.position = collectionPos;
    }

    private GameObject CreateWeapon(Vector2 pos)
    {
        
        //Body
        GameObject randomBody = GetRandomPart(bodyParts);
        GameObject instantiatedBody=Instantiate(randomBody,pos,Quaternion.Euler(-90,0,0));
        currentWeapon=instantiatedBody.GetComponent<Weapon>();

        SpawnPart(magazineParts, currentWeapon.magazineSocket);
        if(currentWeapon.UsePart(WeaponPart.SCOPE)) SpawnPart(scopeParts, currentWeapon.scopeSocket);

        SpawnGrip(gripParts, currentWeapon.gripSocket,currentWeapon.type);
        if(!currentWeapon.useComplexGrip)if(currentWeapon.UsePart(WeaponPart.STOCK))SpawnPart(stockParts, currentWeapon.stockSocket);

        if (currentWeapon.UsePart(WeaponPart.HANDGUARD)) SpawnHandguard(handguardParts, currentWeapon.handguardSocket);
        if (currentWeapon.UsePart(WeaponPart.BARREL)) SpawnBarrel(barrelParts);
        if (currentWeapon.UsePart(WeaponPart.MUZZLE)) SpawnMuzzle(muzzleParts);


        previousWeapon = instantiatedBody;
        return instantiatedBody;
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
            while (randomPart.GetComponent<Grip>().type != GripType.SIMPLE)
            {
                randomPart = GetRandomPart(parts);
            }
        }


        GameObject instantiatedPart = Instantiate(randomPart, socket.position, socket.rotation);
        instantiatedPart.transform.parent = socket;

        if (randomPart.GetComponent<Grip>().type == GripType.STOCK || randomPart.GetComponent<Grip>().type == GripType.INTEGRED)
              currentWeapon.useComplexGrip = true;

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
        Transform socket;
        if (currentWeapon.useHandguard)
            socket = currentHandguard.barrelSocket;
        else
            socket = currentWeapon.handguardSocket;

        GameObject instantiatedPart = Instantiate(randomPart, socket.position, socket.rotation);
        instantiatedPart.transform.parent = currentWeapon.transform;

        currentBarrel = instantiatedPart.GetComponent<Barrel>();

    }

    void SpawnMuzzle(List<GameObject> parts)
    {
        GameObject randomPart = GetRandomPart(parts);
        Transform socket;
        if (currentWeapon.useBarrel)
            socket = currentBarrel.muzzleSocket;
        else if (currentWeapon.useHandguard)
            socket = currentHandguard.barrelSocket;
        else
            socket = currentWeapon.handguardSocket;

        GameObject instantiatedPart = Instantiate(randomPart, socket.position, socket.rotation);
        instantiatedPart.transform.parent = currentWeapon.transform;
    }
    GameObject GetRandomPart(List <GameObject> partsList)
    {
        int randomNumber= UnityEngine.Random.Range(0,partsList.Count);
        return partsList[randomNumber];
    }
}
