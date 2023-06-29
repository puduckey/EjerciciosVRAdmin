using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.XR.Management;

public class XRManager : MonoBehaviour
{
    public static XRManager instance;
    public bool startInit = true;

//     private void Start()
//     {
// #if !UNITY_EDITOR
//         if (startInit)
//             StartCoroutine(InitXR());
//         else
//             StopXR();
// #endif
//     }
// 
    // public IEnumerator InitXR()
    // {
    //     yield return XRGeneralSettings.Instance.Manager.InitializeLoader();
    // 
    //     if (XRGeneralSettings.Instance.Manager.activeLoader == null)
    //     {
    //         Debug.LogError("Initializing XR Failed. Check Editor or Player log for details.");
    //     }
    //     else
    //     {
    //         Debug.Log("XR initialized.");
    // 
    //         Debug.Log("Starting XR...");
    //         XRGeneralSettings.Instance.Manager.StartSubsystems();
    //         Debug.Log("XR started.");
    //     }
    // }
    // 
    // public void StopXR()
    // {
    //     Debug.Log("Stopping XR...");
    //     XRGeneralSettings.Instance.Manager.StopSubsystems();
    //     Debug.Log("XR stopped.");
    // 
    //     Debug.Log("Deinitializing XR...");
    //     XRGeneralSettings.Instance.Manager.DeinitializeLoader();
    //     Debug.Log("XR deinitialized.");
    // 
    //     Camera.main.ResetAspect();
    //     Camera.main.fieldOfView = 60.0f;
    // }
}
