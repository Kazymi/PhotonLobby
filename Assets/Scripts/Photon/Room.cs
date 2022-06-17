using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text roomName;
    [SerializeField] private TMP_Text listPeopleInRoom;
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button toMenuButton;

    private PanelController _panelController;
    private void Start()
    {
        _panelController = ServiceLocator.GetService<PanelController>();
        startGameButton.gameObject.SetActive(false);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        toMenuButton.onClick.AddListener(ToMenu);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        toMenuButton.onClick.RemoveListener(ToMenu);
    }

    public override void OnCreatedRoom()
    {
        startGameButton.gameObject.SetActive(true);
        Debug.Log("Created Room");
    }

    public override void OnJoinedRoom()
    {
        _panelController.ActivatePanel(MainMenuPanelType.Room);
        roomName.text = PhotonNetwork.CurrentRoom.Name;
        RecalculatePeopleInRoom();
        Debug.Log("Joined Room");
    }

    private void ToMenu()
    {
        PhotonNetwork.LeaveRoom();
        _panelController.ActivatePanel(MainMenuPanelType.MainMenu);

    }
    private void RecalculatePeopleInRoom()
    {
        listPeopleInRoom.text = "";
        var peopleInRoom = PhotonNetwork.CurrentRoom.Players;
        foreach (var player in peopleInRoom)
        {
            listPeopleInRoom.text += $"\n{player.Value.NickName}";
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("Player left to the room");
        RecalculatePeopleInRoom();
        CheckHost();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player enter to the room");
        RecalculatePeopleInRoom();
    }

    private void CheckHost()
    {
        if (PhotonNetwork.CurrentRoom.masterClientId == PhotonNetwork.LocalPlayer.ActorNumber)
        {
            startGameButton.gameObject.SetActive(true);
        }
    }
    public override void OnLeftRoom()
    {
        startGameButton.gameObject.SetActive(false);
        _panelController.ActivatePanel(MainMenuPanelType.Loading);
        Debug.Log("Left from room");
    }
}