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
    public static bool showPerson = false;

    // private 리스트, 직접 접근을 막고 property를 통해 접근
    private static List<Vector3> _touchPositions = new List<Vector3>();

    // Property로 touchPositions 리스트에 접근
    public static List<Vector3> touchPositions
    {
        get { return _touchPositions; }
        set
        {
            _touchPositions = value;
            Debug.Log("touchPositions 리스트가 업데이트되었습니다.");
        }
    }

    // touchPositions에 새 좌표 추가 시 로그 출력
    public static void AddTouchPosition(Vector3 newPosition)
    {
        _touchPositions.Add(newPosition);
        Debug.Log("added new touchPosition: " + newPosition);
        Debug.Log("touchPositions size" + _touchPositions.Count);
    }

    //만들다 말음
    public static void AddLocations(LocationProbability newPosition)
    {
        locations.Add(newPosition);
        Debug.Log("added new locations: " + newPosition.location);
        Debug.Log("locations size" + locations.Count);
    }


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
