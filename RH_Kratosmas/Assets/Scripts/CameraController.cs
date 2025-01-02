using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraController : MonoBehaviour
{
    public Transform seagullTransform;
    private Vector3 cameraOffset;
    private float followDistance = 5f;
    public Volume volume;
    private ColorAdjustments colorAdjustments;
    private Color underwaterColor = new Color(2f / 255f, 90f / 255f, 135f / 255f);// (0f, 101f / 255f, 178f / 255f);

    // Start is called before the first frame update
    void Start()
    {
        cameraOffset = seagullTransform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = seagullTransform.position - seagullTransform.forward * followDistance;
        transform.LookAt(seagullTransform);

        if(transform.position.y < 5)
        {
            if (volume != null)
            {
                volume.profile.TryGet(out ColorAdjustments colorAdjustments);
                if (colorAdjustments != null)
                {
                    colorAdjustments.colorFilter.value = underwaterColor;
                }
            }
        }
        else
        {
            if (volume != null)
            {
                volume.profile.TryGet(out ColorAdjustments colorAdjustments);
                if (colorAdjustments != null)
                {
                    colorAdjustments.colorFilter.value = Color.white;
                }
            }
        }
    }
}
