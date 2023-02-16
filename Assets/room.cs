using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;


public class room : MonoBehaviourPunCallbacks
{

    public TMP_Text Name;
    public bool usernameset2;
    public string BefName;
    public bool errorset1;

    // Start is called before the first frame update
    public void JoinRoom()
    {
        JoinRoomInList(Name.text);
    }

    public void JoinRoomInList(string roomName)
    {
        
        PhotonNetwork.JoinRoom(roomName);
      
    }


}
