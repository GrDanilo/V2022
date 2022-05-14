using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CameraMover : MonoBehaviour
{
    private PhotonView view;
    private Rigidbody2D rb;
    [SerializeField]private float speed;
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    [SerializeField]private CameraMover cameraMover;
    [SerializeField]private GameObject Camera;

    void Start() 
    {
        view = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody2D>();

        if(!view.IsMine)
        {
            cameraMover.enabled = false;;
            Camera.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }
}
