using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class GetLocation : MonoBehaviour
{
    /*
    private ARPlaneManager _arPlaneManager;
    // Start is called before the first frame update
    void Start()
    {
        _arPlaneManager = GetComponent<ARPlaneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var plane in _arPlaneManager.trackables)
        {
            // 평면의 월드 좌표 추출
            Vector3 planePosition = plane.transform.position;
            Quaternion planeRotation = plane.transform.rotation;
            Debug.Log($"Plane position: {planePosition}, Plane rotation: {planeRotation}");
        }
    }
    */
    public ARPlaneManager planeManager;

    void Start()
    {
        planeManager = GetComponent<ARPlaneManager>();
        planeManager.planesChanged += OnPlanesChanged;
    }

    void OnPlanesChanged(ARPlanesChangedEventArgs eventArgs)
    {
        foreach (var plane in eventArgs.added)
        {
            Debug.Log("Detected new plane: " + plane);
            Debug.Log("Position: " + plane.transform.position);
            Debug.Log("Size: " + plane.size);
            Debug.Log("Rotation: " + plane.transform.rotation);
        }
    }
}
