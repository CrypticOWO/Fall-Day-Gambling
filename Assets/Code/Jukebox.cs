using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Jukebox : MonoBehaviour
{
    public TMP_Text JukeboxPrompt;
    public GameObject SongStuff;
    public GameObject ExitJukebox;

    void Start()
    {
        ExitJukebox.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (CameraCode.LookingAt == "Jukebox" && CameraCode.LockView == "No")
        {
            JukeboxPrompt.gameObject.SetActive(true);
        }
        if (CameraCode.LookingAt != "Jukebox" || CameraCode.InCutscene == "Yes")
        {
            JukeboxPrompt.gameObject.SetActive(false);
            SongStuff.gameObject.SetActive(false);
        }
        if (CameraCode.LookingAt == "Jukebox" && CameraCode.LockView == "Yes")
        {
            JukeboxPrompt.gameObject.SetActive(false);
            SongStuff.gameObject.SetActive(true);   
            ExitJukebox.gameObject.SetActive(true);
        }
        else
        {
            ExitJukebox.gameObject.SetActive(false);
        }
    }
}
