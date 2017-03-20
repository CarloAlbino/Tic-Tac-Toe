using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TicTacTowText : MonoBehaviour {

    public Text[] buttonText;
    private int[] board = { 0, 0, 0, 0, 0, 0, 0, 0, 0};
    private bool canPlay = true;
	// Use this for initialization
	void Start () {
        PrintBoard();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    int win(int[] board)
    {
        uint[,] wins = { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 }, { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 }, { 0, 4, 8 }, { 2, 4, 6 } };

        for(int i = 0; i < 8; ++i)
        {
            if (board[wins[i, 0]] != 0 && board[wins[i, 0]] == board[wins[i, 1]] && board[wins[i, 0]] == board[wins[i, 2]])
            {
                return board[wins[i, 2]];
            }
        }
        return 0;
    }

    int MiniMax(int[] board, int player)
    {
        // How is the posiiton like for the player (their turn) on the board?
        int winner = win(board);
        if (winner != 0)
            return winner * player;

        int move = -1;
        int score = -2; // Losing moves are prefered to no move

        for(int i = 0; i < board.Length; ++i) // For all moves
        {
            if(board[i] == 0) // If a legal move
            {
                board[i] = player; // Try the move
                int thisScore = -MiniMax(board, player * -1);
                if(thisScore > score)
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

    void ComputerMove(int[] board)
    {
        int move = -1;
        int score = -2;

        for(int i = 0; i < board.Length; ++i)
        {
            if(board[i] == 0)
            {
                board[i] = 1;
                int tempScore = -MiniMax(board, -1);
                board[i] = 0;
                if(tempScore > score)
                {
                    score = tempScore;
                    move = i;
                }
            }
        }

        // returns a score based on minimax tree at a given node
        board[move] = 1;
        PrintBoard();
        canPlay = true;
    }

    public void PlayerMove(int pos)
    {
        if(board[pos] == 0)
        {
            board[pos] = -1;
            PrintBoard();
            canPlay = false;
            StartCoroutine(PlayComputer());
        }
    }

    private IEnumerator PlayComputer()
    {
        yield return new WaitForSeconds(2);
        ComputerMove(board);
    }

    private void PrintBoard()
    {
        for(int i = 0; i < board.Length; i++)
        {
            if(board[i] == -1)
            {
                buttonText[i].text = "X";
            }
            else if(board[i] == 1)
            {
                buttonText[i].text = "O";
            }
            else
            {
                buttonText[i].text = "";
            }
        }
    }
}
