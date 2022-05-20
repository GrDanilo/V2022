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
    [SerializeField]private float TravelDistance;
    [SerializeField]private float AttackDistance;
    private Vector3 MyPos;
    private bool IcanGo;
    [SerializeField]private GameObject InfoMenu;
    void Start()
    {
        view = GetComponent<PhotonView>();
        //получение камеры канвасом
        camera = GameObject.FindGameObjectWithTag("Camera").GetComponent<Camera>() as Camera;
        canvas.worldCamera = camera;
    }
    
    void FixedUpdate()
    {
        if(view.IsMine)
        {
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
