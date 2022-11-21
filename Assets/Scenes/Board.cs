using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Row
{
    public Slot[] e = new Slot[3];
    public bool IsWin()
    {
        return e[0].IsEmpty() == false && (e[0].status == e[1].status && e[1].status == e[2].status);
    }
    public void Reset()
    {
        e[0].Clear();
        e[1].Clear();
        e[2].Clear();
    }
}

[Serializable]
public class Board
{
    public enum GameStatus { AI_WIN, PLAYER_WIN, DRAW, RUNNING };
    public Row[] rows = new Row[3];

    public GameStatus GetGameStatus()
    {
        for (int i = 0; i < 3; ++i)
        {
            if (rows[i].IsWin())
            {
                if (Get(0, i) == 'O') return GameStatus.PLAYER_WIN;
                else return GameStatus.AI_WIN;
            }
        }

        for (int i = 0; i < 3; ++i)
        {
            if (Get(i, 0) != ' ' && Get(i, 0) == Get(i, 1) && Get(i, 0) == Get(i, 2))
            {
                if (Get(i, 0) == 'O') return GameStatus.PLAYER_WIN;
                else return GameStatus.AI_WIN;
            }
        }

        // diagonal
        if (Get(1, 1) != ' ')
        {
            if ((Get(0, 0) == Get(1, 1) && Get(0, 0) == Get(2, 2))
                || (Get(0, 2) == Get(1, 1) && Get(0, 2) == Get(2, 0)))
            {
                if (Get(1, 1) == 'O') return GameStatus.PLAYER_WIN;
                else return GameStatus.AI_WIN;
            }
        }

        // check if the board is full
        for (int i = 0; i < 3; ++i)
        {
            for (int j = 0; j < 3; ++j)
            {
                if (Get(i, j) == ' ') return GameStatus.RUNNING;
                if (Get(i, j) == ' ') return GameStatus.RUNNING;
                if (Get(i, j) == ' ') return GameStatus.RUNNING;
            }
        }

        return GameStatus.DRAW;
    }

    public char Get(int x, int y)
    {
        return rows[y].e[x].status;
    }

    public void Set(int x, int y, char c)
    {
        rows[y].e[x].status = c;
    }

    public void AIPick(int x, int y)
    {
        Debug.Log("AI pick: " + x + ", " + y);
        rows[y].e[x].OnAIPick();
    }

    public void Reset()
    {
        for (int i = 0; i < 3; ++i)
        {
            rows[i].Reset();
        }
    }
    
}
