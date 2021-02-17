using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BundleObjectLoader : MonoBehaviour
{
    // Assetin nimi joka halutaa hakea
    public string testiAssetName = "";

    // Bundlen nimi, josta assetti haetaan
    public string testiBundleName = "testibundle";


    // Start is called before the first frame update
    void Start()
    {
        GameObject asset = LoadFromLocalAssetBundle(testiAssetName, testiBundleName);

        if (asset != null)
        {
            Instantiate(asset, this.transform);
        }
    }

    GameObject LoadFromLocalAssetBundle(string assetName, string bundleName)
    {
        AssetBundle localAB = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, bundleName));

        if (localAB == null)
        {
            Debug.LogError("AssetBundlea nimellä " + bundleName + " ei löytynyt!");
            return null;
        }

        GameObject asset = localAB.LoadAsset<GameObject>(assetName);

        localAB.Unload(false);

        return asset;
    }
}
