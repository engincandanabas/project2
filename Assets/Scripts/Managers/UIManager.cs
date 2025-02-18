using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject winView;
    [SerializeField] private GameObject failView;
    [SerializeField] private Button failTryButton;
    [SerializeField] private Button winTryButton;

    private void Start()
    {
        failTryButton.onClick.AddListener(() => RestartGame());
        winTryButton.onClick.AddListener(() => RestartGame());
    }
    private void OnEnable()
    {
        GameManager.OnGameWin += EnableWinView;
        GameManager.OnGameFail += EnableFailView;
    }
    private void OnDisable()
    {
        GameManager.OnGameWin -= EnableWinView;
        GameManager.OnGameFail -= EnableFailView;
    }
    private void EnableWinView()
    {
        winView.SetActive(true);
    }
    private void EnableFailView()
    {
        failView.SetActive(true);
    }
    private void RestartGame()
    {
        DOTween.KillAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
