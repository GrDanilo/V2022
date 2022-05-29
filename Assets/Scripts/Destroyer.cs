using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Destroyer : MonoBehaviour
{
    [SerializeField] private float DestroyTime;

    void FixedUpdate()
    {
        if(DestroyTime <= 0)
        {
            PhotonNetwork.Destroy(gameObject);
        }
        else
        {
            DestroyTime -= Time.deltaTime;
        }
    }
}
