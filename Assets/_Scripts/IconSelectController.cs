using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IconSelectController : MonoBehaviour {

    [SerializeField]
    private Button[] m_iconSelectButtons;
    [SerializeField]
    private Image m_playerSelect;
    [SerializeField]
    private Image m_AISelect;
    [SerializeField]
    private Button m_playButton;
    [SerializeField]
    private Text m_feedbackText;

    [SerializeField]
    private GameObject m_iconSelectCanvas, m_gameController;
    [SerializeField]
    private ScoreBoardController m_scoreController;

    private bool m_canStartGame = false;
    private bool m_playerIsSelecting = true;

	void Update ()
    {
        if (DataController.Instance.newGame)
        {
            if (m_canStartGame)
            {
                m_playButton.interactable = true;
                m_feedbackText.text = "Click [START] to start the game";
            }
            else
            {
                m_playButton.interactable = false;
                if (m_playerIsSelecting)
                {
                    m_feedbackText.text = "Select your icon";
                }
                else
                {
                    m_feedbackText.text = "Select the CPU's icon";
                }
            }
        }
        else
        {
            m_canStartGame = true;
            _Select_StartGame();
        }
	}

    public void _Select_SetIcon(int index)
    {
        if (m_playerIsSelecting)
        {
            DataController.Instance.playerIconIndex = index;
            m_iconSelectButtons[index].interactable = false;
            m_playerSelect.sprite = DataController.Instance.GetSprite(index);
            m_playerSelect.color = Color.white;
            m_playerIsSelecting = false;
        }
        else
        {
            if (!m_canStartGame)
            {
                DataController.Instance.AIIconIndex = index;
                m_iconSelectButtons[index].interactable = false;
                m_AISelect.sprite = DataController.Instance.GetSprite(index);
                m_AISelect.color = Color.white;
                m_canStartGame = true;
            }
        }
    }

    public void _Select_ReturnToMenu()
    {
        // Load main menu
        SceneManager.LoadScene(0);
    }

    public void _Select_StartGame()
    {
        if(m_canStartGame)
        {
            // Start the game
            m_gameController.SetActive(true);
            m_scoreController.gameObject.SetActive(true);
            m_scoreController.StartGame();
            m_iconSelectCanvas.SetActive(false);
        }
    }
}
