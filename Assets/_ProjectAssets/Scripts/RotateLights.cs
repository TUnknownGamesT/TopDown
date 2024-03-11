using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateLights : MonoBehaviour
{
    public float rotationSpeed = 3f;
    public float offset = 75f;
    void Start()
    {
        StartCoroutine(GiveDelay());
    }   

    IEnumerator GiveDelay()
    {
        yield return new WaitForSeconds(Random.Range(0, 2f));
        RotateObject();
    }
    void RotateObject()
    {
        Vector3 rotationTarget = new Vector3(90+Random.Range(-offset, offset), Random.Range(-offset, offset),
            Random.Range(-offset, offset));
        LeanTween.rotate(gameObject, rotationTarget, rotationSpeed)
            .setOnComplete(() => RotateObject());
    }
}
