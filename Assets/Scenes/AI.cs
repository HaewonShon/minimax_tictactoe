using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AI
{
    public Board board;
    public const int WIN = 100;
    public const int LOSE = -100;
    
    public int[] Minimax(bool isAITurn, int depth) // int[3] {score, x, y}
    {
        int[] best = new int[3];
        best[0] = (isAITurn ? int.MinValue : int.MaxValue); // score
        best[1] = -1; // x
        best[2] = -1; // y            
        
        Board.GameStatus status = board.GetGameStatus();

        if (status != Board.GameStatus.RUNNING)
        {
            if (status == Board.GameStatus.DRAW)
            {
                best[0] = 0;
            }
            else if (status == Board.GameStatus.AI_WIN)
            {
                best[0] = WIN;
            }
            else if (status == Board.GameStatus.PLAYER_WIN)
            {
                best[0] = LOSE;
            }
            return best; // if game end = return best;
        }


        // Perform DFS for nodes(next status of the board)
        for (int i = 0; i < 3; ++i)
        {
            for (int j = 0; j < 3; ++j)
            {
                if (board.Get(j, i) != ' ') continue;

                // AI turn - get max
                if (isAITurn)
                {
                    board.Set(j, i, 'X');

                    int[] result = Minimax(!isAITurn, depth + 1);
                    if (result[0] > best[0])
                    {
                        best[0] = result[0] - depth;
                        best[1] = j;
                        best[2] = i;
                    }

                    board.Set(j, i, ' ');
                }
                // Player Turn - get min
                else
                {
                    board.Set(j, i, 'O');

                    int[] result = Minimax(!isAITurn, depth + 1);
                    if (result[0] < best[0]) // get minimum score
                    {
                        best[0] = result[0] + depth;
                        best[1] = j;
                        best[2] = i;
                    }

                    board.Set(j, i, ' ');
                }
            }
        }


        return best;
    }

    public int[] MinimaxPruning(bool isAITurn, int depth, int alpha, int beta) // int[3] {score, x, y}
    {
        int[] best = new int[3];
        best[0] = (isAITurn ? int.MinValue : int.MaxValue); // score
        best[1] = -1; // x
        best[2] = -1; // y            

        Board.GameStatus status = board.GetGameStatus();

        if (status != Board.GameStatus.RUNNING)
        {
            if (status == Board.GameStatus.DRAW)
            {
                best[0] = 0;
            }
            else if (status == Board.GameStatus.AI_WIN)
            {
                best[0] = WIN;
            }
            else if (status == Board.GameStatus.PLAYER_WIN)
            {
                best[0] = LOSE;
            }
            return best; // if game end = return best;
        }

        bool shouldStop = false;
        // Perform DFS for nodes(next status of the board)
        for (int i = 0; i < 3; ++i)
        {
            for (int j = 0; j < 3; ++j)
            {
                if (board.Get(j, i) != ' ') continue;

                // AI turn - get max
                if (isAITurn)
                {
                    board.Set(j, i, 'X');

                    int[] result = MinimaxPruning(!isAITurn, depth + 1, alpha, beta);
                    if (result[0] > best[0])
                    {
                        best[0] = result[0] - depth;
                        best[1] = j;
                        best[2] = i;

                        alpha = Math.Max(alpha, best[0]);
                        if (beta <= alpha)
                        {
                            shouldStop = true;
                        }
                    }

                    board.Set(j, i, ' ');
                }
                // Player Turn - get min
                else
                {
                    board.Set(j, i, 'O');

                    int[] result = MinimaxPruning(!isAITurn, depth + 1, alpha, beta);
                    if (result[0] < best[0]) // get minimum score
                    {
                        best[0] = result[0] + depth;
                        best[1] = j;
                        best[2] = i;

                        beta = Math.Min(beta, best[0]);
                        if (beta <= alpha)
                        {
                            shouldStop = true;
                        }
                    }

                    board.Set(j, i, ' ');
                }
                if (shouldStop == true) break;
            }
            if (shouldStop == true) break;
        }


        return best;
    }

    public int[] GetNextSpot(bool isPruning = false)
    {
        int[] spot;
        if (isPruning)
        {
            spot = MinimaxPruning(true, 0, int.MinValue, int.MaxValue);
        }
        else
        {
            spot = Minimax(true, 0);
        }
        
         
        return new int[2] { spot[1], spot[2] };
    }
}
