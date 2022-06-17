using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PanelButtonController : MonoBehaviour
{
    [SerializeField] private MainMenuPanelType openPanelType;

    private Button _button;
    private PanelController _panelController;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OpenPanel);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OpenPanel);
    }

    private void Start()
    {
        _panelController = ServiceLocator.GetService<PanelController>();
    }

    private void OpenPanel()
    {
        _panelController.ActivatePanel(openPanelType);
    }
}
