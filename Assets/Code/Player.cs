using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform TargetCamera;

    void Update()
    {
        if(CameraCode.LockView == "Yes")
        {

        }
        else
        {
            transform.position = TargetCamera.transform.position + new Vector3(0,-3.5f, 0);
        }
    }
}
