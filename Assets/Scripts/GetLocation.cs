using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Structures;

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
    public static GetLocation instance; // 싱글톤 패턴을 사용하여 인스턴스를 전역적으로 접근 가능하도록 설정

    public ARPlaneManager planeManager;
    public List<PlaneData> planeDataList;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 현재 게임 오브젝트를 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 존재하면 현재 게임 오브젝트를 파괴
        }
    }

    void Start()
    {
        planeManager = GetComponent<ARPlaneManager>();
        planeManager.planesChanged += OnPlanesChanged;
        planeDataList = new List<PlaneData>();
    }

    void OnPlanesChanged(ARPlanesChangedEventArgs eventArgs)
    {
        if (eventArgs == null)
            return;

        foreach (var plane in eventArgs.added)
        {
            if (plane == null)
                continue;
            // Plane 데이터 수집
            PlaneData planeData = new PlaneData
            {
                Position = plane.transform.position,
                Rotation = plane.transform.rotation,
                Size = plane.size
            };
            Debug.Log($"Plane position:{planeData.Position}, Plane size: {planeData.Size}");

            planeDataList.Add(planeData);
            // Plane 데이터 직렬화
            //string serializedPlaneData = JsonUtility.ToJson(planeData);

            // 직렬화된 데이터 저장
            //SavePlaneData(serializedPlaneData);
        }
    }

    /*
    void SavePlaneData(string serializedPlaneData)
    {
        // 파일 저장, PlayerPrefs 사용, 네트워크 전송 등의 방법으로 데이터 저장
        PlayerPrefs.SetString("PlaneData", serializedPlaneData);
    }
    */

}
