using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform seagullTransform;
    private Vector3 cameraOffset;
    // Start is called before the first frame update
    void Start()
    {
        cameraOffset = seagullTransform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = seagullTransform.position - cameraOffset;
    }
}