using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;
    public Vector3[] spawns;
    public int spawnPos;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (PV.IsMine)
        {
            CreateController();
        }
    }

    void CreateController()
    {
        spawnPos = Random.Range(0, 5);   

        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), spawns[spawnPos], Quaternion.identity);
    }
}
