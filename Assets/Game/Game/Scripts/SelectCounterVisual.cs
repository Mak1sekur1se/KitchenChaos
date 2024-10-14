using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjectArray; 
    private void Start()
    {
        Player.Instance.onSelectCounterChange += Player_onSelectCounterChange;
    }

    private void Player_onSelectCounterChange(object sender, Player.OnSelectCounterChange e)
    {
        if (e.selectCounter == baseCounter)
            Show();
        else
            Hide();
    }

    private void Show() {
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(true);
        }
    }

    private void Hide() {
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(false);
        }
    }
}
