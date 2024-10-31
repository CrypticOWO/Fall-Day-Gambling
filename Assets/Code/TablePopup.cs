using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TablePopup : MonoBehaviour
{
    public GameObject Popup;

    // Start is called before the first frame update
    void Start()
    {
        Popup.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (CameraCode.LookingAt == "Table" && CameraCode.LockView == "No")
        {
            Popup.gameObject.SetActive(true); 
        }
        else
        {
            Popup.gameObject.SetActive(false);
        }
    }
}