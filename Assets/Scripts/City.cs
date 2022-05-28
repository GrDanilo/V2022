using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class City : MonoBehaviour
{
    private PhotonView view;
    [SerializeField]private Canvas canvas;
    private Camera camera;
    [Header("Switch")]
    [SerializeField]private bool Colour;
    [SerializeField]private GameObject FinalMenu;
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

        if(!view.IsMine)
        {
            //Canvas.SetActive(false);
        }
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
            EndGame();
        }

        if (other.CompareTag("PlayerRed") && Colour == false)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        //включить финальное меню
        if(Colour == true)
        {
            FinalMenu.SetActive(true);
        }
        else if(Colour == false)
        {
            FinalMenu.SetActive(true);
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

    public void EndButton(bool MenuButton)
    {
        if(MenuButton == true)
        {
            SceneManager.LoadScene("Menu");
        }
        else if(MenuButton == false)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
