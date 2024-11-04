using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerBalance : MonoBehaviour
{
    public TMP_Text Balance;
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Balance.text = "Wallet: " + Player.Balance;
        if(CameraCode.LockView == "Yes" && CameraCode.LookingAt == "Table")
        {
            rectTransform.anchoredPosition = new Vector2(410,430);
        }
        else
        {
            rectTransform.anchoredPosition = new Vector2(440,500);
        }
    }
}