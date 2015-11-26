using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionScript : MonoBehaviour {
    GameObject optionPanel;
    static OptionScript mInst;
    static public OptionScript instance { get { return mInst; } }
    void Awake()
    {
        if (mInst == null) mInst = this;
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        Debug.Log("start");
        optionPanel = GameObject.Find("OptionPanel");
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
    }

    public void back()
    {
        optionPanel.SetActive(false);
    }

}
