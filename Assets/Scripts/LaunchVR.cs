using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchVR : MonoBehaviour
{
    public void TryLaunch()
    {
        bool fail = false;
        string bundleId = "com.aCompany.EjerciciosVRPaciente"; // your target bundle id
        AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject ca = up.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject packageManager = ca.Call<AndroidJavaObject>("getPackageManager");

        AndroidJavaObject launchIntent = null;
        try
        {
            launchIntent = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", bundleId);
        }
        catch
        {
            fail = true;
        }

        if (!fail)
            ca.Call("startActivity", launchIntent);

        up.Dispose();
        ca.Dispose();
        packageManager.Dispose();
        launchIntent.Dispose();
    }
}
