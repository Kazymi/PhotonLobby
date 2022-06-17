using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private string region;

    private PanelController _panelController;

    private void Start()
    {
        _panelController = ServiceLocator.GetService<PanelController>();
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.ConnectToRegion(region);
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
    }

    public override void OnConnected()
    {
        Debug.Log($"Photon manager connected to Server {PhotonNetwork.CloudRegion}");
        _panelController.ActivatePanel(string.IsNullOrEmpty(PhotonNetwork.NickName)
            ? MainMenuPanelType.NickName
            : MainMenuPanelType.MainMenu);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Photon manager disconnected from Server");
    }
}