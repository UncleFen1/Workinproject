using System;
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class PreprocessBuildHandler : IPreprocessBuildWithReport, IPostprocessBuildWithReport
{
    const string TEMP_FILE_PATH = "Assets/Resources/app_build_time.txt";

    public int callbackOrder { get { return 0; } }

    public void OnPreprocessBuild(BuildReport report)
    {
        // can't set it in Player Settings, tbh see no effect, but according to docs should be on
        PlayerSettings.WebGL.useEmbeddedResources = true;

        var dateString = DateTime.UtcNow.ToString("yyyy.MM.dd HH:mm:ss");
        File.WriteAllText(TEMP_FILE_PATH, $"build (UTC): {dateString}");
        AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
        
        Debug.Log($"buildTime text asset '{TEMP_FILE_PATH}' has been created");
    }

    public void OnPostprocessBuild(BuildReport report)
    {
        if (File.Exists(TEMP_FILE_PATH))
        {
            if (AssetDatabase.DeleteAsset(TEMP_FILE_PATH))
            {
                Debug.Log($"DeleteAsset '{TEMP_FILE_PATH}' has been deleted");
            }
            else
            {
                Debug.Log($"DeleteAsset '{TEMP_FILE_PATH}' has failed");
            }
        }
    }
}
