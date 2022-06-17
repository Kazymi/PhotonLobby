using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class NickNameController : MonoBehaviour
{
    [SerializeField] private TMP_InputField tmpInputField;
    [SerializeField] private Button saveNickName;
    [SerializeField] private Button randomNickNameButton;

    private PanelController _panelController;

    private void Start()
    {
        _panelController = ServiceLocator.GetService<PanelController>();
    }

    private readonly List<string> _randomName = new List<string>
    {
        "Быстрая", "Резкая", "Вонючяя", "Горбатая", "Мокрая", "Красивая", "Воинственная"
    };

    private readonly List<string> _randomSecondName = new List<string>
    {
        "Струя", "Мивина", "Горошина", "Хлебобучное изделие", "Какашка", "Тероборона", "Милки шоколадка"
    };

    private void OnEnable()
    {
        saveNickName.onClick.AddListener(InitializeNickName);
        randomNickNameButton.onClick.AddListener(RandomNameGenerator);
    }

    private void OnDisable()
    {
        saveNickName.onClick.RemoveListener(InitializeNickName);
        randomNickNameButton.onClick.RemoveListener(RandomNameGenerator);
    }

    private void RandomNameGenerator()
    {
        tmpInputField.text =
            $"{_randomName[Random.Range(0, _randomName.Count)]} {_randomSecondName[Random.Range(0, _randomSecondName.Count)]}";
    }

    private void InitializeNickName()
    {
        if (tmpInputField.text.Length > 20 || string.IsNullOrEmpty(tmpInputField.text)) return;
        PhotonNetwork.NickName = tmpInputField.text;
        _panelController.ActivatePanel(MainMenuPanelType.MainMenu);
    }
}