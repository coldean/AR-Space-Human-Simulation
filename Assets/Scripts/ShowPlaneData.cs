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
        List<PlaneData> planeDataList = GlobalData.planeDataList;

        Debug.Log(planeDataList.Count);
        planeDataText.text = "Plane Data:\n";
        foreach (var planeData in planeDataList)
        {
            Debug.Log($"Position: {planeData.Position}, Rotation: {planeData.Rotation}, Size: {planeData.Size}\n");
            planeDataText.text += $"Position: {planeData.Position}, Rotation: {planeData.Rotation}, Size: {planeData.Size}\n";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
