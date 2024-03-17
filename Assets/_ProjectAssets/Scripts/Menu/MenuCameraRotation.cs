using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraRotation : MonoBehaviour
{
    public GameObject camera;
    public Vector2 angleOffset = new Vector2();
    [SerializeField] private Vector3 lvlSelectPosition, lvlSelectRotation;
    [SerializeField] private float moveTime;

    private bool lvlSelect = false;
    private bool isInLeanTween = false;

    private void Start()
    {
        lvlSelect = false;
        isInLeanTween = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInLeanTween) return;

        Vector3 CameraRotation = camera.transform.rotation.eulerAngles;
        if (!lvlSelect)
        {
            CameraRotation.x = (-Input.mousePosition.y / 100) + angleOffset.x;
            CameraRotation.y = (Input.mousePosition.x / 100) + angleOffset.y;
        }
        else
        {
            CameraRotation.x = (-Input.mousePosition.y / 100) + lvlSelectRotation.x;
            CameraRotation.y = (Input.mousePosition.x / 100) + lvlSelectRotation.y;
        }

        camera.transform.rotation = Quaternion.Euler(CameraRotation);
    }

    public void GoToLevelSelect()
    {
        if (!isInLeanTween)
        {
            try
            {
                isInLeanTween = true;
                LeanTween.move(camera, lvlSelectPosition, moveTime);
                LeanTween.rotate(camera, lvlSelectRotation, moveTime).setOnComplete(() =>
                {
                    isInLeanTween = false;
                });
                lvlSelect = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}