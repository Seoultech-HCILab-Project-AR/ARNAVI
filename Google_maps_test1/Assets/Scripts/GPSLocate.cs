using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class GPSLocate : MonoBehaviour
{
    public double latitude = 0;
    public double longitude = 0;

    [HideInInspector] public float delay = 3;
    [HideInInspector] public float maxTime = 5;

    public bool receiveGps = false;

    private double detailed_num = 1.0;
    
    void Start()
    {
        StartCoroutine(Gps_man());
        Input.compass.enabled = true;
    }

    IEnumerator Gps_man()
    {
        Debug.Log(Permission.HasUserAuthorizedPermission(Permission.FineLocation));
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            
            Permission.RequestUserPermission(Permission.FineLocation);
            while (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                yield return null;
            }
        }
        
        if (Input.location.isEnabledByUser) yield break;
        Input.location.Start(0.1f,0.1f);
        if (Input.location.status == LocationServiceStatus.Failed || Input.location.status == LocationServiceStatus.Stopped)
            Debug.Log(Input.location.status + " 위치 정보 가져오는데 실패");
        while (Input.location.status == LocationServiceStatus.Initializing && delay < maxTime)
        {
            Debug.Log("1");
            yield return new WaitForSeconds(1);
            delay++;
        }
        
        receiveGps = true;
        while (receiveGps)
        {
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;

            yield return new WaitForSeconds(0.2f);
        }
    }
}
