using UnityEngine;
using System.Collections;
using System;

public class CameraController : MonoBehaviour
{
    InterfaceController interfaceController;

    [SerializeField] private Camera myCamera;
    [SerializeField] private Transform target;
    [SerializeField] private float distanceToTarget;

    private Vector3 previousPosition;

    private float zoomRatio = 0;
    private float zoomIncrement = 2.0f;
    private int zoomInClamp = 10;
    private int zoomOutClamp = 100;

    public bool isOrbiting = false;

    private void Start()
    {
        interfaceController =GameObject.Find("Interface").GetComponent<InterfaceController>();

        distanceToTarget=Vector3.Distance(GetComponent<Camera>().transform.position, target.position);
    }
    private void Update()
    {
        OrbitAroundObject();
        Zoom();
    }

    private void Zoom()
    {
        if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            GetComponent<Camera>().fieldOfView += zoomIncrement;

            if(GetComponent<Camera>().fieldOfView >=zoomOutClamp)
                GetComponent<Camera>().fieldOfView = zoomOutClamp;
        }
        else if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            GetComponent<Camera>().fieldOfView-=zoomIncrement;

            if (GetComponent<Camera>().fieldOfView <= zoomInClamp)
                GetComponent<Camera>().fieldOfView = zoomInClamp;
        }

    }

    private void OrbitAroundObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isOrbiting = true;
            previousPosition = GetComponent<Camera>().ScreenToViewportPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {

            Vector3 newPosition = GetComponent<Camera>().ScreenToViewportPoint(Input.mousePosition);
            Vector3 direction = previousPosition - newPosition;

            float rotationAroundYAxis = -direction.x * 180; // camera moves horizontally
            float rotationAroundXAxis = direction.y * 180; // camera moves vertically

            GetComponent<Camera>().transform.position = target.position;

            GetComponent<Camera>().transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);
            GetComponent<Camera>().transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World);

            GetComponent<Camera>().transform.Translate(new Vector3(0, 0, -distanceToTarget));

            previousPosition = newPosition;
        }
        else
            isOrbiting=false;

    }

    


}