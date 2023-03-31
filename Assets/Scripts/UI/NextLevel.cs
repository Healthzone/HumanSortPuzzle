using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class NextLevel : MonoBehaviour
{
    public void GoToNextLevel()
    {
        YandexGame.FullscreenShow();
        SceneManager.LoadScene("Game");
    }
}
