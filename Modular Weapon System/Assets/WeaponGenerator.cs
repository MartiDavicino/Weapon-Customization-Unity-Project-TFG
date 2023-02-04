using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGenerator : MonoBehaviour
{
    GameObject previousWeapon = null;

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
        Debug.Log("Generating new weapon");
        //GameObject weapon = new GameObject();
        //List<GameObject> weaponParts = new List<GameObject>();

        //Body
        GameObject randomBody = GetRandomPart(bodyParts);
        GameObject instantiatedBody=Instantiate(randomBody);
        Weapon weaponbody=instantiatedBody.GetComponent<Weapon>();

        SpawnPart(stockParts, weaponbody.stockSocket);
        SpawnPart(gripParts, weaponbody.gripSocket);
        SpawnPart(magazineParts, weaponbody.magazineSocket);
        SpawnPart(handguardParts, weaponbody.handguardSocket);
        SpawnPart(scopeParts, weaponbody.scopeSocket);

        //SpawnPart(barrelParts, );

        //SpawnPart(muzzleParts, );

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

    GameObject GetRandomPart(List <GameObject> partsList)
    {
        int randomNumber= UnityEngine.Random.Range(0,partsList.Count);
        return partsList[randomNumber];
    }
}
