using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AddressablesTest : MonoBehaviour
{
    public AssetReference test;
 
    // Start is called before the first frame update
    void Start()
    {
        Addressables.InstantiateAsync(test, this.transform);
    }
}
