using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {

    public Camera billboardCamera;
    private void Start()
    {
        billboardCamera = Camera.main;
    }
    void Update()
    {
        // TODO: Change the code so that the way it works is a child object with no rotation has the +Y axis facing towards the camera
        transform.LookAt(transform.position + billboardCamera.transform.rotation * Vector3.forward, billboardCamera.transform.rotation * Vector3.up);
    }
}
