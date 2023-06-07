using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGenerator : MonoBehaviour
{
    [HideInInspector]
    public bool customizationEnabled;

    public Weapon currentWeapon = null;

    [HideInInspector]
    GameObject previousCollection = null;
    [HideInInspector]
    GameObject previousWeapon = null;

    //private Handguard currentHandguard = null;

    //Pointers to save weapon parts
    GameObject currentStock = null;
    GameObject currentMagazine = null;
    GameObject currentGrip = null;

    GameObject currentBarrel = null;
    GameObject currentHandguard = null;
    GameObject currentMuzzle = null;
    GameObject currentScope = null;


    //Where prefabs are stored
    public List<GameObject> bodyParts;
    public List<GameObject> stockParts;
    public List<GameObject> magazineParts;
    public List<GameObject> gripParts;
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
        if (Input.GetKeyDown(KeyCode.G))
        {
            GenerateRandomWeapon();
        }
    }
    public void GenerateRandomWeapon()
    {
        customizationEnabled = true;

        DestroyCurrentWeapon();
        DestroyCurrentCollection();

        RandomizeWeaponElements();


        SpawnWeapon(Vector2.zero);
    }

    private void CheckWeaponConsistency()
    {
        //Change grip

        if (currentWeapon.type == WeaponType.NON_BULLPUP)
        {
            int index = UnityEngine.Random.Range(1, gripParts.Count);
            currentGripIndex = index;

        }
        if (currentWeapon.type == WeaponType.BULLPUP)
        {
            int index = UnityEngine.Random.Range(2, gripParts.Count);
            currentGripIndex = index;
        }
        if (currentWeapon.type == WeaponType.UMP)
        {
            //Dont do nothing
        }

        //Change stock
        if(gripParts[currentGripIndex].GetComponent<Grip>().type == GripType.INTEGRATED)
        {
            int index = UnityEngine.Random.Range(0, 1);
            currentStockIndex = index;
        }
        if (gripParts[currentGripIndex].GetComponent<Grip>().type == GripType.HYBRID)
        {
            currentStockIndex = 1;
        }
    }

    public void UpdateWeapon()
    {

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
                RandomizeWeaponElements();
                GameObject newWeapon=SpawnWeapon(newPos);
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

    private void RandomizeWeaponElements()
    {
        //currentBodyIndex=UnityEngine.Random.Range(0,bodyParts.Count);

        currentBodyIndex++; if (currentBodyIndex > bodyParts.Count - 1) currentBodyIndex = 0;

        currentMagazineIndex = GetRandomIndex(WeaponPart.MAGAZINE, magazineParts);
        currentScopeIndex = GetRandomIndex(WeaponPart.SCOPE, scopeParts);
        currentGripIndex = GetRandomIndex(WeaponPart.GRIP, gripParts);
        currentStockIndex = GetRandomIndex(WeaponPart.STOCK, stockParts);
        currentHandguardIndex = GetRandomIndex(WeaponPart.HANDGUARD, handguardParts);
        currentBarrelIndex = GetRandomIndex(WeaponPart.BARREL, barrelParts);
        currentMuzzleIndex = GetRandomIndex(WeaponPart.MUZZLE, muzzleParts);

    }
    private GameObject SpawnWeapon(Vector2 pos)
    {

        //Body
        GameObject weaponBody = bodyParts[currentBodyIndex];
        GameObject instantiatedBody =Instantiate(weaponBody, pos,Quaternion.Euler(-90,0,0));
        currentWeapon=instantiatedBody.GetComponent<Weapon>();

        
            CheckWeaponConsistency();
        

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

    GameObject SpawnPart(WeaponPart weaponPart,List<GameObject> parts,int index,Transform socket)
    {

        GameObject part = parts[index];
        GameObject instantiatedPart = null;
        instantiatedPart = Instantiate(part, socket.position, socket.rotation);
        instantiatedPart.transform.parent = socket;
        
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
        SpawnPart(WeaponPart.MAGAZINE, magazineParts,currentMagazineIndex, currentWeapon.magazineSocket);
    }
    void SpawnScope()
    {
        SpawnPart(WeaponPart.SCOPE, scopeParts, currentScopeIndex, currentWeapon.scopeSocket);
    }
    void SpawnStock()
    {
        SpawnPart(WeaponPart.STOCK, stockParts, currentStockIndex, currentWeapon.stockSocket);
    }
    void SpawnHandguard()
    {
        SpawnPart(WeaponPart.HANDGUARD, handguardParts, currentHandguardIndex, currentWeapon.handguardSocket);
    }
    void SpawnGrip()
    { 
        SpawnPart(WeaponPart.GRIP, gripParts,currentGripIndex, currentWeapon.gripSocket);
    }
    void SpawnBarrel()
    {
        SpawnPart(WeaponPart.BARREL, barrelParts, currentBarrelIndex, currentHandguard.GetComponent<Handguard>().barrelSocket);
    }
    void SpawnMuzzle()
    {
        SpawnPart(WeaponPart.MUZZLE, muzzleParts, currentMuzzleIndex, currentBarrel.GetComponent<Barrel>().muzzleSocket);
    }

    int GetRandomIndex(WeaponPart weaponPart,List<GameObject> partsList)
    {
        int index= UnityEngine.Random.Range(0, partsList.Count);


        switch (weaponPart)
        {
            case WeaponPart.SCOPE:
                {
                    currentScopeIndex = index;
                }
                break;
            case WeaponPart.GRIP:
                {
                    currentGripIndex = index;
                }
                break;
            case WeaponPart.MAGAZINE:
                {                 
                    currentMagazineIndex = index;
                }
                break;
            case WeaponPart.STOCK:
                {
                    currentStockIndex = index;
                }
                break;
            case WeaponPart.HANDGUARD:
                {
                    currentHandguardIndex = index;
                }
                break;
            case WeaponPart.BARREL:
                {
                   currentBarrelIndex = index;
                }
                break;
            case WeaponPart.MUZZLE:
                {
                    currentMuzzleIndex = index;
                }
                break;
        }
        return index;
    }

    int GetNextIndexPart(WeaponPart weaponPart,List<GameObject> partsList)
    {
        //Here we take considerations about weapon restrictions
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

                    if (currentWeapon != null)
                    {

                        if (currentWeapon.type == WeaponType.BULLPUP)
                        {

                            //We test it twice because there are in the list two incompatible grips with bullpups
                            if (partsList[currentGripIndex].GetComponent<Grip>().type != GripType.SIMPLE)
                            {
                                currentGripIndex++;
                                if (currentGripIndex > partsList.Count - 1) currentGripIndex = 0;
                            }
                            if (partsList[currentGripIndex].GetComponent<Grip>().type != GripType.SIMPLE)
                            {
                                currentGripIndex++;
                                if (currentGripIndex > partsList.Count - 1) currentGripIndex = 0;
                            }
                        }
                        //Non Bullpup cases
                        else 
                        {
                            if(stockParts[currentStockIndex].GetComponent<Stock>().type == StockType.SIMPLE)
                            {
                                //If stock is simple we wont use the two first grips
                                if (partsList[currentGripIndex].GetComponent<Grip>().type == GripType.HYBRID)
                                {
                                    Debug.Log("No deberia 1");

                                    currentGripIndex++;
                                    if (currentGripIndex > partsList.Count - 1) currentGripIndex = 0;
                                }
                                if (partsList[currentGripIndex].GetComponent<Grip>().type == GripType.INTEGRATED)
                                {
                                    Debug.Log("No deberia 2");
                                    currentGripIndex++;
                                    if (currentGripIndex > partsList.Count - 1) currentGripIndex = 0;
                                }
                                
                            }
                            //Cases for reduced and none stocks
                            else 
                            {
                                if (partsList[currentGripIndex].GetComponent<Grip>().type == GripType.HYBRID)
                                {
                                    if (currentWeapon.type == WeaponType.UMP)
                                    {
                                        currentStockIndex = 1;
                                        UpdateStock();  
                                    }
                                    else
                                    {
                                        currentGripIndex++;
                                        if (currentGripIndex > partsList.Count - 1) currentGripIndex = 0;
                                    }
                                        
                                }
                            }
                         }
                    }
                    

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
                    if (gripParts[currentGripIndex].GetComponent<Grip>().type == GripType.SIMPLE)
                    {
                        currentStockIndex++;
                        if (currentStockIndex > partsList.Count - 1) currentStockIndex = 0;
                        index = currentStockIndex;
                    }

                    if (gripParts[currentGripIndex].GetComponent<Grip>().type == GripType.INTEGRATED)
                    {
                        if (currentStockIndex == 0) currentStockIndex = 1;
                        else currentStockIndex = 0;
                    }
                    if (gripParts[currentGripIndex].GetComponent<Grip>().type == GripType.HYBRID)
                    {
                        Debug.Log("Dont do anything");
                    }

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
        return index;
    }


    //Customization functions
    public void ChangeStock()
    {
        if(currentStock != null)
        {
            GetNextIndexPart(WeaponPart.STOCK, stockParts);
            Destroy(currentStock);
            SpawnStock();
        }
    }
    public void UpdateStock()
    {
        if (currentStock != null)
        {
            Destroy(currentStock);
            SpawnStock();
        }
    }
    public void UpdateGrip()
    {
        if (currentGrip != null)
        {
            Destroy(currentGrip);
            SpawnGrip();
        }
    }
    public void ChangeScope()
    {
        if (currentScope != null)
        {
            GetNextIndexPart(WeaponPart.SCOPE, scopeParts);
            Destroy(currentScope);
            SpawnScope();
        }

    }
    public void ChangeMagazine()
    {
        if (currentMagazine != null)
        {
            GetNextIndexPart(WeaponPart.MAGAZINE, magazineParts);
            Destroy(currentMagazine);
            SpawnMagazine();
        }
    }
    public void ChangeMuzzle()
    {
        if (currentMuzzle != null)
        {
            GetNextIndexPart(WeaponPart.MUZZLE,muzzleParts);
            Destroy(currentMuzzle);
            SpawnMuzzle();
        }
    }
    public void ChangeGrip()
    {
        if (currentGrip != null)
        {
            GetNextIndexPart(WeaponPart.GRIP, gripParts);
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

            GetNextIndexPart(WeaponPart.BARREL, barrelParts);
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

            GetNextIndexPart(WeaponPart.HANDGUARD,handguardParts);
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
