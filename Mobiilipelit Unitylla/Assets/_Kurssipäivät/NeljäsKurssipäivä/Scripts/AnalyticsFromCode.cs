using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsFromCode : MonoBehaviour
{
    public GameObject doorGO;

    //// Start is called before the first frame update
    //void Start()
    //{
    //// Toteuttaa saman asian kuin AnalyticsEventTracker -komponentit "AnalyticsEventTracker_Testi" GameObjektissa
    //    AnalyticsEvent.GameStart();
    //    AnalyticsEvent.LevelStart("StandardEventsTesti");
    //}

    public void SendCustomEvent()
    {
        Debug.Log("Lähetetään custom eventti");
        doorGO.SetActive(true);

        // key = Edellinen osio, value = uusi osio
        Dictionary<string, object> customEventDictionary = new Dictionary<string, object>();
        customEventDictionary.Add("osio 1", "osio 2");

        Analytics.CustomEvent("osio_suoritettu", customEventDictionary);

        Destroy(this.gameObject);
    }

    void OnDestroy()
    {
        // Toteuttaa saman asian kuin AnalyticsEventTracker -komponentit tässä GameObjektissa (inspektorissa)

        AnalyticsEvent.LevelComplete("Osio 1");
        AnalyticsEvent.LevelStart("Osio 2");
    }
}
