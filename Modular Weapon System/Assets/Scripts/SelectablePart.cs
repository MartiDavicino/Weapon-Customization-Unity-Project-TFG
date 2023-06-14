using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectablePart : MonoBehaviour
{
    private Renderer renderer;
    CameraController cameraController;
    WeaponGenerator weaponGenerator;
    public List<Material> partMaterials;

    private Color highlightColor;
    private Material highlightMat;

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

        highlightColor=new Color(0,0.2f,1);

        highlightMat=GameObject.Find("Highlight Object").GetComponent<Renderer>().material;
        if (highlightMat == null) Debug.Log("could find mat");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseEnter()
    {
        if(weaponGenerator.customizationEnabled)
        {
            renderer.material.color = Color.red;

            if (tag!="Body")
            {
                for(int i=0; i<renderer.materials.Length;i++)
                {
                    //renderer.materials[i].color = highlightColor;

                    renderer.material = highlightMat;
                }


            }
        }

        
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && weaponGenerator.customizationEnabled)
        {
            //renderer.material.color = Color.blue;

            //Debug.Log("Change " + tag);
            switch(tag)
            {
                case "Grip":
                    weaponGenerator.ChangeGrip();
                    break;
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
                //case "Body":
                //    weaponGenerator.GenerateNewWeapon();
                //    break;
            }
            
        }

    }

    private void OnMouseExit()
    {
        //renderer.material.color = Color.white;

        for (int i = 0; i < renderer.materials.Length; i++)
        {
            //renderer.materials[i].color = partMaterials[i].color;

            renderer.material = partMaterials[i];
        }
    }
}
