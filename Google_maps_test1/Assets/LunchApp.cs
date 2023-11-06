using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunchApp : MonoBehaviour
{
    public string packageName;// 실행할 앱의 패키지 이름
    public string targetAppName;// 실행할 앱 이름

    public void LaunchApp(string packageName)
    {
        try
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            AndroidJavaObject packageManager = currentActivity.Call<AndroidJavaObject>("getPackageManager");
            AndroidJavaObject intent = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", packageName);

            if (intent != null)
            {
                currentActivity.Call("startActivity", intent);
            }
            else
            {
                Debug.Log("App not found");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error launching app: " + e.Message);
            throw;
        }
    }
    public void GetPackageName()
    {
        try
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            AndroidJavaObject packageManager = currentActivity.Call<AndroidJavaObject>("getPackageManager");
            AndroidJavaObject appsList = packageManager.Call<AndroidJavaObject>("getInstalledApplications", 0);

            int size = appsList.Call<int>("size");
            for (int i = 0; i < size; i++)
            {
                AndroidJavaObject appInfo = appsList.Call<AndroidJavaObject>("get", i);
                string appName = packageManager.Call<string>("getApplicationLabel", appInfo);

                if (appName == targetAppName)
                {
                    string packageName = appInfo.Call<string>("packageName");
                    LaunchApp(packageName);
                    Debug.Log("Package Name: " + packageName);
                    break;
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error getting package name: " + e.Message);
        }
    }
    public void LaunchAdpp()
    {
        try
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            AndroidJavaObject packageManager = currentActivity.Call<AndroidJavaObject>("getPackageManager");
            AndroidJavaObject intent = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", packageName);

            if (intent != null)
            {
                currentActivity.Call("startActivity", intent);
            }
            else
            {
                Debug.Log("App not found");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error launching app: " + e.Message);
        }
    }
    
    /*public bool LaunchAndroidApp(string inBundleID)
    {
#if UNITY_ANDROID
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayer?.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject packageManager = currentActivity?.Call<AndroidJavaObject>("getPackageManager");

        AndroidJavaObject packageInfo = null;
        try
        {
            packageInfo = packageManager.Call<AndroidJavaObject>("getPackageInfo", inBundleID, 0);
            if (null == packageInfo) Debug.Log("packageInfo is null");
        }
        catch (System.Exception e)
        {
            Debug.Log($"Error! msg= {e.Message}");
        }

        if (null == packageInfo)
        {
            unityPlayer?.Dispose();
            currentActivity?.Dispose();
            packageManager?.Dispose();
            return false;
        }

        AndroidJavaObject launchIntent = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", inBundleID);
        if (null == launchIntent) Debug.Log("launchIntent is null");
        currentActivity.Call("startActivity", launchIntent);

        launchIntent?.Dispose();

        unityPlayer?.Dispose();
        currentActivity?.Dispose();
        packageManager?.Dispose();
        return true;
#else
	return false;
#endif
    }*/
}
