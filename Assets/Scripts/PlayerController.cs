using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviour
{
    private PhotonView view;
    [SerializeField]private Canvas canvas;
    private Camera camera;
    private float TravelDistance;
    [SerializeField]private float MaxTravelDistance;
    private Vector3 MyPos;
    private bool IcanGo;
    [SerializeField]private GameObject InfoMenu;
    [SerializeField]private bool Colour;
    [SerializeField]private GameObject GoButton;
    void Start()
    {
        view = GetComponent<PhotonView>();
        //получение камеры канвасом
        camera = GameObject.FindGameObjectWithTag("Camera").GetComponent<Camera>() as Camera;
        canvas.worldCamera = camera;
        TravelDistance = MaxTravelDistance;
        if(!view.IsMine)
        {
            GoButton.SetActive(false);
        }
    }
    
    void FixedUpdate()
    {
        if(view.IsMine)
        {
            if(TravelDistance < MaxTravelDistance)
            {
                TravelDistance += 0.002f;
            }
            if(TravelDistance > 0 && IcanGo == true)
            {
                if(Input.GetKeyDown(KeyCode.W))
                {
                    MyPos = transform.position;
                    MyPos.y += 1f;
                    transform.position = MyPos;
                    TravelDistance -= 1;
                }
                else if(Input.GetKeyDown(KeyCode.S))
                {
                    MyPos = transform.position;
                    MyPos.y -= 1f;
                    transform.position = MyPos;
                    TravelDistance -= 1;
                }
                else if(Input.GetKeyDown(KeyCode.A))
                {
                    MyPos = transform.position;
                    MyPos.x -= 1f;
                    transform.position = MyPos;
                    TravelDistance -= 1;
                }
                else if(Input.GetKeyDown(KeyCode.D))
                {
                    MyPos = transform.position;
                    MyPos.x += 1f;
                    transform.position = MyPos;
                    TravelDistance -= 1;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(view.IsMine)
        {
            if(other.CompareTag("PlayerBlue") && Colour == true)
            {
                Destroy(gameObject);
            }

            if(other.CompareTag("PlayerRed") && Colour == false)
            {
                Destroy(gameObject);
            }
        }
    }

    public void PlayerButton()
    {
        if(view.IsMine)
        {
            if(IcanGo == false)
            {
                IcanGo = true;
                InfoMenu.SetActive(true);
            }
            else
            {
                IcanGo = false;
                InfoMenu.SetActive(false);
            }
        }
    }
}
