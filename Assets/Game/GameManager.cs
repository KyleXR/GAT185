using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;


public class GameManager : Singleton<GameManager>
{
    
    [SerializeField] AudioSource gameMusic;

    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform playerStart;

    [Header("Events")]
    [SerializeField] EventRouter startGameEvent;
    [SerializeField] EventRouter stopGameEvent;
    [SerializeField] EventRouter winGameEvent;


    public enum State
    {
        TITLE,
        START_GAME,
        PLAY_GAME,
        GAME_WON,
        GAME_OVER
    }

    State state = State.TITLE;
    float stateTimer = 1;
    private void Start()
    {
        winGameEvent.onEvent += SetGameWon;
    }

    private void Update()
    {
        switch (state)
        {
            case State.TITLE:
                UIManager.Instance.ShowTitle(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case State.START_GAME:
                UIManager.Instance.ShowTitle(true);
                Cursor.lockState = CursorLockMode.Locked;
                var player = Instantiate(playerPrefab, playerStart.position, playerStart.rotation);
                FindObjectOfType<CinemachineFreeLook>().LookAt = player.transform;
                FindObjectOfType<CinemachineFreeLook>().Follow = player.transform;
                state = State.PLAY_GAME;
                gameMusic.Play();
                break;
            case State.PLAY_GAME:
                break;
            case State.GAME_WON:
                stateTimer -= Time.deltaTime;
                if (stateTimer <= 0)
                {
                    UIManager.Instance.ShowGameWon(false);
                    state = State.TITLE;
                }
                break;
            case State.GAME_OVER:
                stateTimer -= Time.deltaTime;
                if(stateTimer <= 0)
                {
                    UIManager.Instance.ShowGameOver(false);
                    state = State.TITLE;
                }
                break;


        }
    }

    public void SetGameOver()
    {
        UIManager.Instance.ShowGameOver(true);
        state = State.GAME_OVER;
        stateTimer = 3;
    }

    public void SetGameWon()
    {
        UIManager.Instance.ShowGameWon(true);
        state = State.GAME_WON;
        stateTimer = 5;
    }

    public void OnStartGame()
    {
        state = State.START_GAME;
    }
}
