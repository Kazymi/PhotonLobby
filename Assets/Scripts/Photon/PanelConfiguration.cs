using System;
using UnityEngine;

[Serializable]
public class PanelConfiguration
{
    [SerializeField] private MainMenuPanelType menuPanelType;
    [SerializeField] private Canvas canvas;

    public MainMenuPanelType MenuPanelType => menuPanelType;

    public void DisableCanvas()
    {
        canvas.enabled = false;
    }

    public void EnableCanvas()
    {
        canvas.enabled = true;
    }
}