///////////////////////
///////////////////////
/// how to use?
/// use Structure named 'LocationProbability'
///
///     'LocationProbability' structure should include 3 factors : location / probability / count
///
///         'location' is touched location, which came from user's input
/// 
///         'probability' is percentage of density in location, alos came from user's input, Range is 0 to 1
///
///         [IGNORE THIS FACTOR] 'count' is total count of person in location, maybe it doesn't need, delete after.
///
///     If have any question, contact Jiseong Yu.
///
///////////////////////
///////////////////////

///////////////////////
///////////////////////
///
/// For Jiwon
///
/// add coordinate of touched point at 'List<Vector3> touchPisitions'
/// Vector3 coordinate should be x, y, z
/// examples of coordinate : (0.3, 0.0, 0.2) / (1.1, 0.0, -0.7)
/// it will be great projection coordinate by axis Y
/// or it doesnt matter, just need plane coordinate
///
///////////////////////
///////////////////////

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
        //locations.Add(new LocationProbability
        //{
        //    location = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f)),
        //    probability = Random.Range(0f, 1f),
        //    count = Random.Range(1, 10) // 필요 시 수정
        //});

        //locations.Add(new LocationProbability
        //{
        //    location = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f)),
        //    probability = Random.Range(0f, 1f),
        //    count = Random.Range(1, 10) // 필요 시 수정
        //});

        //foreach (Vector3 touchPosition in touchPositions)
        //{
        //    locations.Add(new LocationProbability
        //    {
        //        location = touchPosition,
        //        probability = Random.Range(0f, 1f), // 무작위로 설정
        //        count = Random.Range(1, 10) // 무작위로 설정 (추후 제거 가능)
        //    });
        //}
    }
}
