using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Android;
using System;
using static UnityEngine.EventSystems.EventTrigger;
using Unity.VisualScripting;
using TMPro;
using UnityEngine.XR.ARFoundation;

public class GPSLocation : MonoBehaviour
{
    // Start is called before the first frame update

    [HideInInspector]
    public double latitude = 0;
    [HideInInspector]
    public double longitude = 0;

    [HideInInspector]
    public float delay;
    [HideInInspector]
    public float maxtime = 5.0f;

    [HideInInspector]
    public bool receiveGPS = false;

    double detailed_num = 1.0;
    void Start()
    {
        StartCoroutine(Gps_man());
        Input.compass.enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Gps_man()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation)) // 권한 요청하기  // GPS 요청 
        {
            Permission.RequestUserPermission(Permission.FineLocation);
            while (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                yield return null;
            }
        }

        Input.location.Start(0.1f, 0.1f); ;
        while (Input.location.status == LocationServiceStatus.Initializing && delay < maxtime)
        {
            yield return new WaitForSeconds(1);
            delay++;
        }

        receiveGPS = true;

        while (receiveGPS)
        {
            latitude = (double)Input.location.lastData.latitude;
            longitude = (double)Input.location.lastData.longitude;

            yield return new WaitForSeconds(0.2f);
        }
    }
}
