using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance {  get; private set; }

    public delegate void GameStateChange();
    public static event GameStateChange OnGameWin;
    public static event GameStateChange OnGameFail;


    private bool game_success;
    private bool game_fail;

    public bool GameFail
    {
        get
        {
            return game_fail;
        }
        set
        {
            if (game_fail != value)
            {
                game_fail = value;
                if (game_fail)
                {
                    OnGameFail?.Invoke();
                }
            }
        }
    }

    public bool GameSuccess
    {
        get
        {
            return game_success;
        }
        set
        {
            if (game_success != value)
            {
                game_success = value;
                if (game_success)
                {
                    OnGameWin?.Invoke();
                }
            }
        }
    }

    private void Awake()
    {
        Instance = this;
        GameSuccess = false;
        GameFail = false;
    }

}
