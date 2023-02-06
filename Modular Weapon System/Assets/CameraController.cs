using UnityEngine;
using System.Collections;
using System;

public class CameraController : MonoBehaviour
{
    InterfaceController interfaceController;

    [SerializeField] private Camera camera;
    [SerializeField] private Transform target;
    [SerializeField] private float distanceToTarget;

    private Vector3 previousPosition;

    private float zoomRatio = 0;
    private float zoomIncrement = 2.0f;
    private int zoomInClamp = 10;
    private int zoomOutClamp = 100;
    private void Start()
    {
        interfaceController =GameObject.Find("Interface").GetComponent<InterfaceController>();

        distanceToTarget=Vector3.Distance(camera.transform.position, target.position);
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
            camera.fieldOfView += zoomIncrement;

            if(camera.fieldOfView >=zoomOutClamp)
                camera.fieldOfView = zoomOutClamp;
        }
        else if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            camera.fieldOfView-=zoomIncrement;

            if (camera.fieldOfView <= zoomInClamp)
                camera.fieldOfView = zoomInClamp;
        }

    }

    private void OrbitAroundObject()
    {
        if (Input.GetMouseButtonDown(0))
        {

            previousPosition = camera.ScreenToViewportPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {

            Vector3 newPosition = camera.ScreenToViewportPoint(Input.mousePosition);
            Vector3 direction = previousPosition - newPosition;

            float rotationAroundYAxis = -direction.x * 180; // camera moves horizontally
            float rotationAroundXAxis = direction.y * 180; // camera moves vertically

            camera.transform.position = target.position;

            camera.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);
            camera.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World);

            camera.transform.Translate(new Vector3(0, 0, -distanceToTarget));

            previousPosition = newPosition;
        }

    }
}