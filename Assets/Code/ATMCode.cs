using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ATMCode : MonoBehaviour
{
    public TMP_Text ATMPrompt;

    // Update is called once per frame
    void Update()
    {
        if (CameraCode.LookingAt == "ATM" && CameraCode.InCutscene == "No")
        {
            ATMPrompt.gameObject.SetActive(true);

            if(Input.GetKeyDown(KeyCode.E) && Player.Balance < 10000)
            {
                Player.Balance = 10000;
                Debug.Log("It's ok, you still have enough");
            }
            else if(Input.GetKeyDown(KeyCode.P))
            {
                Player.Balance = 1000000;
                Debug.Log("Oh you're DEFINITELY overdrafting lmafo");
            }
        }
        else
        {
            ATMPrompt.gameObject.SetActive(false);
        }
    }
}
