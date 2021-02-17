using UnityEngine;
using System.IO;
using UnityEditor;

public class CreateAssetBundles
{
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        string assetBundleDir = "Assets/StreamingAssets";


        if (!Directory.Exists(Application.streamingAssetsPath))
        {
            Debug.Log("Luodaan uusi StreamingAssets -kansio kohteeseen: " + assetBundleDir);
            Directory.CreateDirectory(assetBundleDir);
        }

        BuildPipeline.BuildAssetBundles(assetBundleDir, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
    }
}
