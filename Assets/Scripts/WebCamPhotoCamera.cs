﻿using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class WebCamPhotoCamera : MonoBehaviour
{
    WebCamTexture webCamTexture;
    public Renderer m_RendererPhoto;
    public Renderer m_RendererRendu;

    bool m_PhotoMode=true;

    Texture2D photo;

	public Sprite Lion;
	public Sprite Bear;

	public Image Winner;

    void Start()
    {
		if(PlayerPrefs.GetInt("Win",2)==1)
		{
			Winner.sprite=Bear;
		}
		else
		{
			Winner.sprite=Lion;
		}

        webCamTexture = new WebCamTexture("Camera 1");
        //webCamTexture = new WebCamTexture();

        Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height);

        m_RendererPhoto.material.mainTexture = webCamTexture;
        
         //StartVideo
        webCamTexture.Play();

        
        //Set Mode
        m_PhotoMode = true;
        m_RendererPhoto.gameObject.SetActive(m_PhotoMode);
        m_RendererRendu.gameObject.SetActive(!m_PhotoMode);



    }

    //Take the picture and change mode
    void TakePhoto()
    {

        photo = new Texture2D(webCamTexture.width, webCamTexture.height);

        photo.SetPixels(webCamTexture.GetPixels());

        photo.Apply();


        /*
        //Encode to a PNG
        byte[] bytes = photo.EncodeToPNG();
        //Write out the PNG. Of course you have to substitute your_path for something sensible
        File.WriteAllBytes(your_path + "photo.png", bytes);
        */
        DisplayPhoto();
    }

    //Display Picture
    void DisplayPhoto()
    {
        m_RendererRendu.material.mainTexture = photo;

    }

    void ChangeMode()
    {
        if (m_PhotoMode)
        {
            TakePhoto();
        }


        m_PhotoMode = !m_PhotoMode;

        m_RendererPhoto.gameObject.SetActive(m_PhotoMode);
        m_RendererRendu.gameObject.SetActive(!m_PhotoMode);

    }

   public void TouchOk()
	{
		if (m_PhotoMode) {
			SoundManagerEvent.emit(SoundManagerType.PhotoClick);
			ChangeMode();
		} else {
			Application.LoadLevel (0);
		}
	}

	public void TouchNo()
	{
		if (m_PhotoMode) {
			Application.LoadLevel (0);
		} else {
			ChangeMode();
		}
	}
   
   
}