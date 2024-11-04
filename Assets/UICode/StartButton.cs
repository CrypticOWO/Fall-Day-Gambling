using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public GameObject StartPanel;
    
    public void OnButtonClick()
    {
        CameraCode.InCutscene = "Yes";
        CameraCode.LockView = "Yes";
        CameraCode.TypeOfMenu = "Casino";
        StartPanel.gameObject.SetActive(false);
    }
}
