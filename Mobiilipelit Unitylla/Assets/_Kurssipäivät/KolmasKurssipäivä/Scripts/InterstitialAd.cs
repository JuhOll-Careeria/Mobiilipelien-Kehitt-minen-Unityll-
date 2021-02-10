using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

namespace CourseDayThree
{
    public class InterstitialAd : MonoBehaviour
    {
        public InterstitialType interstitialType = InterstitialType.display;

        public void ShowInterstitialAd()
        {
            Debug.Log("Trying to show Interstitial Ad: " + interstitialType.ToString());

            if (Advertisement.IsReady(interstitialType.ToString()))
            {
                Advertisement.Show(interstitialType.ToString());
                Debug.Log("Showing Interstitial Ad: " + interstitialType);
            }
        }
    }

    public enum InterstitialType
    {
        video,
        display,
        playable
    }
}
