using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;



public class mainmenu : MonoBehaviourPunCallbacks
{

   
    public GameObject RoomPrefab;
    public Transform playerListContent;
    public GameObject PlayerListItemPrefab;
    public TMP_InputField nameTextfield;
    public TMP_Text RoomName;
    public GameObject lobbyMenu;
    public GameObject[] AllRooms;
    public TMP_Text errortext;
    public room room;
    public bool usernameset = false;
    public Transform Content;
    public GameObject NametextInput;
    public GameObject Nametext;
    public GameObject Namebutton;
    public GameObject HostServer;
    public GameObject joinserver;
    public GameObject StartGameButton;
    
    
   

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waiter());
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster() {
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void AddName()
    {
        
        
        PhotonNetwork.NickName = nameTextfield.text;
        usernameset = true;
        
        

        
    }

    public void hostServer()
    {
        if (usernameset == true)
        {
            PhotonNetwork.CreateRoom($"{PhotonNetwork.NickName}'s game");
            print($"{PhotonNetwork.NickName}'s game created!");
            lobbyMenu.SetActive(true);
            NametextInput.SetActive(false);
            Nametext.SetActive(false);
            Namebutton.SetActive(false);
            joinserver.SetActive(false);
            HostServer.SetActive(false);
            
            RoomName.text = $"{PhotonNetwork.NickName}'s game";
           
        } else {
            errortext.text = "Please set username before hosting/joining";
        }
        
        
        
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        for (int i = 0; i < AllRooms.Length; i++)
        {
            if(AllRooms[i] != null)
            {
                Destroy(AllRooms[i]);
            }
        }

        AllRooms = new GameObject[roomList.Count];


        for(int i = 0; i < roomList.Count; i++)
        {
            if(roomList[i].IsOpen && roomList[i].IsVisible && roomList[i].PlayerCount >= 1)
            {
            GameObject Room = Instantiate(RoomPrefab , Vector3.zero , Quaternion.identity , Content);
            Room.GetComponent<room>().Name.text = roomList[i].Name;

            AllRooms[i] = Room;
            }
        }


    }



    public void JoinRoomInList(string roomName)
    {
        if (usernameset == true)
        {
            PhotonNetwork.JoinRoom(roomName);
            print("hi");

            
        } else {
            errortext.text = "Please set username before hosting/joining";
        }
        
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(PlayerListItemPrefab, playerListContent).GetComponent<roomplayer>().SetUp(newPlayer);
    }

    public override void OnJoinedRoom()
    {
        lobbyMenu.SetActive(true);
        NametextInput.SetActive(false);
        Nametext.SetActive(false);
        Namebutton.SetActive(false);
        joinserver.SetActive(false);
        HostServer.SetActive(false);
        StartGameButton.SetActive(PhotonNetwork.IsMasterClient);

        Player[] players = PhotonNetwork.PlayerList;
        for (int i = 0; i < players.Length; i++)
        {
            Instantiate(PlayerListItemPrefab, playerListContent).GetComponent<roomplayer>().SetUp(players[i]);
        }

        
    }

    void Update()
    {
        
    }
    

   IEnumerator waiter()
{
    yield return new WaitForSeconds(3);
}

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }
   
    
     
}

