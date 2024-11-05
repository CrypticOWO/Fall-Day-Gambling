using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveJukebox : MonoBehaviour
{
    public static string AtJukebox = "Yes";

    void Start()
    {
        AtJukebox = "Yes";
    }
    
    public void OnButtonClick()
    {
        AtJukebox = "No";
    }
}
