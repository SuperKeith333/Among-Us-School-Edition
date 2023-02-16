using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class choose_role : MonoBehaviour
{
    private List<string> imposters = new List<string>();
    private List<string> crewmates = new List<string>();

    private void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            int playersCount = PhotonNetwork.PlayerList.Length;
            int randomIndex;
            int impostersCount = (int)Mathf.Ceil(playersCount * 0.33f);

            // Choose imposters based on the number of players
            for (int i = 0; i < impostersCount; i++)
            {
                randomIndex = Random.Range(0, playersCount);
                PhotonNetwork.PlayerList[randomIndex].SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "Role", "Imposter" } });
                imposters.Add(PhotonNetwork.PlayerList[randomIndex].NickName);
                playersCount--;
            }

            // Set the remaining players as crewmates
            for (int i = 0; i < playersCount; i++)
            {
                if (!imposters.Contains(PhotonNetwork.PlayerList[i].NickName))
                {
                    PhotonNetwork.PlayerList[i].SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "Role", "Crewmate" } });
                    crewmates.Add(PhotonNetwork.PlayerList[i].NickName);
                }
            }

            // Print the players and their roles
            Debug.Log("Imposters: " + string.Join(", ", imposters));
            Debug.Log("Crewmates: " + string.Join(", ", crewmates));
        }
    }
}

