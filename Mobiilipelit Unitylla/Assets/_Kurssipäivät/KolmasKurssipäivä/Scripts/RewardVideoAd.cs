using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;

namespace CourseDayThree
{
    public class RewardVideoAd : MonoBehaviour, IUnityAdsListener
    {
        public string rewardPlacementID = "rewardedVideo";

        public TextMeshProUGUI pelaajanRaha;
        public TextMeshProUGUI resultTMP;

        private TextMeshProUGUI readyTMP;

        private int raha = 100;
        bool isReady = false;

        void Start()
        {
            Advertisement.AddListener(this);
            resultTMP.text = "";
            pelaajanRaha.text = raha.ToString();
            readyTMP = GetComponentInChildren<TextMeshProUGUI>();
            readyTMP.text = "Reward Ad: Not Ready";
        }

        public void OnUnityAdsDidError(string message)
        {
            resultTMP.text = message;
        }

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            switch (showResult)
            {
                case ShowResult.Failed:
                    // Jokin muu error tapahtui, ei palkita
                    resultTMP.text = "Reward Mainokse näyttäminen epäonnistui";
                    break;
                case ShowResult.Skipped:
                    // Käyttäjä skippasi mainoksen, ei palkita
                    resultTMP.text = "Skippasit mainoksen, et saa palkintoa!";
                    break;
                case ShowResult.Finished:
                    // Käyttäjä katsoi mainoksen kokonaan, palkitaan pelaajaa
                    resultTMP.text = "Katsoit mainoksen kokonaan, tässä palkintosi!";
                    raha += 100;
                    pelaajanRaha.text = raha.ToString();
                    break;
            }
        }

        public void OnUnityAdsDidStart(string placementId)
        {
            resultTMP.text = "Started Reward Ad";
            Debug.Log("Started Reward Ad");
        }

        public void OnUnityAdsReady(string placementId)
        {
            if (placementId == rewardPlacementID)
            {
                isReady = true;
                readyTMP.text = "Reward AD: Ready";
            }
        }

        public void InitializeRewardAd()
        {
            resultTMP.text = "Trying to start Reward Ad";
            if (Advertisement.IsReady(rewardPlacementID) && isReady)
            {
                Advertisement.Show(rewardPlacementID);
                isReady = false;
                readyTMP.text = "Reward AD: Not Ready";
            }
        }
    }
}