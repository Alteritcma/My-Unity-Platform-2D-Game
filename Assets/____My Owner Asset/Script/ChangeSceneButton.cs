using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneButton : MonoBehaviour
{
    public void ChangeSceneButtons(string sceneName)
    {
        LevelManager.instance.LoadScene(sceneName);
    }
    public void ChangeQuitButton()
    {
        LevelManager.instance.QuitFromGame();
    }
    public void ChangeRestartButton()
    {
        LevelManager.instance.RestartGame();
    }
}
