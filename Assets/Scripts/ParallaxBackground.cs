using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField]
    private Vector3 parallaxMultiplaer;
    [SerializeField]
    private Transform cameraPosition;
    private Vector3 lastCameraPosition;

    // Start is called before the first frame update
    void Start()
    {
        //cameraPosition = Camera.current.transform;
        lastCameraPosition =  cameraPosition.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var deltaMove = cameraPosition.position - lastCameraPosition;
        transform.position += new Vector3(deltaMove.x * parallaxMultiplaer.x, deltaMove.y * parallaxMultiplaer.y);
        lastCameraPosition = cameraPosition.position;
    }
}
