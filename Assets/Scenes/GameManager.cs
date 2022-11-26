using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum GameState { AI_TURN, PLAYER_TURN, END };
    private GameState state;

    public Board board = new Board();

    public Text statusText;
    public AI ai = new AI();

    public bool usePruning;

    public Text timeMeasure;

    // Start is called before the first frame update
    void Start()
    {
        state = GameState.PLAYER_TURN;
        ai.board = board;
        usePruning = true;
    }

    public void OnClick()
    {
        if (state == GameState.AI_TURN)
        {
            state = GameState.PLAYER_TURN;
        }
        else if (state == GameState.PLAYER_TURN)
        {
            state = GameState.AI_TURN;
        }

        Board.GameStatus status = board.GetGameStatus();
        if (status != Board.GameStatus.RUNNING)
        {
            state = GameState.END;
        }
        else // Game does not end -> run AI turn and check status again
        {
            // time measrue start
            float start = Time.realtimeSinceStartup;

            int[] aiMove = ai.GetNextSpot(usePruning);

            float end = Time.realtimeSinceStartup;

            // time measrue finish
            timeMeasure.text = "Time: " + ((end - start) * 1000).ToString() + "ms";

            board.AIPick(aiMove[0], aiMove[1]);

            state = GameState.PLAYER_TURN;

            status = board.GetGameStatus();
            if (status != Board.GameStatus.RUNNING)
            {
                state = GameState.END;
            }
        }

        switch (status)
        {
            case Board.GameStatus.PLAYER_WIN:
                statusText.text = "PLAYER WIN";
                break;
            case Board.GameStatus.AI_WIN:
                statusText.text = "AI WIN";
                break;
            case Board.GameStatus.DRAW:
                statusText.text = "DRAW";
                break;
            case Board.GameStatus.RUNNING:
                statusText.text = "RUNNING";
                break;
        }
    }

    public void Reset()
    {
        board.Reset();
        state = GameState.PLAYER_TURN;
        statusText.text = "RUNNING";
    }

    public GameState GetState()
    {
        return state;
    }

    public void ToggleUsePruning()
    {
        usePruning = !usePruning;
    }
}
