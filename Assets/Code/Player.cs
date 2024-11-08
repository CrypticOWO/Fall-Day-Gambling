using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform TargetCamera;

    public static float Balance = 0;

    void Start()
    {
        Balance = 10000;
    }

    void Update()
    {
        if(CameraCode.LockView == "No" && CameraCode.InCutscene == "No")
        {
            transform.position = TargetCamera.transform.position + new Vector3(0,-3f, 0);
        }
    }
}
