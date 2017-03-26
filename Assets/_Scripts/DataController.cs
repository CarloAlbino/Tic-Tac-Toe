using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public struct IconSprites
{
    public Sprite[] sprites;
}

public class DataController : Singleton<DataController> {

    // Current game data
    private int m_wins = 0;
    private int m_loses = 0;
    private int m_draws = 0;
    private int[] m_gameBoard = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private bool m_isPlayerTurn = true;
    private int m_currentTurn = 0;
    private Icon m_playerIcon;
    private Icon m_AIIcon;
    private bool m_isNewGame = true;
    // Getters and Setters
    public int wins { get { return m_wins; } set { m_wins = value; } }
    public int loses { get { return m_loses; } set { m_loses = value; } }
    public int draws { get { return m_draws; } set { m_draws = value; } }
    public int[] gameBoard { get { return m_gameBoard; } set { m_gameBoard = value; } }
    public bool isPlayerTurn { get { return m_isPlayerTurn; } set { m_isPlayerTurn = value; } }
    public int currentTurn { get { return m_currentTurn; } set { m_currentTurn = value; } }
    public Sprite playerIcon { get { return m_playerIcon.icon; } }
    public int playerIconIndex { set { m_playerIcon = new Icon(value); } }
    public Sprite AIIcon { get { return m_AIIcon.icon; } }
    public int AIIconIndex { set { m_AIIcon = new Icon(value); } }
    public bool newGame { get { return m_isNewGame; } }
    // Save key
    [SerializeField]
    private string m_saveKey = "SaveFile0";
    // Reference to sprites
    [SerializeField]
    private IconSprites m_sprites;
    public Sprite GetSprite(int index) { return m_sprites.sprites[index]; }

    public void LoadGame(bool isNewGame)
    {
        m_isNewGame = isNewGame;
        if (isNewGame)
        {
            m_wins = 0;
            m_loses = 0;
            m_draws = 0;
            m_gameBoard = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            m_isPlayerTurn = true;
            m_currentTurn = 0;
        }
        else
        {
            if(PlayerPrefs.HasKey(m_saveKey))
            {
                Debug.Log("Loading...");
                // Load save data
                string loadedSaveString = PlayerPrefs.GetString(m_saveKey);
                Debug.Log(loadedSaveString);
                // Parse save data
                string[] splitSaveString = loadedSaveString.Split('_');
                Debug.Log("Split string...");
                foreach (string s in splitSaveString)
                {
                    Debug.Log(s);
                }
                // Load wins, loses, and draws
                m_wins = Int32.Parse(splitSaveString[0]);
                Debug.Log("Loaded wins..");
                m_loses = Int32.Parse(splitSaveString[1]);
                Debug.Log("Loaded loses..");
                m_draws = Int32.Parse(splitSaveString[2]);
                Debug.Log("Loaded draws..");
                // Load game board
                string[] gameBoardString = splitSaveString[3].Split('/');
                for (int i = 0; i < gameBoardString.Length; i++)
                {
                    m_gameBoard[i] = Int32.Parse(gameBoardString[i]);
                }
                // Load turn state
                m_isPlayerTurn = Boolean.Parse(splitSaveString[4]);
                m_currentTurn = Int32.Parse(splitSaveString[5]);
                // Get icon
                Icon pIcon = new Icon(Int32.Parse(splitSaveString[6]));
                m_playerIcon = pIcon;
                Icon aIcon = new Icon(Int32.Parse(splitSaveString[7]));
                m_AIIcon = aIcon;
                // Complete
                Debug.Log("Game loaded.");
            }
            else
            {
                Debug.LogWarning("No save game detected. Starting a new game.");
                m_wins = 0;
                m_loses = 0;
                m_draws = 0;
                m_gameBoard = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                m_isPlayerTurn = true;
                m_currentTurn = 0;
                m_isNewGame = true;
            }
        }
    }

    public void SaveGame(int wins, int loses, int draws, int[] gameBoard, bool isPlayerTurn, int currentTurn, Icon playerIcon, Icon AIIcon)
    {
        Debug.Log("Saving...");
        string saveString = "";
        // Save wins, loses, and draws
        saveString += wins.ToString() + "_";
        saveString += loses.ToString() + "_";
        saveString += draws.ToString() + "_";
        // Save game board
        for(int i = 0; i < gameBoard.Length; i++)
        {
            saveString += gameBoard[i].ToString();
            if (i < gameBoard.Length - 1)
                saveString += "/";
        }
        saveString += "_";
        // Save turn state
        saveString += isPlayerTurn.ToString() + "_";
        saveString += currentTurn.ToString() + "_";
        // Save icons
        saveString += playerIcon.iconID.ToString() + "_";
        saveString += AIIcon.iconID.ToString();
        // Save to player prefs
        Debug.Log("Save string: " + saveString);
        PlayerPrefs.SetString(m_saveKey, saveString);
        Debug.Log("Game Saved!");
    }

    public void SaveGame()
    {
        Debug.Log("Saving...");
        string saveString = "";
        // Save wins, loses, and draws
        saveString += wins.ToString() + "_";
        saveString += loses.ToString() + "_";
        saveString += draws.ToString() + "_";
        // Save game board
        for (int i = 0; i < gameBoard.Length; i++)
        {
            saveString += gameBoard[i].ToString();
            if(i < gameBoard.Length - 1)
                saveString += "/";
        }
        saveString += "_";
        // Save turn state
        saveString += isPlayerTurn.ToString() + "_";
        saveString += m_currentTurn.ToString() + "_";
        // Save icons
        saveString += m_playerIcon.iconID.ToString() + "_";
        saveString += m_AIIcon.iconID.ToString();
        // Save to player prefs
        Debug.Log("Save string: " + saveString);
        PlayerPrefs.SetString(m_saveKey, saveString);
        Debug.Log("Game Saved!");
    }
    
}
