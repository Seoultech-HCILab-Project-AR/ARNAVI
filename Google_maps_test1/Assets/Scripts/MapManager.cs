using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.Timeline;

public class MapManager : MonoBehaviour
{
    public RawImage mapRawImage;

    [Header("�� ���� ����")]
    public string strBaseURL = "";

    public int zoom = 14;
    public int mapWidth;
    public int mapHeight;
    public string strAPIKey = "";

    [FormerlySerializedAs("GPSlocation")] public GPSLocate gpsLocation;
    private double latitude = 0;
    private double longitude = 0;

    private double save_latitude = 0;
    private double save_longitude = 0;

    // Start is called before the first frame update
    void Start()
    {
        mapRawImage = GetComponent<RawImage>();
        gpsLocation = GetComponent<GPSLocate>();
        StartCoroutine(WaitForSecond());
    }


    // Update is called once per frame
    void Update()
    {
        latitude = gpsLocation.latitude;
        longitude = gpsLocation.longitude;
        //Debug.Log("latitude  : " + latitude);
        //Debug.Log("longitude  : " + longitude);
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
                Debug.Log("start Coroutine LoadMap");
                StartCoroutine(LoadMap());
            }
            //print("3��");
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
        Debug.Log(req);
        yield return req.SendWebRequest(); //req�� ��ȯ!
        Debug.Log("yield return over send web request");
        mapRawImage.texture = DownloadHandlerTexture.GetContent(req); // �� >> �̹����� ����
        Debug.Log(mapRawImage.texture);
    }
}
