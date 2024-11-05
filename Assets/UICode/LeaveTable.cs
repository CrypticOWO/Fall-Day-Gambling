using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveTable : MonoBehaviour
{
    public static string AtTable = "Yes";

    void Start()
    {
        AtTable = "Yes";
    }
    
    public void OnButtonClick()
    {
        AtTable = "No";
    }
}
