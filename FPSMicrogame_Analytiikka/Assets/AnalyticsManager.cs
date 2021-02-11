using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsManager : MonoBehaviour
{
    private static AnalyticsManager instance;

    public static AnalyticsManager Instance { get => instance; }

    Dictionary<string, object> playerDeathDictionary = new Dictionary<string, object>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void SendEventLevelComplete(string levelName)
    {
        AnalyticsEvent.LevelComplete(levelName);
        Debug.Log("Sending Level Complete -event, with parameter: " + levelName);
    }

    public void SendEventLevelFail(string levelName)
    {
        AnalyticsEvent.LevelFail(levelName);
        Debug.Log("Sending Level Fail -event, with parameter: " + levelName);
    }

    public void SendEventPlayerDeath(string deathSource)
    {
        playerDeathDictionary.Add("Death Source", deathSource);
        Analytics.CustomEvent("player_death", playerDeathDictionary);
        Debug.Log("Sending Player Death -event, with parameter: " + deathSource);

        playerDeathDictionary.Clear();
    }

    public void SendEventEnemyDeath(string enemyName)
    {
        Dictionary<string, object> enemyDeath = new Dictionary<string, object>();
        enemyDeath.Add("Enemy Type", enemyName);

        Analytics.CustomEvent("enemies_killed", enemyDeath);
        Debug.Log("Sending Enemy Death -event, with parameter: " + enemyName);
    }

    private void OnApplicationQuit()
    {
        // Lähetetään game-quit eventti kun peli sammutetaan
        Analytics.CustomEvent("game_quit");
    }
}
