using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreBoardController : MonoBehaviour {

    // In game UI
    [SerializeField]
    private Text m_wins, m_loses, m_draws;
    [SerializeField]
    private Image m_playerIcon, m_AIIcon;
    [SerializeField]
    private Image m_playerHighlight, m_AIHighlight;
    [SerializeField]
    private Button m_quitButton, m_saveButton;
    // Game over UI
    [SerializeField]
    private GameObject m_gameCompleteCanvas;
    [SerializeField]
    private Text m_endMessage;
    [SerializeField]
    private Image m_winnerIcon;
    [SerializeField]
    private Sprite m_drawIcon;
    // Reference to the game
    [SerializeField]
    private GameController m_game;

    public void StartGame()
    {
        Debug.Log("Starting game..");
        // Set all icons and stats
        m_game.StartGame(DataController.Instance.gameBoard, DataController.Instance.isPlayerTurn, 0); //ToDO: Save current turn;
        UpdateScoreTally();
        m_playerIcon.sprite = DataController.Instance.playerIcon;
        m_AIIcon.sprite = DataController.Instance.AIIcon;
        UpdateTurn();
        m_quitButton.interactable = true;
        m_saveButton.interactable = true;
        m_gameCompleteCanvas.SetActive(false);
    }

    public void UpdateTurn()
    {
        if (DataController.Instance.isPlayerTurn)
        {
            m_playerHighlight.color = Color.white;
            m_AIHighlight.color = Color.clear;
        }
        else
        {
            m_playerHighlight.color = Color.clear;
            m_AIHighlight.color = Color.white;
        }
    }

    public void UpdateScoreTally()
    {
        m_wins.text = DataController.Instance.wins.ToString();
        m_loses.text = DataController.Instance.loses.ToString();
        m_draws.text = DataController.Instance.draws.ToString();
    }

    public void ShowGameOverDisplay(int result)
    {
        m_gameCompleteCanvas.SetActive(true);
        m_quitButton.interactable = false;
        m_saveButton.interactable = false;

        switch(result)
        {
            case -1:
                // AI wins
                m_winnerIcon.sprite = DataController.Instance.AIIcon;
                m_endMessage.text = "CPU wins! Computers are so smart...";
                break;
            case 1:
                // Player wins
                m_winnerIcon.sprite = DataController.Instance.playerIcon;
                m_endMessage.text = "You win! Congrats on beating a machine!";
                break;
            default:
                // Draw
                m_winnerIcon.sprite = m_drawIcon;
                m_endMessage.text = "A draw! You're as smart as a computer!";
                break;
        }
    }

    public void _Score_Save()
    {
        // Save the game
        DataController.Instance.SaveGame();
    }

    public void _Score_Continue()
    {
        // Continue to next game
        m_gameCompleteCanvas.SetActive(false);
        UpdateScoreTally();
        // Call proper function on game controller
        m_game.ResetGame();
        m_quitButton.interactable = true;
        m_saveButton.interactable = true;
    }

    public void _Score_Quit()
    {
        // Return to main menu
        SceneManager.LoadScene(0);
    }
}
