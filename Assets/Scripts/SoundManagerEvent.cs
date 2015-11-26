using UnityEngine;
using System.Collections;

/*
 * Comment émettre un event:
		SoundManagerEvent.emit(EventManagerType.ENEMY_HIT);
 * 
 * Comment traiter un event (dans un start ou un initialisation)
		EventManagerScript.onEvent += (EventManagerType emt, GameObject go) => {
			if (emt == EventManagerType.ENEMY_HIT)
			{
				//SpawnFXAt(go.transform.position);
			}
		};
 * ou:
		void maCallback(EventManagerType emt, GameObject go) => {
			if (emt == EventManagerType.ENEMY_HIT)
			{
				//SpawnFXAt(go.transform.position);
			}
		};
		EventManagerScript.onEvent += maCallback;
 * 
 * qui permet de:
		EventManagerScript.onEvent -= maCallback; //Retire l'appel
 */


public enum SoundManagerType
{
	ABSOLUMENT,
	BIENJOUE,
	BONBOULOT,
	BRAVO,
	EXCELLENT,
	SUPER,
	AIEAIEAIE1,
	AIEAIEAIE2,
	AIEAIEAIE3,
	CENESTPASCA,
	NONNONNON1,
	NONNONNON2,
	PERDU,
	PRESQUE,
	TUFERASMIEUX,
	RANDOMPOSITIVE,
	RANDOMNEGATIVE,
	TURMEL00,
	TURMEL01,
	TURMEL02,
	TURMEL03,
	TURMEL04,
	TURMEL05,
	TURMEL06,
	TURMEL07,
	TURMEL08,
	TURMEL09,
	TURMEL10,
	MISHOOOO1,
	MISHOOOO2
}

public class SoundManagerEvent : MonoBehaviour
{

	public delegate void EventAction(SoundManagerType emt);
	public static event EventAction onEvent;

	#region Singleton
	static private SoundManagerEvent s_Instance;
	static public SoundManagerEvent instance
	{
		get
		{
			return s_Instance;
		}
	}
	


	void Awake()
	{
		if (s_Instance == null)
			s_Instance = this;
		//DontDestroyOnLoad(this);
	}
    #endregion
    void Start()
	{
		SoundManagerEvent.onEvent += (SoundManagerType emt) => { Debug.Log(""); };
	}

	public static void emit(SoundManagerType emt)	{
		
		if (onEvent != null)
		{
			onEvent(emt);
		}
	}



}
