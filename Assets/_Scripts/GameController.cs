using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour { 

    [SerializeField]
    private Sprite m_xSprite, m_oSprite, m_emptySprite;
    [SerializeField]
    private  Image[] m_boardImage;
    [SerializeField]
    private ScoreBoardController m_scoreBoard;

    private int[] m_board = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private bool m_canPlay = true;
    private int m_turn = 0;

    public void StartGame(int[] board, bool isPlayerTurn, int turn)
    {
        m_xSprite = DataController.Instance.playerIcon;
        m_oSprite = DataController.Instance.AIIcon;

        m_board = board;
        m_canPlay = isPlayerTurn;
        m_turn = turn;

        PrintBoard();

        if(!isPlayerTurn)
        {
            StartCoroutine(PlayComputer());
        }
    }

    public void ResetGame()
    {
        for(int i = 0; i < m_board.Length; i++)
        {
            m_board[i] = 0;
        }
        m_canPlay = true;
        m_turn = 0;
        DataController.Instance.isPlayerTurn = true;
        m_scoreBoard.UpdateTurn();

        PrintBoard();
    }

    public void _Game_PlayerMove(int pos)
    {
        if (m_canPlay)
        {
            if (m_board[pos] == 0)
            {
                m_board[pos] = -1;
                PrintBoard();
                m_canPlay = false;
                DataController.Instance.isPlayerTurn = false;
                m_scoreBoard.UpdateTurn();
                // Check for win
                if (Win(m_board) == 0)
                {
                    bool isDraw = CheckForDraw();
                    if (!isDraw)
                    {
                        // Game not over
                        // Continue game
                        StartCoroutine(PlayComputer());
                    }
                    else
                    {
                        // Draw
                        m_scoreBoard.ShowGameOverDisplay(0);
                        DataController.Instance.draws += 1;
                    }
                }
                else
                {
                    // Game over, player wins
                    m_scoreBoard.ShowGameOverDisplay(1);
                    DataController.Instance.wins += 1;
                }
            }
        }
    }

    public void PrintBoard()
    {
        for (int i = 0; i < m_board.Length; i++)
        {
            if (m_board[i] == -1)
            {
                m_boardImage[i].sprite = m_xSprite;
            }
            else if (m_board[i] == 1)
            {
                m_boardImage[i].sprite = m_oSprite;
            }
            else
            {
                m_boardImage[i].sprite = m_emptySprite;
            }
        }
    }

    private int Win(int[] board)
    {
        uint[,] wins = { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 }, { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 }, { 0, 4, 8 }, { 2, 4, 6 } };

        for (int i = 0; i < 8; ++i)
        {
            if (board[wins[i, 0]] != 0 && board[wins[i, 0]] == board[wins[i, 1]] && board[wins[i, 0]] == board[wins[i, 2]])
            {
                return board[wins[i, 2]];
            }
        }
        return 0;
    }

    private int MiniMax(int[] board, int player)
    {
        // How is the posiiton like for the player (their turn) on the board?
        int winner = Win(board);
        if (winner != 0)
            return winner * player;

        int move = -1;
        int score = -2; // Losing moves are prefered to no move

        for (int i = 0; i < board.Length; ++i) // For all moves
        {
            if (board[i] == 0) // If a legal move
            {
                board[i] = player; // Try the move
                int thisScore = -MiniMax(board, player * -1);
                if (thisScore > score)
                {
                    score = thisScore;
                    move = i;
                }
                // Pick the one that's the worst for the opponent
                board[i] = 0; // Reset the board after trying
            }
        }

        if (move == -1)
            return 0;

        return score;
    }

    private void ComputerMove(int[] board)
    {
        //int random3rdTurn = 1;
        //if (m_turn == 3)
        //{
        //    random3rdTurn = Random.Range(0, 100);
        //    Debug.Log("THIRD TURN: " + random3rdTurn);
        //}
        //if (m_turn == 0 || random3rdTurn % 2 == 0)
        if (m_turn == 3 && Random.Range(0, 1000) < 150)  // 15% chance of a random move on 3rd turn
        {
            print("Playing random move");
            int randomMove;
            do
            {
                randomMove = Random.Range(0, 8);
                //print(randomMove);
            } while (board[randomMove] != 0);
            board[randomMove] = 1;
            PrintBoard();
        }
        else
        {
            int move = -1;
            int score = -2;

            for (int i = 0; i < board.Length; ++i)
            {
                if (board[i] == 0)
                {
                    board[i] = 1;
                    int tempScore = -MiniMax(board, -1);
                    board[i] = 0;
                    if (tempScore > score)
                    {
                        score = tempScore;
                        move = i;
                    }
                }
            }

            // returns a score based on minimax tree at a given node
            board[move] = 1;
            PrintBoard();
    }

        // Check for win
        if (Win(board) == 0)
        {
            bool isDraw = CheckForDraw();
            if (!isDraw)
            {
                // Game not over
                // Continue game
                m_canPlay = true;
                DataController.Instance.isPlayerTurn = true;
                m_scoreBoard.UpdateTurn();
            }
            else
            {
                // Draw
                m_scoreBoard.ShowGameOverDisplay(0);
                DataController.Instance.draws += 1;
            }
        }
        else
        {
            // Game over, AI wins
            m_scoreBoard.ShowGameOverDisplay(-1);
            DataController.Instance.loses += 1;
        }

        m_turn++;
        Debug.Log("Turn: " + m_turn);
    }

    private IEnumerator PlayComputer()
    {
        yield return new WaitForSeconds(1);
        ComputerMove(m_board);
    }

    private bool CheckForDraw()
    {
        foreach (int i in m_board)
        {
            if (i == 0)
            {
                return false;
            }
        }
        return true;
    }

}
