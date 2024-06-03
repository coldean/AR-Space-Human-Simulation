using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using Structures;
public class PlaneMapGenerator : MonoBehaviour
{
    public RawImage planeMapImage; // UI에서 결과를 표시할 RawImage
    public int mapWidth = Screen.width; // 텍스처 너비
    public int mapHeight = Screen.height; // 텍스처 높이
    public float mapScale = 200.0f; // 월드 좌표계에서 텍스처 좌표계로의 스케일

    public Texture2D planeTexture;

    private void Awake()
    {
        planeMapImage.gameObject.SetActive(false);
    }
    public void OnclickMap()
    {
        planeTexture = new Texture2D(mapWidth, mapHeight);
        planeMapImage.texture = planeTexture;

        // 2D 맵을 갱신
        UpdatePlaneMap();
    }

    public void UpdatePlaneMap()
    {
        // 텍스처 초기화
        
        Color32[] resetColorArray = new Color32[planeTexture.width * planeTexture.height];

        for (int i = 0; i < resetColorArray.Length; i++)
        {
            resetColorArray[i] = Color.white; // 초기화 시 하양색으로 채우기
        }
        planeTexture.SetPixels32(resetColorArray);
        
       // GenerateRandomPlaneData(3);
        List<PlaneData> planeDataList = GlobalData.planeDataList;

        // 각 플레인을 맵에 그리기
        foreach (var planeData in planeDataList)
        {
            // 바닥면 중심을 텍스처 좌표계로 변환
            int texX = (int)(planeData.Position.x * mapScale) + (mapWidth / 2);
            int texY = (int)(planeData.Position.z * mapScale) + (mapHeight / 2);
            // 바닥면의 크기
            int planeWidth = (int)(planeData.Size.x * mapScale);
            int planeHeight = (int)(planeData.Size.y * mapScale);
            for (int y = texY - planeHeight / 2; y <= texY + planeHeight / 2; y++)
            {
                for (int x = texX - planeWidth / 2; x <= texX + planeWidth / 2; x++)
                {
                    if (x >= 0 && x < mapWidth && y >= 0 && y < mapHeight)
                    {
                        if (y == texY - planeHeight / 2 || y == texY + planeHeight / 2 || x == texX - planeWidth / 2 || x == texX + planeWidth / 2)
                        {
                            planeTexture.SetPixel(x, y, Color.red); // 테두리는 빨간색으로 채우기
                        }
                        else
                        {
                            planeTexture.SetPixel(x, y, Color.grey); // 내부는 회색으로 채우기
                        }
                    }
                }
            }
        }

        // 텍스처 적용
        planeTexture.Apply();
        planeMapImage.gameObject.SetActive(true);
    }

    // plane 정보 랜덤으로 넣기
   /*
    public void GenerateRandomPlaneData(int count)
    {
        for (int i = 0; i < count; i++)
        {
            PlaneData planeD = new PlaneData
            {
                Position = new Vector3(
                    Random.Range(-10, 10), // X 좌표
                    0,
                    Random.Range(-10, 10)  // Z 좌표
                ),
                Size = new Vector2(
                    Random.Range(0.5f, 10.0f),  // Width
                    Random.Range(0.5f, 10.0f)   // Height
                )
            };
            GlobalData.planeDataList.Add(planeD);
        }
    }
    */

}

