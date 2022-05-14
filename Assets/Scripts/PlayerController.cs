using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviour
{
    private PhotonView view;
    [SerializeField]private float TravelDistance;
    [SerializeField]private float AttackDistance;
    void Start()
    {
        view = GetComponent<PhotonView>();
    }
    
    void FixedUpdate()
    {
        
    }
}
