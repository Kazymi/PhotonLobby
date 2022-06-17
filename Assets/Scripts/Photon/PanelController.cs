using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    [SerializeField] private List<PanelConfiguration> panelConfigurations;
    private Dictionary<MainMenuPanelType, PanelConfiguration> _panelConfigurations;

    private void Awake()
    {
        Initialize();
    }

    private void OnEnable()
    {
        ServiceLocator.Subscribe<PanelController>(this);
    }

    private void OnDisable()
    {
        ServiceLocator.Unsubscribe<PanelController>();
    }

    private void Initialize()
    {
        _panelConfigurations = new Dictionary<MainMenuPanelType, PanelConfiguration>();
        foreach (var panelConfiguration in panelConfigurations)
        {
            _panelConfigurations.Add(panelConfiguration.MenuPanelType, panelConfiguration);
            panelConfiguration.DisableCanvas();
        }

        _panelConfigurations[MainMenuPanelType.Loading]?.EnableCanvas();
    }

    public void ActivatePanel(MainMenuPanelType panelType)
    {
        foreach (var panelConfiguration in _panelConfigurations)
        {
            if (panelConfiguration.Key == panelType)
            {
                panelConfiguration.Value.EnableCanvas();
            }
            else
            {
                panelConfiguration.Value.DisableCanvas();
            }
        }
    }
}