// One-shot batch setup for the Cat Pomodoro project (run once at creation by the Builder lane setup).
// Sets: 2D default behaviour mode, portrait 1080x1920, product "Cat Pomodoro", company "Captain",
// package com.captain.catpomodoro, creates Assets/CatPomodoro/Scenes/Main.unity with a PERSPECTIVE camera
// (a 3D room with 2D cat sprites, like the Cats R rooms), adds Main to Build Settings, and switches the
// active build target to Android only when the Android module is installed in this editor.
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.SceneManagement;
using UnityEngine;
using System.IO;

namespace CatPomodoro.EditorTools
{
    public static class CatPomodoroSetup
    {
        public static void Apply()
        {
            EditorSettings.defaultBehaviorMode = EditorBehaviorMode.Mode2D;
            PlayerSettings.productName = "Cat Pomodoro";
            PlayerSettings.companyName = "Captain";
            PlayerSettings.defaultScreenWidth = 1080;
            PlayerSettings.defaultScreenHeight = 1920;
            PlayerSettings.defaultInterfaceOrientation = UIOrientation.Portrait;
            PlayerSettings.allowedAutorotateToPortrait = true;
            PlayerSettings.allowedAutorotateToPortraitUpsideDown = false;
            PlayerSettings.allowedAutorotateToLandscapeLeft = false;
            PlayerSettings.allowedAutorotateToLandscapeRight = false;
            PlayerSettings.runInBackground = true;
            PlayerSettings.SetApplicationIdentifier(NamedBuildTarget.Android, "com.captain.catpomodoro");
            PlayerSettings.SetApplicationIdentifier(NamedBuildTarget.iOS, "com.captain.catpomodoro");
            PlayerSettings.SetScriptingBackend(NamedBuildTarget.Android, ScriptingImplementation.IL2CPP);
            PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;
            PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel24;

            Directory.CreateDirectory("Assets/CatPomodoro/Scenes");
            Directory.CreateDirectory("Assets/CatPomodoro/Data");
            Directory.CreateDirectory("Assets/CatPomodoro/Scripts");
            Directory.CreateDirectory("Assets/CatPomodoro/Art");
            AssetDatabase.Refresh();

            const string scenePath = "Assets/CatPomodoro/Scenes/Main.unity";
            if (!File.Exists(scenePath))
            {
                var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
                var cam = Camera.main;
                if (cam != null)
                {
                    // Cats R room setup: perspective camera looking down into a 3D room, cats are 2D sprites in it.
                    cam.orthographic = false;
                    cam.fieldOfView = 40f;
                    cam.nearClipPlane = 0.3f;
                    cam.farClipPlane = 200f;
                    cam.transform.position = new Vector3(0f, 9f, -12f);
                    cam.transform.rotation = Quaternion.Euler(35f, 0f, 0f);
                    cam.backgroundColor = new Color(0.55f, 0.72f, 0.58f); // forest green
                    cam.clearFlags = CameraClearFlags.SolidColor;
                }
                EditorSceneManager.SaveScene(scene, scenePath);
            }
            EditorBuildSettings.scenes = new[] { new EditorBuildSettingsScene(scenePath, true) };

            // Keep the Unity-made SampleScene out of the way if it exists.
            if (File.Exists("Assets/Scenes/SampleScene.unity"))
            {
                AssetDatabase.DeleteAsset("Assets/Scenes/SampleScene.unity");
                AssetDatabase.DeleteAsset("Assets/Scenes");
            }
            AssetDatabase.SaveAssets();

            bool androidModule = BuildPipeline.IsBuildTargetSupported(BuildTargetGroup.Android, BuildTarget.Android);
            if (androidModule)
            {
                if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
                {
                    EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
                }
                Debug.Log("CatPomodoroSetup.Apply: Android module present, active build target = Android");
            }
            else
            {
                Debug.Log("CatPomodoroSetup.Apply: ANDROID MODULE ABSENT in this editor; build target left as "
                          + EditorUserBuildSettings.activeBuildTarget);
            }
            Debug.Log("CatPomodoroSetup.Apply: done (2D mode, perspective Main camera, portrait 1080x1920, Cat Pomodoro / Captain, com.captain.catpomodoro, Main.unity)");
        }
    }
}
