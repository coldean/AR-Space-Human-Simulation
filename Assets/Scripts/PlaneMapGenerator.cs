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
    private void Update()
    {
        if (planeTexture == null)
        {
            return;
        }

        // 터치 입력 처리 (모바일)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) // 터치 시작
            {
                Vector2 touchPos = touch.position;

                // 터치한 좌표가 이미지 영역 내에 있는지 확인
                if (RectTransformUtility.RectangleContainsScreenPoint(planeMapImage.rectTransform, touchPos))
                {
                    // 화면 좌표를 이미지의 로컬 좌표로 변환
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(planeMapImage.rectTransform, touchPos, null, out Vector2 localPoint);

                    // 로컬 좌표를 텍스처 좌표로 변환
                    int texX = (int)((localPoint.x + (planeMapImage.rectTransform.rect.width / 2)) * (planeTexture.width / planeMapImage.rectTransform.rect.width));
                    int texY = (int)((-localPoint.y + (planeMapImage.rectTransform.rect.height / 2)) * (planeTexture.height / planeMapImage.rectTransform.rect.height));

                    // 텍스처 좌표가 유효한지 확인
                    if (texX >= 0 && texX < planeTexture.width && texY >= 0 && texY < planeTexture.height)
                    {
                        // 터치한 위치의 좌표를 planeTexture에서 가져옴
                        Color pixelColor = planeTexture.GetPixel((int)texX, (int)texY);

                        //  if (pixelColor == Color.grey)
                        {
                            // 텍스처 좌표를 월드 좌표로 변환
                            Vector3 worldPos = new Vector3((texX - mapWidth / 2) / mapScale, 0, (texY - mapHeight / 2) / mapScale);

                            Debug.Log("planeMapGenerator position : " + worldPos);
                            // 좌표를 GlobalData의 touchPositions 리스트에 추가
                            GlobalData.AddTouchPosition(worldPos);
                            Debug.Log(worldPos);
                        }
                    }
                }
            }
        }
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

