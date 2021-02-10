using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;


namespace CourseDayThree
{
    public class AdManager : MonoBehaviour
    {
        [Header("Game ID's")]
        public string gameIdAndroid = "4002309";
        public string gameIdIOs = "";
        public bool testMode = true;

        // Start is called before the first frame update
        void Start()
        {
#if UNITY_IOS
            Advertisement.Initialize(gameIdIOs, testMode);
#elif UNITY_ANDROID
            Advertisement.Initialize(gameIdAndroid, testMode);
#endif
        }
    }
}
