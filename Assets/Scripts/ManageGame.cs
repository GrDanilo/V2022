using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;


public class ManageGame : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject CameraPrefab;
    [SerializeField] Vector3 RedPos;
    [SerializeField] Vector3 BluePos;
    [SerializeField] private int Team;
    [SerializeField] private PhotonView view;

    [SerializeField] GameObject CityPrefabRed;
    [SerializeField] GameObject CityPrefabBlue;
    [SerializeField] Vector3[] CitySpawners;

    [SerializeField] GameObject Button;
    private int rand;

    private void Start()
    {
        //Vector3 pos = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        int selectedChar = PlayerPrefs.GetInt("SelectedPlayer");
        rand = Random.Range(0, 11);

        view = GetComponent<PhotonView>();

        if(PhotonNetwork.IsMasterClient == true)
        {
            //Первый игрок подключится к красной команде
            Team = 0;
            PhotonNetwork.Instantiate(CityPrefabRed.name, CitySpawners[rand], Quaternion.identity);
        } 
        else
        {
            Team = 1;
            PhotonNetwork.Instantiate(CityPrefabBlue.name, CitySpawners[rand], Quaternion.identity); 
        }
    }

    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Menu");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.LogFormat("Player {0} entered room", newPlayer.NickName);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.LogFormat("Player {0} left room", otherPlayer.NickName);
    }
}
