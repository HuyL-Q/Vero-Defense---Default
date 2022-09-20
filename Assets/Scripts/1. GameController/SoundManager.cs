using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider BGMslider;
    AudioSource mixer;
    float soundIndex;
    GameObject SettingMenu;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        mixer = GameObject.Find("AudioManager").GetComponent<AudioSource>();
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
        SettingMenu = GameController.FindInActiveObjectByName("Setting Canvas");
        DontDestroyOnLoad(SettingMenu);
    }
    public void TurnOffSound()
    {
        if (BGMslider.value == 0)
            BGMslider.value = soundIndex;
        else
        {
            soundIndex = BGMslider.value;
            BGMslider.value = 0;
        }//code??
    }
    void ChangeBGMVol()
    {

        AudioListener.volume = BGMslider.value;
        Save();
    }
    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", BGMslider.value);
    }
    private void Load()
    {
        BGMslider.value = PlayerPrefs.GetFloat("musicVolume");
    }
    // Update is called once per frame
    void Update()
    {
        ChangeBGMVol();
    }
    public void CloseSetting()
    {
        SettingMenu.SetActive(false);
    }
    public void OpenSetting()
    {
        SettingMenu.SetActive(true);
    }
}