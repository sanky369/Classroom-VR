using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Voice.Unity;

public class RecorderManager : MonoBehaviourPunCallbacks
{
    private Recorder recorder;
    void Awake()
    {
        recorder = GetComponent<Recorder>();
    }

    [PunRPC]
    public void ToggleRecorder()
    {
        if (recorder.isActiveAndEnabled)
        {
            recorder.enabled = false;
        } else
        {
            recorder.enabled = true;
        }
    }
}
