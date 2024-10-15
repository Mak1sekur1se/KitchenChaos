using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        playButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.GameScene);//Test Index 是Build中的Scene
        });

        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });

        //暂停跳转回来后时间继续
        Time.timeScale = 1f;

    }
}
