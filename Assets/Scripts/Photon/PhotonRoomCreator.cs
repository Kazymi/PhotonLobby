using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhotonRoomCreator : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField roomNameField;
    [SerializeField] private Slider maxPlayerSlider;
    [SerializeField] private Button createRoomButton;

    private PanelController _panelController;

    private void Start()
    {
        _panelController = ServiceLocator.GetService<PanelController>();
    }

    public override void OnEnable()
    {
        createRoomButton.onClick.AddListener(CreateRoom);
    }

    public override void OnDisable()
    {
        createRoomButton.onClick.RemoveListener(CreateRoom);
    }

    private void CreateRoom()
    {
        if (string.IsNullOrEmpty(roomNameField.text) || roomNameField.text.Length > 10)
        {
            Debug.Log("Your room name is unCorrectly");
            return;
        }

        var roomSetting = new RoomOptions {MaxPlayers = (byte) maxPlayerSlider.value};
        PhotonNetwork.CreateRoom(roomNameField.text, roomSetting, TypedLobby.Default);
        _panelController.ActivatePanel(MainMenuPanelType.Loading);
        Debug.Log("Room created");
    }
}