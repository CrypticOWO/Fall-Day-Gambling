using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveJukebox : MonoBehaviour
{
    public static string AtJukebox = "No";

    void Start()
    {
        AtJukebox = "Yes";
    }
    
    public void OnButtonClick()
    {
        AtJukebox = "No";
    }
}
