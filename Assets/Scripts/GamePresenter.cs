using System;
using UnityEngine;
using UnityEngine.UI;

class GamePresenter : MonoBehaviour
{
    [SerializeField] Button _continueButton;
    [SerializeField] Button _reloadButton;
    [SerializeField] GameObject _victoryPanel;
    [SerializeField] GameObject _failurePanel;

    public void Initialize(Action dismissed)
    {
        _continueButton.onClick.AddListener(() => dismissed());
        _reloadButton.onClick.AddListener(() => dismissed());
    }

    public void ShowVictory() => _victoryPanel.SetActive(true);
    public void ShowFailure() => _failurePanel.SetActive(true);

    public void HidePanels()
    {
        _victoryPanel.SetActive(false);
        _failurePanel.SetActive(false);
    }
}