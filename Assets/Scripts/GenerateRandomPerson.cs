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

    void Start()
    {
        if (arPlaneManager == null)
        {
            arPlaneManager = FindObjectOfType<ARPlaneManager>();
        }
    }

    void Update()
    {
        // Update를 통해 평면을 감지하고 무작위로 'Person' 오브젝트를 배치하는 기능을 실행합니다.
        if (arPlaneManager.trackables.count > 0 && personPrefab != null && GlobalData.showPerson && !hasSpawned)
        {
            Debug.Log("\n\n\n\n generate on \n\n\n\n\n");
            Debug.Log(GlobalData.locations[0].probability);
            SpawnPersons();          // Debug Log
        }
        else if (personPrefab == null)
        {
            Debug.LogError("personPrefab이 할당되지 않았습니다. personPrefab을 확인하세요.");
        }
    }

    void SpawnPersons()
    {
        // 이미 생성된 Person의 수를 계산.
        int alreadySpawned = CountSpawnedPersons();
        // 전체 생성할 'Person' 수에서 이미 생성된 수를 뺀 만큼 반복.
        while (alreadySpawned < totalPersons)
        {
            foreach (var locProb in GlobalData.locations)
            {
                // 각 위치의 확률을 체크하여 'Person' 생성 여부 결정.
                if (Random.value <= locProb.probability)
                {
                    // 무작위 분포 반경으로 X와 Z 좌표 모두 생성.
                    Vector3 randomPosition = locProb.location + new Vector3(
                        Random.Range(-2.0f, 2.0f), // X축의 랜덤 값
                        0,                         // Y축은 평면에 고정
                        Random.Range(-2.0f, 2.0f)  // Z축의 랜덤 값
                    );
                    Vector3 planePosition = GetRandomPointInPlane(randomPosition);
                    if (planePosition != Vector3.zero)
                    {
                        Debug.Log($"Spawned at: {planePosition}"); // Debug Log                            
                        Instantiate(personPrefab, planePosition, Quaternion.identity);
                        alreadySpawned++;
                        if (alreadySpawned >= totalPersons)
                        {
                            GlobalData.showPerson = false;
                            Debug.Log(alreadySpawned);
                            hasSpawned = true; // Person이 생성됐음을 표시.
                            break; // 생성할 'Person'의 총 수를 초과하지 않도록 함.
                        }
                    }
                }
            }
        }
    }

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

        ARPlane selectedPlane = planes[Random.Range(0, planes.Count)];

        // 특정 위치가 주어지면 그 위치를 기반으로 평면 내 무작위 위치를 반환합니다.
        if (specificLocation.HasValue)
        {
            Vector3 randomPosition = specificLocation.Value;
            if (selectedPlane.boundary.Contains(new Vector2(randomPosition.x, randomPosition.z)))
            {
                // 평면 위의 y 좌표를 사용하여 위치를 조정합니다.
                Debug.Log($"returned at: {randomPosition}"); // Debug Log                            
                return new Vector3(randomPosition.x, selectedPlane.transform.position.y, randomPosition.z);
            }
        }

        // 선택된 평면의 경계 내에서 무작위 위치를 생성합니다.
        Vector3 center = selectedPlane.center;
        Vector3 extents = selectedPlane.extents;

        Debug.Log($"\n\n\n\n\nextents\n\n\n\n\n\n: {extents}"); // Debug Log                            

        float randomX = Random.Range(center.x - extents.x / 2, center.x + extents.x / 2);
        float randomZ = Random.Range(center.y - extents.y / 2, center.y + extents.y / 2);

        // 평면 위의 y 좌표를 사용하여 위치를 조정합니다.
        return new Vector3(randomX, selectedPlane.transform.position.y, randomZ);
        //return new Vector3(randomX, randomy, randomZ);
    }

    int CountSpawnedPersons()
    {
        // 현재까지 생성된 'Person'의 수를 계산.
        return GameObject.FindGameObjectsWithTag("Person").Length; // 'Person' 프리팹에 "Person" 태그가 지정되어 있어야 함.
    }
}