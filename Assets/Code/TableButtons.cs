using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableButtons : MonoBehaviour
{
    public GameObject HitButton;
    public GameObject StandButton;

    public GameObject PlayerValue;
    public GameObject DealerValue;

    public GameObject InputAndConfirmBet;
    public GameObject BetAndPayout;

    // Start is called before the first frame update
    void Start()
    {
        HitButton.gameObject.SetActive(false);
        StandButton.gameObject.SetActive(false);
        PlayerValue.gameObject.SetActive(false);
        DealerValue.gameObject.SetActive(false);
        InputAndConfirmBet.gameObject.SetActive(false);
        BetAndPayout.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (CameraCode.LookingAt == "Table" && CameraCode.LockView == "Yes")
        {
            BetAndPayout.gameObject.SetActive(true);

            if (ConfirmBet.BetConfirmed == "Yes")
            {
                HitButton.gameObject.SetActive(true);
                StandButton.gameObject.SetActive(true);
                PlayerValue.gameObject.SetActive(true);
                DealerValue.gameObject.SetActive(true);
                
                InputAndConfirmBet.gameObject.SetActive(false);
            }
            else
            {
                InputAndConfirmBet.gameObject.SetActive(true);
            }
        }
        else
        {
            HitButton.gameObject.SetActive(false);
            StandButton.gameObject.SetActive(false);
            PlayerValue.gameObject.SetActive(false);
            DealerValue.gameObject.SetActive(false);
            InputAndConfirmBet.gameObject.SetActive(false);
            BetAndPayout.gameObject.SetActive(false);
        }
    }
}
