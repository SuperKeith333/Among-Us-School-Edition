using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class billboard : MonoBehaviour
{
    Camera cam;
    public TMP_Text usernametext;
    public PhotonView playerPV;

    void Start()
    {
        usernametext.text = playerPV.Owner.NickName;
    }


    void Update()
    {
        if(cam == null)
        {
            cam = FindObjectOfType<Camera>();
        }

        if(cam == null)
            return;
        transform.LookAt(cam.transform);
        transform.Rotate(Vector3.up * 180);
    }
}
