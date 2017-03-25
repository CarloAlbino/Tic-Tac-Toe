using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icon : MonoBehaviour
{
    private Sprite m_icon;
    private int m_iconID;

    public Sprite icon { get { return m_icon; } }
    public int iconID { get { return m_iconID; } }

    public Icon(int ID)
    {
        m_icon = DataController.Instance.GetSprite(ID);
        m_iconID = ID;
    }
}
