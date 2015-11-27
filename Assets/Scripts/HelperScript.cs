using UnityEngine;
using System.Collections;

public class HelperScript : MonoBehaviour {

	public SlingshotMenu m_SlingshotMenu;

	public void Helper()
	{
		m_SlingshotMenu.RestartHelper();
	}
}
