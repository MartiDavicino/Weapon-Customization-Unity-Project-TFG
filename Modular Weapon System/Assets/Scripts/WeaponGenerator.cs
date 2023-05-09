using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGenerator : MonoBehaviour
{
    [HideInInspector]
    public bool customizationEnabled;

    Weapon currentWeapon = null;

    GameObject previousCollection = null;
    GameObject previousWeapon = null;

    //private Handguard currentHandguard = null;

    //Pointers to save weapon parts
    GameObject currentStock = null;
    GameObject currentMagazine = null;
    GameObject currentGrip = null;
    GameObject currentForegrip = null;
    public GameObject currentBarrel = null;
    GameObject currentHandguard = null;
    public GameObject currentMuzzle = null;
    GameObject currentScope = null;


    //Where prefabs are stored
    public List<GameObject> bodyParts;
    public List<GameObject> stockParts;
    public List<GameObject> magazineParts;
    public List<GameObject> gripParts;
    //public List<GameObject> foregripParts;
    public List<GameObject> barrelParts;
    public List<GameObject> handguardParts;
    public List<GameObject> muzzleParts;
    public List<GameObject> scopeParts;

    public int currentBodyIndex,currentStockIndex,
    currentMagazineIndex,currentGripIndex,currentBarrelIndex,
    currentHandguardIndex,currentMuzzleIndex,currentScopeIndex;
    // Start is called before the first frame update
    void Start()
    {
        customizationEnabled = false;
        
        currentBodyIndex=currentStockIndex=
        currentMagazineIndex=currentGripIndex=currentBarrelIndex=
        currentHandguardIndex=currentMuzzleIndex=currentScopeIndex=0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateNewWeapon()
    {
        customizationEnabled = true;

        DestroyCurrentWeapon();
        DestroyCurrentCollection();

        CreateWeapon(Vector2.zero);
    }
    private void DestroyCurrentWeapon()
    {
        if (previousWeapon != null)
        {
            Destroy(previousWeapon);
        }
    }
    public void GenerateCollection()
    {
        customizationEnabled=false;

        DestroyCurrentWeapon();
        DestroyCurrentCollection();

        GameObject collection=new GameObject();
        collection.name = "Weapon Collection";
        int rows, columns;
        rows = 6;
        columns = 6;

        //Vector3 pos=new Vector2(0,0);
        Vector2 increment=new Vector2(4,1.5f);

        for(int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                
                Vector2 newPos = new Vector2((increment.x * i), (increment.y * j));
                GameObject newWeapon=CreateWeapon(newPos);
                newWeapon.transform.parent = collection.transform;
            }
        }
        Vector3 collectionPos = new Vector3(-(increment.x * rows) / 2, -(increment.y * columns) / 2, -10);
        collection.transform.position = collectionPos;
        previousCollection = collection;
    }
    private void DestroyCurrentCollection()
    {
        if (previousCollection != null)
        {
            Destroy(previousCollection);
        }
    }
    private GameObject CreateWeapon(Vector2 pos)
    {
        
        //Body
        GameObject randomBody = GetRandomPart(bodyParts);
        GameObject instantiatedBody=Instantiate(randomBody,pos,Quaternion.Euler(-90,0,0));
        currentWeapon=instantiatedBody.GetComponent<Weapon>();

        SpawnMagazine();
        SpawnScope();
        SpawnGrip();
        SpawnStock();
        SpawnHandguard();

        SpawnBarrel();
        SpawnMuzzle();


        previousWeapon = instantiatedBody;
        return instantiatedBody;
    }

    GameObject SpawnPart(WeaponPart weaponPart,List<GameObject> parts,Transform socket)
    {
        //GameObject randomPart=GetRandomPart(parts);
        GameObject randomPart = GetPartFromList(parts,weaponPart);

        GameObject instantiatedPart = null;
        instantiatedPart = Instantiate(randomPart, socket.position, socket.rotation);
        instantiatedPart.transform.parent = socket;
        //instantiatedPart.transform.localScale = new Vector3(10,10,10);
        
        switch (weaponPart)
        {
            case WeaponPart.MAGAZINE:
                {
                    currentMagazine = instantiatedPart;
                }
                break;
            case WeaponPart.HANDGUARD:
                {
                    currentHandguard = instantiatedPart;
                }
                break;
            case WeaponPart.STOCK:
                {
                    currentStock = instantiatedPart;
                }
                break;
            case WeaponPart.GRIP:
                {

                    //if (currentWeapon.type == WeaponType.BULLPUP || !currentWeapon.useComplexGrip)
                    //{
                    //    Destroy(instantiatedPart);

                    //    while (randomPart.GetComponent<Grip>().type != GripType.SIMPLE)
                    //    {
                    //        randomPart = GetRandomPart(parts);
                    //    }
                    //}
                    //else if (currentWeapon.useComplexGrip)
                    //{
                    //    Destroy(instantiatedPart);

                    //    while (randomPart.GetComponent<Grip>().type == GripType.SIMPLE)
                    //    {
                    //        randomPart = GetRandomPart(parts);
                    //    }
                    //}



                    instantiatedPart = Instantiate(randomPart, socket.position, socket.rotation);
                    instantiatedPart.transform.parent = socket;

                    currentGrip = instantiatedPart;
                }
                break;
            case WeaponPart.BARREL:
                {
                    currentBarrel = instantiatedPart;
                }
                break;
            case WeaponPart.SCOPE:
                {
                    currentScope = instantiatedPart;
                }
                break;
            case WeaponPart.MUZZLE:
                {
                    currentMuzzle = instantiatedPart;
                }
                break;

        }
        

        return instantiatedPart;
    }

    void SpawnMagazine()
    {
        SpawnPart(WeaponPart.MAGAZINE, magazineParts, currentWeapon.magazineSocket);
    }
    GameObject SpawnScope()
    {
        if (currentWeapon.UsePart(WeaponPart.SCOPE))
        {
            return SpawnPart(WeaponPart.SCOPE, scopeParts, currentWeapon.scopeSocket);
        }
        else
            return null;
    }
    void SpawnStock()
    {
        if (!currentWeapon.useComplexGrip)
        {
            if (currentWeapon.UsePart(WeaponPart.STOCK))
            {
                SpawnPart(WeaponPart.STOCK, stockParts, currentWeapon.stockSocket);

            }
        }
    }
    void SpawnHandguard()
    {
        if (currentWeapon.UsePart(WeaponPart.HANDGUARD))
        {
            SpawnPart(WeaponPart.HANDGUARD,handguardParts, currentWeapon.handguardSocket);
        }
    }
    void SpawnGrip()
    {
        currentWeapon.UsePart(WeaponPart.GRIP);
        
        SpawnPart(WeaponPart.GRIP, gripParts, currentWeapon.gripSocket);
    }
    void SpawnBarrel()
    {
        if (currentWeapon.UsePart(WeaponPart.BARREL))
        {
        
            if(currentWeapon.useHandguard)
                SpawnPart(WeaponPart.BARREL, barrelParts, currentHandguard.GetComponent<Handguard>().barrelSocket);
            else
                SpawnPart(WeaponPart.BARREL, barrelParts, currentWeapon.handguardSocket);

        }

    }
    void SpawnMuzzle()
    {
        if (currentWeapon.UsePart(WeaponPart.MUZZLE))
        {
            if(currentWeapon.useBarrel)
                SpawnPart(WeaponPart.MUZZLE, muzzleParts, currentBarrel.GetComponent<Barrel>().muzzleSocket);
            else if (currentWeapon.useHandguard)
                SpawnPart(WeaponPart.MUZZLE, muzzleParts, currentHandguard.GetComponent<Handguard>().barrelSocket);
            else
                SpawnPart(WeaponPart.MUZZLE, muzzleParts, currentWeapon.handguardSocket);

        }

    }

    GameObject GetRandomPart(List <GameObject> partsList)
    {
        int randomNumber= UnityEngine.Random.Range(0,partsList.Count);
        return partsList[randomNumber];
    }

    GameObject GetPartFromList(List<GameObject> partsList,WeaponPart weaponPart)
    {
        int index = 0;
        switch(weaponPart)
        {
            case WeaponPart.SCOPE:
                {
                    currentScopeIndex++;
                    if (currentScopeIndex > partsList.Count-1) currentScopeIndex = 0;
                    index = currentScopeIndex;
                }
                break;
            case WeaponPart.GRIP:
                {
                    currentGripIndex++;
                    if (currentGripIndex > partsList.Count - 1) currentGripIndex = 0;
                    index = currentGripIndex;
                }
                break;
            case WeaponPart.MAGAZINE:
                {
                    currentMagazineIndex++;
                    if (currentMagazineIndex > partsList.Count - 1) currentMagazineIndex = 0;
                    index = currentMagazineIndex;
                }
                break;
            case WeaponPart.STOCK:
                {
                    currentStockIndex++;
                    if (currentStockIndex > partsList.Count - 1) currentStockIndex = 0;
                    index = currentStockIndex;
                }
                break;
            case WeaponPart.HANDGUARD:
                {
                    currentHandguardIndex++;
                    if (currentHandguardIndex > partsList.Count - 1) currentHandguardIndex = 0;
                    index = currentHandguardIndex;
                }
                break;
            case WeaponPart.BARREL:
                {
                    currentBarrelIndex++;
                    if (currentBarrelIndex > partsList.Count - 1) currentBarrelIndex = 0;
                    index = currentBarrelIndex;
                }
                break;
            case WeaponPart.MUZZLE:
                {
                    currentMuzzleIndex++;
                    if (currentMuzzleIndex > partsList.Count - 1) currentMuzzleIndex = 0;
                    index = currentMuzzleIndex;
                }
                break;
        }
        return partsList[index];
    }


    //Customization functions
    public void ChangeStock()
    {
        if(currentStock != null)
        {
            Destroy(currentStock);
            SpawnStock();
        }
    }
    public void ChangeScope()
    {
        

        if (currentScope != null)
        {
            Destroy(currentScope);
            SpawnScope();
        }

    }
    public void ChangeMagazine()
    {
        if (currentMagazine != null)
        {
            Destroy(currentMagazine);
            SpawnMagazine();
        }
    }
    public void ChangeMuzzle()
    {
        if (currentMuzzle != null)
        {
            Destroy(currentMuzzle);
            SpawnMuzzle();
        }
    }
    public void ChangeGrip()
    {
        if (currentGrip != null)
        {
            Destroy(currentGrip);
            SpawnGrip();
        }

    }
    public void ChangeBarrel()
    {
        if (currentBarrel != null)
        {
            if(currentMuzzle!= null)
            {
                currentMuzzle.transform.parent = GameObject.Find("Weapon Generator").transform.parent;
            }

            Destroy(currentBarrel);
            SpawnBarrel();

            if(currentMuzzle!=null)
            {
                currentMuzzle.transform.parent = currentBarrel.transform;
                currentMuzzle.transform.position = currentBarrel.GetComponent<Barrel>().muzzleSocket.position;
            }
            

        }
    }
    public void ChangeHandguard()
    {
       
        if (currentHandguard != null)
        {
            if (currentMuzzle != null)
                currentMuzzle.transform.parent = GameObject.Find("Weapon Generator").transform.parent;
            if (currentBarrel != null)
                currentBarrel.transform.parent = GameObject.Find("Weapon Generator").transform.parent;

            Destroy(currentHandguard);
            SpawnHandguard();

            if (currentBarrel != null)
            {
                currentBarrel.transform.parent = currentHandguard.transform;
                currentBarrel.transform.position = currentHandguard.GetComponent<Handguard>().barrelSocket.position;
            }
            if (currentMuzzle != null)
            {
                currentMuzzle.transform.parent = currentBarrel.transform;
                currentMuzzle.transform.position = currentBarrel.GetComponent<Barrel>().muzzleSocket.position;
            }
            
        }
    }
    public void ChangeBody()
    {
        if (currentWeapon != null)
        {
            //Store all
        }
    }

}
