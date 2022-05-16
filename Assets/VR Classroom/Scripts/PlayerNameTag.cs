using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerNameTag : MonoBehaviourPun
{
    public TextMesh studentName;


    void Awake()
    {
        //photonView.Owner.NickName = PlayerPrefs.GetString("name");
    }
    // Start is called before the first frame update
    private void Start()
    {
        if(photonView.IsMine) { return;}
        //photonView.Owner.NickName = PlayerPrefs.GetString("name");
        //SetName();
        photonView.RPC("SetName", RpcTarget.AllBuffered);
    }


    [PunRPC]
    private void SetName()
    {
        studentName.text = photonView.Owner.NickName;
        //Debug.Log(PlayerPrefs.GetString("name"));
    }
}
