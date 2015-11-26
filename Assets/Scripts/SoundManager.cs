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
			case SoundManagerType.ABSOLUMENT:
				Source[2].Stop();
				Source[2].clip = Music[0];
				Source[2].Play();
				break;
		}
	}

	

}
