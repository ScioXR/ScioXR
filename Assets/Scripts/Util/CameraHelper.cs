using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHelper
{
    private static Camera _camera;

    public static Camera main
    {
        get
        {
            if (_camera == null)
            {
                if (Camera.main)
                {
                    _camera = Camera.main;
                }
                if (_camera == null)
                {
                    Camera camera = FindCamera();
                    if (camera)
                    {
                        _camera = camera;
                    }
                }
            }
            return _camera;
        }
    }

    public static void Reset()
    {
        _camera = null;
    }

    public static void SetCamera(Camera newCamera)
    {
        _camera = newCamera;
    }

    protected static Camera FindCamera()
    {
        Camera[] cameras = GameObject.FindObjectsOfType<Camera>();
        Camera result = null;
        int camerasSum = 0;
        foreach (var camera in cameras)
        {
            if (camera.enabled)
            {
                result = camera;
                camerasSum++;
            }
        }
        if (camerasSum > 1)
        {
            result = null;
        }
        return result;
    }


}
