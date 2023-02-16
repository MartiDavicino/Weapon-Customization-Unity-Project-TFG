using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectablePart : MonoBehaviour
{
    private Renderer renderer;
    CameraController cameraController;
    WeaponGenerator weaponGenerator;

    // Start is called before the first frame update
    void Start()
    {

        cameraController =GameObject.Find("Main Camera").GetComponent<CameraController>();
        if (cameraController == null)
            Debug.Log("Camera Controller is null");

        weaponGenerator = GameObject.Find("Weapon Generator").GetComponent<WeaponGenerator>();
        if (weaponGenerator == null)
            Debug.Log("Weapon Generator  is null");

        renderer = GetComponent<Renderer>();
        if (renderer == null)
            Debug.Log("Renderer is null");

        renderer.material.color = Color.white;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseEnter()
    {

        if (!cameraController.isOrbiting)
        {
            renderer.material.color = Color.yellow;

        }
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //renderer.material.color = Color.blue;

            string partTag = tag;
            Debug.Log("Change " + tag);
            switch(partTag)
            {
                case "Barrel":
                    weaponGenerator.ChangeBarrel();
                    break;
                case "Muzzle":
                    weaponGenerator.ChangeMuzzle();
                    break;
                case "Handguard":
                    weaponGenerator.ChangeHandguard();
                    break;
                case "Stock":
                    weaponGenerator.ChangeStock();
                    break;
                case "Scope":
                    weaponGenerator.ChangeScope();
                    break;
                case "Magazine":
                    weaponGenerator.ChangeMagazine();
                    break;
            }
            
        }

    }

    private void OnMouseExit()
    {
        
            renderer.material.color = Color.white;
        
    }
}
