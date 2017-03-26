using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    [SerializeField]
    private int m_gameLevelIndex = 1;
    
    public void _Menu_NewGame()
    {
        // Flag to go to icon selection before game begins
        DataController.Instance.LoadGame(true);
        // Load game scene
        SceneManager.LoadScene(m_gameLevelIndex);
    }

    public void _Menu_LoadGame()
    {
        // Load data first
        DataController.Instance.LoadGame(false);
        // Load game scene
        SceneManager.LoadScene(m_gameLevelIndex);
    }

    public void _Menu_QuitGame()
    {
        // Quit game
        Application.Quit();
    }
}
