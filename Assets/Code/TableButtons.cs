using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableButtons : MonoBehaviour
{
    public GameObject HitButton;
    public GameObject StandButton;

    // Start is called before the first frame update
    void Start()
    {
        HitButton.gameObject.SetActive(false);
        StandButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (CameraCode.LookingAt == "Table" && CameraCode.LockView == "Yes")
        {
            HitButton.gameObject.SetActive(true);
            StandButton.gameObject.SetActive(true);
        }
        else
        {
            HitButton.gameObject.SetActive(false);
            StandButton.gameObject.SetActive(false);
        }
    }
}
