using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using Structures;
public class PlaneMapGenerator : MonoBehaviour
{
    public RawImage planeMapImage; // UI���� ����� ǥ���� RawImage
    public ARPlaneManager arPlaneManager;
    public int mapWidth = Screen.width; // �ؽ�ó �ʺ�
    public int mapHeight = Screen.height; // �ؽ�ó ����
    public float mapScale = 200.0f; // ���� ��ǥ�迡�� �ؽ�ó ��ǥ����� ������

    public Texture2D planeTexture;

    private void Awake()
    {
        planeMapImage.gameObject.SetActive(false);
        if (arPlaneManager == null)
        {
            arPlaneManager = FindObjectOfType<ARPlaneManager>();
        }
    }
    public void OnclickMap()
    {
        planeTexture = new Texture2D(mapWidth, mapHeight);
        planeMapImage.texture = planeTexture;

        // 2D ���� ����
        UpdatePlaneMap();
    }
    
    public void OnclickBack()
    {
        //GlobalData.planeDataList.Clear(); // ��� ���� �ʱ�ȭ, �ٽ� ��ĵ�ϱ� ����
        GlobalData.showPerson = true;
        planeMapImage.gameObject.SetActive(false);
        arPlaneManager.enabled = false;

    }
    public void UpdatePlaneMap()
    {
        // �ؽ�ó �ʱ�ȭ
        
        Color32[] resetColorArray = new Color32[planeTexture.width * planeTexture.height];

        for (int i = 0; i < resetColorArray.Length; i++)
        {
            resetColorArray[i] = Color.white; // �ʱ�ȭ �� �Ͼ������ ä���
        }
        planeTexture.SetPixels32(resetColorArray);
        planeTexture.Apply();
        //GenerateRandomPlaneData(3);
        List<PlaneData> planeDataList = GlobalData.planeDataList;
        // �� �÷����� �ʿ� �׸���
        foreach (var planeData in planeDataList)
        {
            // �ٴڸ� �߽��� �ؽ�ó ��ǥ��� ��ȯ
            int texX = (int)(planeData.Position.x * mapScale) + (mapWidth / 2);
            int texY = (int)(planeData.Position.z * mapScale) + (mapHeight / 2);
            // �ٴڸ��� ũ��
            int planeWidth = (int)(planeData.Size.x * mapScale);
            int planeHeight = (int)(planeData.Size.y * mapScale);

            for (int y = texY - planeHeight / 2; y <= texY + planeHeight / 2; y++)
            {
                for (int x = texX - planeWidth / 2; x <= texX + planeWidth / 2; x++)
                {
                    if (x >= 0 && x < mapWidth && y >= 0 && y < mapHeight)
                    {
                        // planeTexture.SetPixel(x, y, Color.grey); // ����� ��� ȸ������ ä���
                        if (y == texY - planeHeight / 2 || y == texY + planeHeight / 2 || x == texX - planeWidth / 2 || x == texX + planeWidth / 2)
                        {
                            planeTexture.SetPixel(x, y, Color.red); // �׵θ��� ���������� ä���
                        }
                        else
                        {
                            planeTexture.SetPixel(x, y, Color.grey); // ���δ� ȸ������ ä���
                        }
                    }
                }
            }
        }
        // �ؽ�ó ����
        planeTexture.Apply();
        planeMapImage.gameObject.SetActive(true);
    }

    // plane ���� �������� �ֱ�
   /*
    public void GenerateRandomPlaneData(int count)
    {
        for (int i = 0; i < count; i++)
        {
            PlaneData planeD = new PlaneData
            {
                Position = new Vector3(
                    Random.Range(-10, 10), // X ��ǥ
                    0,
                    Random.Range(-10, 10)  // Z ��ǥ
                ),
                Size = new Vector2(
                    Random.Range(0.5f, 5.0f),  // Width
                    Random.Range(0.5f, 5.0f)   // Height
                )
            };
            GlobalData.planeDataList.Add(planeD);
        }
    }
   */
    

}

