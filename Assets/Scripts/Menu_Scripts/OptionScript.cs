﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionScript : MonoBehaviour {
    public GameObject optionPanel;
    static OptionScript mInst;
    bool isInGame = false;
    static public OptionScript instance { get { return mInst; } }

    void Awake()
    {
        if (mInst == null) mInst = this;
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        Debug.Log("start");
        if (optionPanel==null)
        {
            optionPanel = GameObject.Find("OptionPanel");
            
        }
        optionPanel.SetActive(false);
    }

    public Slider musicSlider;
    public Slider soundsEffectsSlider;
    public Toggle muteAll;

    

    public void MuteAll()
    {
        //Mute tous les sons
        if (muteAll.isOn)
        {
            //unMute
            Debug.Log("UnMute");
        }
        else
        {
            //mute
            Debug.Log("Mute");
        }
        
    }

    public void ChangeMusicVolume(){
        //Modifie le volume de la musique
        Debug.Log("Music :" +musicSlider.value);
        float value = musicSlider.value * 100;
        musicSlider.GetComponentInChildren<Text>().text = "Music : " + value.ToString();
        
    }

    public void ChangeSoudsVolume(){
        //Modifie le volume des effets
        Debug.Log("Sounds Effetcs :" + soundsEffectsSlider.value);
        float value = soundsEffectsSlider.value * 100;
        soundsEffectsSlider.GetComponentInChildren<Text>().text = "Sounds Effetcs : " + value.ToString();
    }

    public void ChangeScene()
    {
        Application.LoadLevelAsync(Application.loadedLevel - 1);
    }

    public void Quit()
    {
        Debug.Log("quit");
        Application.Quit();
    }

    public void activeOptionMenu()
    {
        optionPanel.SetActive(true);
    }

    public void retry()
    {
        Application.LoadLevel(Application.loadedLevel);
        GameManagerScript.instance.resetGame();
    }

    public void back()
    {
        optionPanel.SetActive(false);
    }

    public void setActiveOptionInGame(bool setValue)
    {
        optionPanel.SetActive(setValue);                    // active menu option
        GameManagerScript.instance.setPauseGame(setValue); // pause le jeu
    }

}
