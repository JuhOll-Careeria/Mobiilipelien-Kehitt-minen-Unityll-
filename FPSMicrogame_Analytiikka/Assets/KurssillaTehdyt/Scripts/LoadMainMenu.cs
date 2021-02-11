using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenu : MonoBehaviour
{
    public LevelEnums mainMenuScene = LevelEnums.IntroMenu;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(mainMenuScene.ToString());
    }
}
