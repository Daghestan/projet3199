using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class CreateAndJoinRoom : MonoBehaviourPunCallbacks
{
    public TMP_InputField create;
    public TMP_InputField join;
    // Start is called before the first frame update
  
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(create.text);
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(join.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("BaseSceneMultiPlayer");
    }
}
