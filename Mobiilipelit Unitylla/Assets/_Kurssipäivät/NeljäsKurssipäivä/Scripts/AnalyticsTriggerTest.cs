using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticsTriggerTest : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AnalyticsFromCode>())
        {
            other.GetComponent<AnalyticsFromCode>().SendCustomEvent();
        }
    }
}
