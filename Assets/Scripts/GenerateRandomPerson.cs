using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Structures;

public class RandomPersonPlacer : MonoBehaviour
{
    public ARPlaneManager arPlaneManager;
    public GameObject personPrefab;
    public List<LocationProbability> locations; // 사용자 입력 위치 및 확률 정보.
    public int totalPersons = 10; // 전체 생성할 'Person' 수.
    private bool hasSpawned = false; // Person이 생성됐는지 여부를 추적하는 플래그.
    private bool infoLoaded = false; // getLocationInfo 호출 여부를 추적하는 플래그

    void Start()
    {
        if (arPlaneManager == null)
        {
            arPlaneManager = FindObjectOfType<ARPlaneManager>();
        }
        locations = new List<LocationProbability>();
        locations.Clear();
        getLocationInfo();
    }

    void Update()
    {
        // 터치 좌표가 추가된 후, GlobalData의 touchPositions에 값이 있는지 확인
        if (GlobalData.touchPositions.Count > 0 && !infoLoaded)
        {
            getLocationInfo(); // 터치 좌표가 추가된 후 정보 가져오기
            infoLoaded = true; // 정보가 한 번만 로드되도록 설정
        }

        // 평면이 감지되었고 Person 생성 조건이 만족되면 Person을 배치
        if (arPlaneManager.trackables.count > 0 && personPrefab != null && GlobalData.showPerson && !hasSpawned)
        {
            if (locations.Count > 0)
            {
                SpawnPersons();
            }
            else
            {
                Debug.LogWarning("locations 리스트가 비어있습니다. 터치 좌표가 입력되지 않았습니다.");
            }
        }
        else if (personPrefab == null)
        {
            Debug.LogError("personPrefab이 할당되지 않았습니다. personPrefab을 확인하세요.");
        }
    }
    //void SpawnPersons()
    //{
    //    // 이미 생성된 Person의 수를 계산.
    //    int alreadySpawned = CountSpawnedPersons();
    //    // 전체 생성할 'Person' 수에서 이미 생성된 수를 뺀 만큼 반복.
    //    while (alreadySpawned < totalPersons)
    //    {
    //        //foreach (var locProb in GlobalData.locations)
    //        foreach (var locProb in locations)
    //        {
    //            // 각 위치의 확률을 체크하여 'Person' 생성 여부 결정.
    //            if (Random.value <= locProb.probability)
    //            {
    //                // 무작위 분포 반경으로 X와 Z 좌표 모두 생성.
    //                Vector3 randomPosition = locProb.location + new Vector3(
    //                    Random.Range(-2.0f, 2.0f), // X축의 랜덤 값
    //                    0,                         // Y축은 평면에 고정
    //                    Random.Range(-2.0f, 2.0f)  // Z축의 랜덤 값
    //                );
    //                Vector3 planePosition = GetRandomPointInPlane(randomPosition);
    //                if (planePosition != Vector3.zero)
    //                {
    //                    Debug.Log($"Spawned at: {planePosition}"); // Debug Log                            
    //                    Instantiate(personPrefab, planePosition, Quaternion.identity);
    //                    alreadySpawned++;
    //                    if (alreadySpawned >= totalPersons)
    //                    {
    //                        GlobalData.showPerson = false;
    //                        Debug.Log(alreadySpawned);
    //                        hasSpawned = true; // Person이 생성됐음을 표시.
    //                        break; // 생성할 'Person'의 총 수를 초과하지 않도록 함.
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}

    // gpt ver. not working
    void SpawnPersons()
    {
        // 이미 생성된 Person의 수를 계산.
        int alreadySpawned = CountSpawnedPersons();

        // 전체 생성할 'Person' 수에서 이미 생성된 수를 뺀 만큼 반복.
        foreach (var locProb in locations)
        {
            int countToSpawn = Mathf.Min(locProb.count, totalPersons - alreadySpawned);
            if (countToSpawn <= 0) break;

            //for (int i = 0; i < countToSpawn; i++) // countToSpqwn이 아니라 locProb.count로 해야할듯 /////////////////////////////////////////////
            for (int i = 0; i < locProb.count; i++) // countToSpqwn이 아니라 locProb.count로 해야할듯 /////////////////////////////////////////////

            {
                // 확률에 따라 위치 조정
                //float distanceModifier = Mathf.Lerp(1f, 0f, locProb.probability); // 확률에 따라 위치를 멀리 또는 가깝게 생성
                float distanceModifier = (1f - locProb.probability) / 2; // 이걸로 진행하면 될듯 /////////////////////////////////////////////////

                Vector3 offset = new Vector3(
                    Random.Range(-distanceModifier, distanceModifier),
                    0,
                    Random.Range(-distanceModifier, distanceModifier)
                );
                Vector3 tempPosition = new Vector3(locProb.location.x, locProb.location.y, locProb.location.z);
                //Vector3 spawnPosition = locProb.location + offset;
                Vector3 spawnPosition = tempPosition + offset;
                Vector3 planePosition = GetRandomPointInPlane(spawnPosition);

                if (planePosition != Vector3.zero)
                {
                    Debug.Log($"Spawned at: {planePosition}");
                    Instantiate(personPrefab, planePosition, Quaternion.identity);
                    alreadySpawned++;

                    if (alreadySpawned >= totalPersons)
                    {
                        GlobalData.showPerson = false;
                        hasSpawned = true;
                        break;
                    }
                }
            }
        }
    }


    //void SpawnPersons() // for debug, z좌표 문제였던 거 같, 반전하니까 얼추 맞으나 정확하지는 않
    //{
    //    // 평면이 감지되지 않은 경우 함수 종료
    //    ARPlane firstPlane = null;
    //    foreach (var plane in arPlaneManager.trackables)
    //    {
    //        firstPlane = plane;
    //        break;
    //    }

    //    if (firstPlane == null)
    //    {
    //        Debug.LogWarning("No detected planes.");
    //        return;
    //    }

    //    // 이미 생성된 Person의 수를 계산.
    //    int alreadySpawned = CountSpawnedPersons();

    //    // locations 리스트에 있는 각 위치에서 정확히 하나의 Person을 생성합니다.
    //    foreach (var locProb in locations)
    //    {
    //        if (alreadySpawned >= totalPersons) break;

    //        // location 위치에 정확히 생성
    //        //Vector3 spawnPosition = locProb.location;
    //        Vector3 spawnPosition = new Vector3(locProb.location.x, 0, -locProb.location.z);

    //        // 생성 위치의 높이를 첫 번째 평면의 높이로 맞춤
    //        spawnPosition.y = firstPlane.transform.position.y;

    //        Debug.Log($"Spawned at exact location: {spawnPosition}");
    //        Instantiate(personPrefab, spawnPosition, Quaternion.identity);
    //        alreadySpawned++;
    //    }

    //    hasSpawned = true; // 생성 완료 표시
    //    GlobalData.showPerson = false; // 생성 완료 후 showPerson 비활성화
    //}



    Vector3 GetRandomPointInPlane(Vector3? specificLocation = null)
    {
        // 인식된 평면 중 하나를 무작위로 선택합니다.
        List<ARPlane> planes = new List<ARPlane>();
        foreach (var plane in arPlaneManager.trackables)
        {
            planes.Add(plane);
        }
        if (planes.Count == 0)
        {
            return Vector3.zero;
        }

        ARPlane selectedPlane = planes[Random.Range(0, planes.Count)]; // random이 아니라 plane 하나를 지정해서 해야할듯, 함수 인자로 넘겨주기?
        // 상관없을 듯 하다, 사람들 count를 generate 할 때마다 새로 갱신하기 때문에 여기서 생성이 안되어 기록이 안되어도 상관없음. 대신 여기서 return값이
        // 무조건 좌표라, 무조건 생성되는데...  이 문제는 생각해 봐야 할듯. flag를 만들어서 하는게 가장 직관적이긴 함.

        //Vector3 randomPosition = specificLocation.Value;
        Vector3 randomPosition = new Vector3(specificLocation.Value.x, selectedPlane.transform.position.y, -specificLocation.Value.z); // 이렇게 진행 /////////////////////
        // 특정 위치가 주어지면 그 위치를 기반으로 평면 내 무작위 위치를 반환합니다.
        if (specificLocation.HasValue)
        {
            Debug.Log($"HasValue in: {randomPosition}"); // Debug Lo
            //Vector3 randomPosition = specificLocation.Value;
            if (selectedPlane.boundary.Contains(new Vector2(randomPosition.x, randomPosition.z))) // z값 -해야 실행되나? 위쪽에서 z값 반전시켰기 때문에?
            {
                // 평면 위의 y 좌표를 사용하여 위치를 조정합니다.
                Debug.Log($"returned at: {randomPosition}"); // Debug Log
                return randomPosition;
            }
        }
        //봄
        // random 이니까 count 해야 하긴 해서 position 값 줘서 만드는게 맞기는 한듯.
        return randomPosition;
        //return new Vector3(0, 10, 0);

        // 내 생각에 밑부분은 필요하지 않은듯 ///////////////////////////
        // 선택된 평면의 경계 내에서 무작위 위치를 생성합니다.
        //Vector3 center = selectedPlane.center;
        //Vector3 extents = selectedPlane.extents;

        //Debug.Log($"\n\n\n\n\nextents\n\n\n\n\n\n: {extents}"); // Debug Log                            

        //float randomX = Random.Range(center.x - extents.x / 2, center.x + extents.x / 2);
        //float randomZ = Random.Range(center.y - extents.y / 2, center.y + extents.y / 2);

        ////////////////////////////////////////////////////////
        ////
        // 평면 위의 y 좌표를 사용하여 위치를 조정합니다.
        //return new Vector3(randomX, selectedPlane.transform.position.y, randomZ);
        //return new Vector3(randomPosition.x, selectedPlane.transform.position.y, randomPosition.y);
        //return new Vector3(randomX, randomy, randomZ);
    }

    int CountSpawnedPersons()
    {
        // 현재까지 생성된 'Person'의 수를 계산.
        return GameObject.FindGameObjectsWithTag("Person").Length; // 'Person' 프리팹에 "Person" 태그가 지정되어 있어야 함.
    }

    void getLocationInfo()
    {
        Debug.Log("getLocationInfo started");

        if (GlobalData.touchPositions.Count == 0)
        {
            Debug.LogWarning("GlobalData.touchPositions 리스트가 비어있습니다. 터치된 좌표가 없습니다.");
            return;
        }

        //foreach (Vector3 touchPosition in GlobalData.touchPositions)
        //{
        //    Debug.Log("Touched position: " + touchPosition);

        //    locations.Add(new LocationProbability
        //    {
        //        location = touchPosition,
        //        probability = Random.Range(0f, 0.00f), // 무작위로 설정
        //        count = Random.Range(1, 1) // 무작위로 설정 (추후 제거 가능)
        //    });
        //}

        foreach (LocationProbability loc in GlobalData.locations)
        {
            Debug.Log("Touched position: " + loc.location);

            locations.Add(new LocationProbability
            {
                location = loc.location,
                probability = loc.probability,
                count = Random.Range(1, 1) // 무작위로 설정 (추후 제거 가능)
            });
        }
        
    }
}
