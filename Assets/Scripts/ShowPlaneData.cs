using System.Collections;
using System.Collections.Generic;
using Structures;
using TMPro;
using UnityEngine;

public class ShowPlaneData : MonoBehaviour
{
    public TextMeshProUGUI planeDataText;
    // Start is called before the first frame update
    void Start()
    {
        PlaneData planeDataList = GetLocation.instance.planeDataList;

        planeDataText.text = "Plane Data:\n";
        foreach (var planeData in planeDataList.List)
        {
            planeDataText.text += $"Position: {planeData.Position}, Rotation: {planeData.Rotation}, Size: {planeData.Size}\n";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
