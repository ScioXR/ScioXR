using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;

class CIBuildScript : EditorWindow
{

    private static string _locationPathName;
    private static string _appName;
    private static string[] _scenes;// = FindEnabledEditorScenes();
    private static string _buildPath = "Builds/";

    private void OnEnable()
    {
        _scenes = FindEnabledEditorScenes();
    }

    [MenuItem("Tools/ScioXR Build Tools", false, 1)]
    static void OpenBuildWindow()
    {
        EditorWindow.GetWindow(typeof(CIBuildScript), false, "Build version");
    }

    void OnGUI()
    {
        GUILayout.Space(10);

        EditorGUILayout.HelpBox("Choose which platform you want to build:", MessageType.Info);

        if (GUILayout.Button("Build PC"))
        {
            PerformBuild();
        }
        if (GUILayout.Button("Build Android"))
        {
            PerformBuildAndroid();
        }
        if (GUILayout.Button("Build WebXR"))
        {
            PerformBuildWebXR();
        }
    }

    static void PerformBuild()
    {
        Debug.Log("Build started, enabled scenes:");

        _appName = PlayerSettings.productName + ".exe";
        _locationPathName = _buildPath + PlayerSettings.productName + "/" + _appName;

        _scenes = FindEnabledEditorScenes();
        foreach (var scene in _scenes)
        {
            Debug.Log(scene);
        }

        BuildSolution(_scenes, _locationPathName, BuildOptions.None, BuildTarget.StandaloneWindows64, BuildTargetGroup.Standalone);

        Debug.Log("Build finished");
    }

    static void PerformBuildAndroid()
    {
        Debug.Log("Build started, enabled scenes:");

        _appName = PlayerSettings.productName + ".apk";
        _locationPathName = _buildPath + _appName;

        _scenes = FindEnabledEditorScenes();
        foreach (var scene in _scenes)
        {
            Debug.Log(scene);
        }

        BuildSolution(_scenes, _locationPathName, BuildOptions.None, BuildTarget.Android, BuildTargetGroup.Android);

        Debug.Log("Build finished");
    }

    static void PerformBuildWebXR()
    {
        Debug.Log("Build started, enabled scenes:");

        _appName = PlayerSettings.productName;
        _locationPathName = _buildPath + _appName;

        _scenes = FindEnabledEditorScenes();
        foreach (var scene in _scenes)
        {
            Debug.Log(scene);
        }

        BuildSolution(_scenes, _locationPathName, BuildOptions.None, BuildTarget.WebGL, BuildTargetGroup.WebGL);

        Debug.Log("Build finished");
    }

    private static string[] FindEnabledEditorScenes()
    {

        List<string> EditorScenes = new List<string>();

        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {

            if (!scene.enabled) continue;

            EditorScenes.Add(scene.path);
        }
        return EditorScenes.ToArray();
    }

    private static void EnsureBuildPath()
    {
        if (!System.IO.Directory.Exists(_locationPathName))
        {
            System.IO.Directory.CreateDirectory(_locationPathName);
        }
    }

    static void BuildSolution(string[] scenes, string locationPathName, BuildOptions buildOptions, BuildTarget buildTarget, BuildTargetGroup buildTargetGroup)
    {
        //EnsureBuildPath();
        EditorUserBuildSettings.SwitchActiveBuildTarget(buildTargetGroup, buildTarget);

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = scenes;
        buildPlayerOptions.locationPathName = locationPathName;
        buildPlayerOptions.target = buildTarget;
        buildPlayerOptions.options = buildOptions;

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);// scenes, locationPathName, BuildTarget.StandaloneWindows, buildOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }
    }
}
