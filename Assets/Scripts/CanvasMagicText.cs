﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasMagicText : MonoBehaviour {

	public Text m_Text1;
	public Text m_Text2;
	public Text m_Text3;
	public Text m_Text4;

	public Animator m_Animator;

	public void ChangeText(string nouveauTexte)
	{
		m_Text1.text=nouveauTexte;
		m_Text2.text=nouveauTexte;
		m_Text3.text=nouveauTexte;
		m_Text4.text=nouveauTexte;
	}

	public void HideText()
	{
		m_Animator.SetTrigger ("Close");
	}

	public void HideText(float delay)
	{
		Invoke ("HideText", delay);
	}

	public void AppearText()
	{
		m_Animator.SetTrigger ("Open");
	}

}
