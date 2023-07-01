using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Timeline;

public class MapManager : MonoBehaviour
{
    public RawImage mapRawImage;

    [Header("맵 정보 설정")]
    public string strBaseURL = "";

    public int zoom = 14;
    public int mapWidth;
    public int mapHeight;
    public string strAPIKey = "";

    public GPSLocation GPSlocation;
    private double latitude = 0;
    private double longitude = 0;

    private double save_latitude = 0;
    private double save_longitude = 0;

    // Start is called before the first frame update
    void Start()
    {
        mapRawImage = GetComponent<RawImage>();
        StartCoroutine(WaitForSecond());
    }


    // Update is called once per frame
    void Update()
    {
        latitude = GPSlocation.latitude;
        longitude = GPSlocation.longitude;
        //print("location" + latitude + " " + longitude);
    }


    IEnumerator WaitForSecond()
    {
        while (true)
        {

            if (save_latitude != latitude || save_longitude != longitude)
            {
                save_latitude = latitude;
                save_longitude = longitude;
                StartCoroutine(LoadMap());
            }
            print("3초");
            yield return new WaitForSeconds(3f);
        }
        yield return new WaitForSeconds(1f);
    }


    IEnumerator LoadMap()
    {
        string url = strBaseURL + "center=" + latitude + "," + longitude +
            "&zoom=" + zoom.ToString() +
            "&size=" + mapWidth.ToString() + "x" + mapHeight.ToString()
            + "&key=" + strAPIKey;

        Debug.Log("URL : " + url);

        url = UnityWebRequest.UnEscapeURL(url);
        UnityWebRequest req = UnityWebRequestTexture.GetTexture(url);

        yield return req.SendWebRequest(); //req값 반환!

        mapRawImage.texture = DownloadHandlerTexture.GetContent(req); // 맵 >> 이미지에 적용
    }
}
