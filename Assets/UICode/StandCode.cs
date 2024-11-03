using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StandCode : MonoBehaviour
{
    public Button HitButton;
    public Button StandButton;
    
    public void OnButtonClick()
    {
        HitButton.interactable = false;
        StandButton.interactable = false;
        DealerCode.DealerTurn = "Active";
        DealerCode.ButtonsDisabled = "Yes";
    }
}