using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Victory : MonoBehaviour {

    public Color m_LionColor;
    public Color m_BearColor;

    public Sprite m_LionSprite;
    public Sprite m_BearSprite;

    public Image m_ImageWinner;
    public Image m_BackWinner;


    public void IsLionWin(bool isLionWin)
    {
        if(isLionWin)
        {
            m_ImageWinner.sprite = m_LionSprite;
            m_BackWinner.color = m_LionColor;
        }
        else
        {
            m_ImageWinner.sprite = m_BearSprite;
            m_BackWinner.color = m_BearColor;
        }
    }
}
