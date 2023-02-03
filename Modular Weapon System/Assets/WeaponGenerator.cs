using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGenerator : MonoBehaviour
{
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
            //DestroyCurrentWeapon();
            GenerateNewWeapon();
        }
    }

    private void GenerateNewWeapon()
    {
        Debug.Log("Generating new weapon");
        GameObject weapon = new GameObject();
        List<GameObject> weaponParts = new List<GameObject>();

        //Body
        GameObject body = GetRandomPart(bodyParts);
        //Instantiate(body,Vector3.zero,Quaternion.identity);
        Instantiate(body);
        weaponParts.Add(body);

        GameObject grip = GetRandomPart(gripParts);
        Instantiate(grip);
        weaponParts.Add(grip);
    }

    private void DestroyCurrentWeapon()
    {
        throw new NotImplementedException();
    }

    GameObject GetRandomPart(List <GameObject> partsList)
    {
        int randomNumber= UnityEngine.Random.Range(0,partsList.Count);
        return partsList[randomNumber];
    }
}
