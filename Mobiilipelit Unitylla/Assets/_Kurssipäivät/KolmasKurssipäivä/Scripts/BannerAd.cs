using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

namespace CourseDayThree
{
    public class BannerAd : MonoBehaviour
    {
        [Header("Banner")]
        public string bannerID = "testBanner";
        public float bannerTime = 10f;
        public BannerPosition bannerPosition = BannerPosition.TOP_CENTER;

        bool bannerShowing = false;


        public void InitializeBannerShow()
        {
            StartCoroutine(ShowBannerWhenReady());
        }

        IEnumerator ShowBannerWhenReady()
        {
            while (!Advertisement.IsReady(bannerID))
            {
                yield return new WaitForSeconds(0.5f);
            }

            Advertisement.Banner.SetPosition(bannerPosition);
            Advertisement.Banner.Show(bannerID);
            bannerShowing = true;

            StartCoroutine(RemoveBannerAd());
        }

        IEnumerator RemoveBannerAd()
        {
            yield return new WaitForSeconds(bannerTime);

            if (bannerShowing)
            {
                HideBanner();
            }
        }

        public void HideBanner()
        {
            Advertisement.Banner.Hide(false);
            bannerShowing = false;
        }
    }
}