using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;
using UnityEngine.XR.Management;

public class PlatformLoader : MonoBehaviour
{
    public enum ScioXRPlatforms
    {
        Flat3D,
        UnityXR,
        WebXR
    };

    public ScioXRPlatforms currentPlatfrom;

    public GameObject[] platforms;

    public static PlatformLoader instance;

    public Platform platform;

    public GameObject controllerLeft;
    public GameObject controllerRight;

    void Awake()
    {     
        if (!instance)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;

            Init();          
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    // Start is called before the first frame update
    public void Init()
    {       
        platform = platforms[(int)currentPlatfrom].GetComponent<Platform>();
        platforms[(int)currentPlatfrom].SetActive(true);
        platform.Init();
      

#if UNITY_WEBGL
        //platform = gameObject.AddComponent<Scio3DPlatform>();
        //setup3D.SetActive(true);
#else
        List<XRInputSubsystem> lst = new List<XRInputSubsystem>();
        SubsystemManager.GetInstances<XRInputSubsystem>(lst);
        for (int i = 0; i < lst.Count; i++)
        {
            lst[i].TrySetTrackingOriginMode(TrackingOriginModeFlags.Floor);
        }
        /*if (force3D)
        {
            platform = gameObject.AddComponent<Scio3DPlatform>();
            setup3D.SetActive(true);
        }
        else
        {
            XRGeneralSettings.Instance.Manager.InitializeLoaderSync();
            if (XRGeneralSettings.Instance.Manager.activeLoader == null)
            {
                Debug.LogError("Initializing XR Failed. Check Editor or Player log for details.");
                setup3D.SetActive(true);
                platform = gameObject.AddComponent<Scio3DPlatform>();
                //EventSystem.current.GetComponent<XRUIInputModule>().enabled = false;
            }
            else
            {
                Debug.Log("Starting XR...");
                setupVR.SetActive(true);
                platform = gameObject.AddComponent<UnityXRPlatform>();
                XRGeneralSettings.Instance.Manager.StartSubsystems();
            }
        }*/
#endif
    }

    public void HideHands(bool enable)
    {
        SkinnedMeshRenderer[] hands = GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (var hand in hands)
        {
            hand.enabled = !enable;
           // Debug.Log("HideHands111111 " + hand);
        }
        controllerLeft.SetActive(enable);
        controllerRight.SetActive(enable);
       // Debug.Log("HideHands " + enable);
    }
}
