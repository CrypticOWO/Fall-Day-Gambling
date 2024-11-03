using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerBalance : MonoBehaviour
{
    public TMP_Text Balance;

    // Update is called once per frame
    void Update()
    {
        Balance.text = "Wallet: " + Player.Balance;
        if(CameraCode.LockView == "Yes")
        {
            transform.position = new Vector3(820,410,0);
        }
        else
        {
            transform.position = new Vector3(820,440,0);
        }
    }
}
