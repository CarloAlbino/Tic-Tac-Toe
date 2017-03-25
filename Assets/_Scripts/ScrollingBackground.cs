using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {

    [SerializeField]
    private float m_scrollSpeed = 1.0f;
    [SerializeField]
    private GameObject[] m_backgroundPanels;
    [SerializeField]
    private float m_panelSize = 1;
    [SerializeField]
    private Transform m_endPosition;

	void Update ()
    {
        // Move all panels first
        for (int i = 0; i < m_backgroundPanels.Length; i++)
        {
            m_backgroundPanels[i].transform.Translate(Vector2.left * m_scrollSpeed * Time.deltaTime);
        }
        // Reset position if off screen
        for (int i = 0; i < m_backgroundPanels.Length; i++)
        {
            if (m_backgroundPanels[i].transform.position.x < m_endPosition.position.x)
            {
                int lastPanelIndex = ((i - 1) < 0) ? (m_backgroundPanels.Length - 1) : (i - 1);
                Vector3 newPosition = m_backgroundPanels[i].transform.position;
                newPosition.x = m_backgroundPanels[lastPanelIndex].transform.position.x + m_panelSize;
                m_backgroundPanels[i].transform.position = newPosition;
            }
        }
    }
}
