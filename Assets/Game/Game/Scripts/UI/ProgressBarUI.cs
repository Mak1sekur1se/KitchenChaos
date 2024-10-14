using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] private GameObject hasProgressGameObject;

    private IHasProgress hasProgress;

    private void Start()
    {
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();

        if (hasProgress == null)
        {
            Debug.LogError($"GameObejct {hasProgressGameObject} has no a component implements IHasProgress!");
        }

        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;
        barImage.fillAmount = 0;

        Hide();
    }

    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;
        //Debug.Log(e.progressNormalized);
        //进度条有可能归零后再次出现 目前未找到原因 
        if (e.progressNormalized == 0f || e.progressNormalized == 1f)
        {
            //Debug.Log("Hide");
            Hide();
            return;
        }
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false); 
    }
}
