using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeBackground : MonoBehaviour {

    [SerializeField]
    private float m_scrollSpeed = 1.0f;
    [SerializeField]
    private SpriteRenderer[] m_backgroundPanels;
    [SerializeField]
    private Color[] m_colourTargets;

    private uint m_nextTargetIndex = 0;
	
	void Update ()
    {
        // Lerp to next colour in sequence
        for (int i = 0; i < m_backgroundPanels.Length; i++)
        {
            Color newColour = Color.Lerp(m_backgroundPanels[i].color, m_colourTargets[m_nextTargetIndex], m_scrollSpeed * Time.deltaTime);
            m_backgroundPanels[i].color = newColour;
        }

        // Go to next colour when the current colour is reached
        if (m_backgroundPanels[0].color == m_colourTargets[m_nextTargetIndex])
        {
            m_nextTargetIndex++;
            if (m_nextTargetIndex >= m_colourTargets.Length)
                m_nextTargetIndex = 0;
        }
    }
}
