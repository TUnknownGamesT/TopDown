using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBlocks : MonoBehaviour
{
    
    public List<Constants.Tags> tag;

    private readonly List<string> _tagsString = new();

    private void Awake()
    {
        foreach (var value in tag)
        {
            _tagsString.Add(value.ToString());
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (_tagsString.Contains(collision.gameObject.tag))
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 20, ForceMode.Impulse);
        } 
    }
}
