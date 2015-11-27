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

	public Animator m_Animator;

	private bool m_ReadyToGo=false;
    private bool m_panelIsOpened = false;
    private bool m_panelIsOpened2 = false;

    public GameObject m_1;
    public GameObject m_2;
    public GameObject m_3;
    public GameObject m_4;

    public GameObject m_Canvas;
    private bool m_ClosePanel = false;

    void Start()
    {

		SoundManagerEvent.emit(SoundManagerType.Victory);
        if(PlayerPrefs.GetInt("Winner")==1)
        {
            IsLionWin(false);
        }
        else
        {
            IsLionWin(true);
        }

    }

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

        m_Animator.SetTrigger("Open");

    }

	public void OkToGo()
	{
		m_ReadyToGo = true;

	}


    void OnMouseDown()
    {
        Debug.Log(m_ReadyToGo);
        if (m_ReadyToGo == true)
        {
            m_Animator.SetTrigger("Close");
            //Load Selfie Scene
        }
    }


}
