using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraRotation : MonoBehaviour
{
    public GameObject camera;
    

    // Update is called once per frame
    void Update()
    {
        Vector3 CameraRotation = camera.transform.rotation.eulerAngles;
 
        CameraRotation.x = -Input.mousePosition.y/100;
        CameraRotation.y = Input.mousePosition.x/100;

        camera.transform.rotation = Quaternion.Euler(CameraRotation);

    }
}
