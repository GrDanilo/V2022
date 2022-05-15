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
    [SerializeField] private int team;
    [SerializeField] private PhotonView view;

    [SerializeField] GameObject CityPrefab;
    [SerializeField] Vector3[] CitySpawners;
    private int rand;

    private void Start()
    {
        //Vector3 pos = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        int selectedChar = PlayerPrefs.GetInt("SelectedPlayer");
        rand = Random.Range(0, 11);

        if (team == 1)
        {
            //PhotonNetwork.Instantiate(CameraPrefab.name, BluePos, Quaternion.identity);
            PhotonNetwork.Instantiate(CityPrefab.name, CitySpawners[rand], Quaternion.identity);
        }

        if(team == 0)
        {
            //PhotonNetwork.Instantiate(CameraPrefab.name, RedPos, Quaternion.identity);
            team += 1;
            view.RPC("Team", RpcTarget.AllBuffered, team);
            PhotonNetwork.Instantiate(CityPrefab.name, CitySpawners[rand], Quaternion.identity);
        }
    }

    [PunRPC]
    public void Team(float TeamNumber)
    {
        //team = TeamNumber;
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
