using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public InputField nickField;
    public InputField joinInput;
    public InputField createInput;

    private void Start()
    {
        nickField.text = PlayerPrefs.GetString("name");

        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.NickName = nickField.text;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(createInput.text, new Photon.Realtime.RoomOptions { MaxPlayers = 2 });
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined the room");

        PhotonNetwork.LoadLevel("Game");
    }

    public void SaveName()
    {
        PlayerPrefs.SetString("name", nickField.text);
        PhotonNetwork.NickName = nickField.text;
    }
}
