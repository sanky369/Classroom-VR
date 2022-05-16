using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Voice.Unity;

public class TeacherUIManager : MonoBehaviourPunCallbacks
{
    public void ToggleVoice()
    {
        PhotonView[] photonViews = FindObjectsOfType<PhotonView>();
        for (int i = 0; i < photonViews.Length; i++)
        {
            if (photonViews[i].gameObject.tag == "Player")
            {
                photonViews[i].RPC("ToggleRecorder", RpcTarget.AllBuffered);
            }
        }
    }
}
