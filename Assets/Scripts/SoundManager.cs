using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SoundManager : MonoBehaviour {
	#region Members

	[Header("MUSICS")]
	public List<AudioClip> Music = new List<AudioClip>();

	[Header("SOUNDS")]
	public List<AudioClip> Sound= new List<AudioClip>();
	
	[Header("LIONROAR")]
	public List<AudioClip> LionRoar = new List<AudioClip>();

    [Header("BEARROAR")]
    public List<AudioClip> BearRoar = new List<AudioClip>();

    [Header("Sound Listeners")]
	public List<AudioSource> Source = new List<AudioSource>();


	#endregion

	// Use this for initialization
	void Start()
	{
		SoundManagerEvent.onEvent += Play;
	}

	void OnDestroy()
	{
		SoundManagerEvent.onEvent -= Play;
	}

	public void Play(SoundManagerType emt)
	{
		switch (emt)
		{
			case SoundManagerType.BumpBumper:
				Source[2].Stop();
				Source[2].clip = Music[0];
				Source[2].Play();
				break;

		case SoundManagerType.BumpPlayer:
			Source[2].Stop();
			Source[2].clip = Music[0];
			Source[2].Play();
			break;

		case SoundManagerType.BumpWall:
			Source[2].Stop();
			Source[2].clip = Music[0];
			Source[2].Play();
			break;


		case SoundManagerType.DamageBear:
			Source[2].Stop();
			Source[2].clip = Music[0];
			Source[2].Play();
			break;

		case SoundManagerType.DamageLion:
			Source[2].Stop();
			Source[2].clip = Music[0];
			Source[2].Play();
			break;

		case SoundManagerType.DoubleTapBear:
			Source[2].Stop();
			Source[2].clip = Music[0];
			Source[2].Play();
			break;

		case SoundManagerType.DoubleTapLion:
			Source[2].Stop();
			Source[2].clip = Music[0];
			Source[2].Play();
			break;

		case SoundManagerType.MainTheme:
			Source[2].Stop();
			Source[2].clip = Music[0];
			Source[2].Play();
			break;

		case SoundManagerType.MenuTheme:
			Source[2].Stop();
			Source[2].clip = Music[0];
			Source[2].Play();
			break;

		case SoundManagerType.NextTurn:
			Source[2].Stop();
			Source[2].clip = Music[0];
			Source[2].Play();
			break;

		case SoundManagerType.PlacePlayer:
			Source[2].Stop();
			Source[2].clip = Music[0];
			Source[2].Play();
			break;


			case SoundManagerType.ReleaseMush:
			Source[2].Stop();
			Source[2].clip = Music[0];
			Source[2].Play();
			break;

		case SoundManagerType.Shield:
			Source[2].Stop();
			Source[2].clip = Music[0];
			Source[2].Play();
			break;

		case SoundManagerType.SlingReleaseBear:
			Source[2].Stop();
			Source[2].clip = Music[0];
			Source[2].Play();
			break;

		case SoundManagerType.SlingReleaseLion:
			Source[2].Stop();
			Source[2].clip = Music[0];
			Source[2].Play();
			break;

		case SoundManagerType.SlingScale:
			Source[2].Stop();
			Source[2].clip = Music[0];
			Source[2].Play();
			break;

		case SoundManagerType.SpellStop:
			Source[2].Stop();
			Source[2].clip = Music[0];
			Source[2].Play();
			break;

		case SoundManagerType.TakeMush:
			Source[2].Stop();
			Source[2].clip = Music[0];
			Source[2].Play();
			break;

		case SoundManagerType.Transition:
			Source[2].Stop();
			Source[2].clip = Music[0];
			Source[2].Play();
			break;

		case SoundManagerType.Validation:
			Source[2].Stop();
			Source[2].clip = Music[0];
			Source[2].Play();
			break;

		case SoundManagerType.Victory:
			Source[2].Stop();
			Source[2].clip = Music[0];
			Source[2].Play();
			break;

		}
	}

	

}
