using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraRotation : MonoBehaviour
{
    public GameObject camera;
    public Vector2 angleOffset = new Vector2();

    // Update is called once per frame
    void Update()
    {
        Vector3 CameraRotation = camera.transform.rotation.eulerAngles;
 
        CameraRotation.x = (-Input.mousePosition.y/100)+angleOffset.x;
        CameraRotation.y = (Input.mousePosition.x/100)+angleOffset.y;

        camera.transform.rotation = Quaternion.Euler(CameraRotation);

    }
}
