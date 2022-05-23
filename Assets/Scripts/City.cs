using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class City : MonoBehaviour
{
    private PhotonView view;
    [SerializeField]private Canvas canvas;
    [SerializeField]private Camera camera;
    [Header("Switch")]
    [SerializeField]private bool Colour;
    [SerializeField]private GameObject[] SwitchObjects;
    [Header("Players")]
    [SerializeField]private Transform SpawnPosition;
    [SerializeField]private GameObject[] RedPlayerPrefab;
    [SerializeField]private GameObject[] BluePlayerPrefab;
    [Header("Coins")]
    private float CoinTime;
    [SerializeField]private float StartCoinTime;
    private float UnitCost;
    [SerializeField]private float Coin;

    void Start()
    {
        view = GetComponent<PhotonView>();
        //получение камеры канвасом
        camera = GameObject.FindGameObjectWithTag("Camera").GetComponent<Camera>() as Camera;
        canvas.worldCamera = camera;
    }

    void FixedUpdate()
    {
        if(CoinTime <= 0)
        {
            Coin += 1f;
            CoinTime = StartCoinTime;
        }
        else
        {
            CoinTime -= Time.deltaTime;
        }
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        //часть кода для захвата города
        if (other.CompareTag("PlayerBlue") && Colour == true)
        {
            SwitchColour();
        }

        if (other.CompareTag("PlayerRed") && Colour == false)
        {
            SwitchColour();
        }
    }

    private void SwitchColour()
    {
        //включить объекты управления для команды захватившей город
        if(Colour == true)
        {
            SwitchObjects[0].SetActive(false);
            SwitchObjects[1].SetActive(false);
            SwitchObjects[2].SetActive(true);
            SwitchObjects[3].SetActive(true);
        }
        else if(Colour == false)
        {
            SwitchObjects[0].SetActive(true);
            SwitchObjects[1].SetActive(true);
            SwitchObjects[2].SetActive(false);
            SwitchObjects[3].SetActive(false);
        }
    }

    public void UnitsDonate(float Cost)
    {
        UnitCost = Cost;
    }
    public void SpawnUnit(int UnitNumber)
    {
        //Спавн юнитов
        if(Coin > UnitCost)
        {
            if(Colour == true)
            {
                PhotonNetwork.Instantiate(RedPlayerPrefab[UnitNumber].name, SpawnPosition.position, Quaternion.identity);
            }
            else if(Colour == false)
            {
                PhotonNetwork.Instantiate(BluePlayerPrefab[UnitNumber].name, SpawnPosition.position, Quaternion.identity);
            }
            Coin -= UnitCost;
        }
    }
}
