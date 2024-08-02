using System;
using UnityEngine;
using UnityEditor.Build.Reporting;
using UnityEditor;
using System.Linq;
using System.IO;

public class BuildScript : MonoBehaviour
{
    [MenuItem("Build/BuildForWeb")]
    public static void BuildForWeb()
    {
        PreBuild();
        BuildReport report = CreateBuild(BuildTarget.WebGL);
        PostBuild(report);
    }

    // not needed for now
    // [MenuItem("Build/BuildForAndroid")]
    public static void BuildForAndroid()
    {
        PreBuild();
        BuildReport report = CreateBuild(BuildTarget.Android);
        PostBuild(report);
    }

    [MenuItem("Build/BuildForWindows64")]
    public static void BuildForWindows64()
    {
        PreBuild();
        BuildReport report = CreateBuild(BuildTarget.StandaloneWindows64);
        PostBuild(report);
    }

    [MenuItem("Build/BuildForWindows64", true)]
    static bool ValidateBuildForWindows64()
    {
        // return System.Runtime.InteropServices.RuntimeInformation.OSArchitecture.ToString().Equals("X64");
        return System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows);
    }

    [MenuItem("Build/BuildForStandaloneOSX")]
    public static void BuildForStandaloneOSX()
    {
        PreBuild();
        BuildReport report = CreateBuild(BuildTarget.StandaloneOSX);
        PostBuild(report);
    }

    [MenuItem("Build/BuildForStandaloneOSX", true)]
    static bool ValidateBuildForStandaloneOSX()
    {
        // return System.Runtime.InteropServices.RuntimeInformation.OSArchitecture.ToString().Equals("Arm64");
        return System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.OSX);
    }

    private static void PreBuild()
    {
        AssetDatabase.SaveAssets();
    }

    private static void PostBuild(BuildReport report)
    {
        BuildSummary summary = report.summary;
        ConsoleWriteReport(summary);
    }

    private static string[] GetScenes()
    {
        var scenes = EditorBuildSettings.scenes
            .Select(s => s.path)
            .ToArray();
        return scenes;
    }

    private static BuildReport CreateBuild(BuildTarget buildTarget)
    {
        // var buildTargetGroup = UnityEditor.BuildPipeline.GetBuildTargetGroup(buildTarget);
        // var namedBuildTarget = UnityEditor.Build.NamedBuildTarget.FromBuildTargetGroup(buildTargetGroup);
        // PlayerSettings.SetIl2CppCodeGeneration(namedBuildTarget, Il2CppCodeGeneration.OptimizeSpeed);

        string[] scenes = GetScenes();

        var releasePath = $"Builds/{buildTarget}";
        switch (buildTarget) {
            case BuildTarget.Android:
                releasePath += $"{Path.DirectorySeparatorChar}{PlayerSettings.productName}.apk";
                break;
            case BuildTarget.StandaloneOSX:
                releasePath += $"{Path.DirectorySeparatorChar}{PlayerSettings.productName}.app";
                break;
            case BuildTarget.StandaloneWindows64:
                releasePath += $"{Path.DirectorySeparatorChar}{PlayerSettings.productName}.exe";
                break;
        }
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = releasePath,
            target = buildTarget,
            options = BuildOptions.None,
        };

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        return report;
    }

    private static void ConsoleWriteReport(BuildSummary summary)
    {
        switch (summary.result)
        {
            case BuildResult.Succeeded:
                Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
                break;
            case BuildResult.Failed:
                Debug.Log("Build failed");
                break;
            case BuildResult.Unknown:
                break;
            case BuildResult.Cancelled:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
