using System;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoPooled
{
    [SerializeField] private TMP_Text nameRoom;
    [SerializeField] private TMP_Text amountPlayerInRoom;
    [SerializeField] private Button connectButton;

    public void Initialize(RoomInfo info)
    {
        nameRoom.text = info.Name;
        amountPlayerInRoom.text = $"{info.PlayerCount}/{info.MaxPlayers}";
    }

    private void OnEnable()
    {
        connectButton.onClick.AddListener(Connect);
    }

    private void OnDisable()
    {
        connectButton.onClick.RemoveListener(Connect);
    }

    private void Connect()
    {
        PhotonNetwork.JoinRoom(nameRoom.text);
    }
}