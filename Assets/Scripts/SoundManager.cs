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
				Source[5].Stop();
				Source[5].clip = Sound[0];
				Source[5].Play();
				break;

		case SoundManagerType.BumpPlayer:
			Source[5].Stop();
			Source[5].clip = Sound[1];
			Source[5].Play();
			break;

		case SoundManagerType.BumpWall:
			Source[5].Stop();
			Source[5].clip = Sound[2];
			Source[5].Play();
			break;


		case SoundManagerType.DamageBear:
			Source[2].Stop();
			Source[2].clip = BearRoar[0];
			Source[2].Play();
			break;

		case SoundManagerType.DamageLion:
			Source[1].Stop();
			Source[1].clip = LionRoar[0];
			Source[1].Play();
			break;

		case SoundManagerType.DoubleTapBear:
			Source[2].Stop();
			Source[2].clip = Music[0];
			Source[2].Play();
			break;

		case SoundManagerType.DoubleTapLion:
			Source[1].Stop();
			Source[1].clip = LionRoar[1];
			Source[1].Play();
			break;

		case SoundManagerType.MainTheme:
			Source[0].Stop();
			Source[0].clip = Music[0];
			Source[0].Play();
			break;

		case SoundManagerType.MenuTheme:
			Source[0].Stop();
			Source[0].clip = Music[1];
			Source[0].Play();
			break;

		case SoundManagerType.NextTurn:
			Source[7].Stop();
			Source[7].clip = Sound[3];
			Source[7].Play();
			break;

		case SoundManagerType.PlacePlayer:
			Source[10].Stop();
			Source[10].clip = Sound[4];
			Source[10].Play();
			break;

			case SoundManagerType.ReleaseMush:
			Source[8].Stop();
			Source[8].clip = Sound[5];
			Source[8].Play();
			break;

		case SoundManagerType.Shield:
			Source[9].Stop();
			Source[9].clip = Sound[6];
			Source[9].Play();
			break;

		case SoundManagerType.SlingReleaseBear:
			Source[2].Stop();
			Source[2].clip = BearRoar[2];
			Source[2].Play();
			break;

		case SoundManagerType.SlingReleaseLion:
			Source[1].Stop();
			Source[1].clip = LionRoar[2];
			Source[1].Play();
			break;

		case SoundManagerType.SlingScale:
			Source[10].Stop();
			Source[10].clip = Sound[7];
			Source[10].Play();
			break;

		case SoundManagerType.SpellStop:
			Source[4].Stop();
			Source[4].clip = Sound[9];
			Source[4].Play();
			break;

		case SoundManagerType.SpellAcceleration:
			Source[4].Stop();
			Source[4].clip = Sound[8];
			Source[4].Play();
			break;

		case SoundManagerType.TakeMush:
			Source[8].Stop();
			Source[8].clip = Sound[10];
			Source[8].Play();
			break;

		case SoundManagerType.Transition:
			Source[5].Stop();
			Source[5].clip = Sound[11];
			Source[5].Play();
			break;

		case SoundManagerType.Validation:
			Source[5].Stop();
			Source[5].clip = Sound[12];
			Source[5].Play();
			break;
			/*
		case SoundManagerType.Victory:
			Source[2].Stop();
			Source[2].clip = Sound[0];
			Source[2].Play();
			break;
*/
		}
	}

	

}
