using System.Collections;
using System.Collections.Generic;
using Structures;
using UnityEngine;

public class GlobalData
{
    public static List<PlaneData> planeDataList = new List<PlaneData>();
    public static List<LocationProbability> locations = new List<LocationProbability>();
    public static List<Vector3> touchPositions = new List<Vector3>();   // save touched positions here
    public static bool showPerson = false;

    static GlobalData()
    {
        // 무작위 LocationProbability 값 2개 생성
        locations.Add(new LocationProbability
        {
            location = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f)),
            probability = Random.Range(0f, 1f),
            count = Random.Range(1, 10) // 필요 시 수정
        });

        locations.Add(new LocationProbability
        {
            location = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f)),
            probability = Random.Range(0f, 1f),
            count = Random.Range(1, 10) // 필요 시 수정
        });
    }
}
