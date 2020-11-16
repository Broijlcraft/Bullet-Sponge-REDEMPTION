using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    #region Singleton
    public static MenuManager single;
    private void Awake()
    {
        if(single == null)
        {
            single = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
    public GameObject titleScreen;
    public GameObject mainMenuScreen;
    public GameObject pauseScreen;

    public Text[] Titeles;
    public GameObject[] SettingTabs;

    public AudioManager audioManager;
    bool paused;

    private void Start()
    {
        audioManager = AudioManager.single;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            paused = !paused;
        }
        if (paused)
        {
            PauseGame();
        }
        else if(!mainMenuScreen.activeSelf && !titleScreen.activeSelf)
        {
            UnPauseGame();
        }
    }

    public void TurnOffAllTitles()
    {
        foreach (Text title in Titeles)
        {
            title.gameObject.SetActive(false);
        }
    }

    public void TurnOffAllSettingTabs()
    {
        foreach (GameObject settingTab in SettingTabs)
        {
            settingTab.SetActive(false);
        }
    }

    #region MainMenu
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion

    #region PauseMenu
    public void PauseGame()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Time.timeScale = 0f;
        pauseScreen.SetActive(true);
    }

    public void UnPauseGame()
    {
        paused = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Time.timeScale = 1f;
        pauseScreen.SetActive(false);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    #endregion

    #region QualitySettings
    string[] QualityNames;
    List<Resolution> res = new List<Resolution>();

    public void SetUpResolutions(Dropdown resolutionDropDown)
    {
        resolutionDropDown.ClearOptions();

        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            if (!res.Contains(Screen.resolutions[i]))
            {
                res.Add(Screen.resolutions[i]);
            }
        }

        List<string> options = new List<string>();

        for (int ia = 0; ia < res.Count; ia++)
        {
            string option = res[ia].width + " x " + res[ia].height;
            if (!options.Contains(option))
            {
                options.Add(option);
            }
        }

        resolutionDropDown.AddOptions(options);

        for (int ib = 0; ib < options.Count; ib++)
        {
            if (options[ib] == Screen.width + " x " + Screen.height)
            {
                resolutionDropDown.value = ib;
            }
        }
    }

    public void SetUpQualitySettings(Dropdown qualitySettings)
    {
        QualityNames = QualitySettings.names;

        qualitySettings.ClearOptions();

        List<string> options = new List<string>();
        for (int i = 0; i < QualityNames.Length; i++)
        {
            if (!options.Contains(QualityNames[i]))
            {
                options.Add(QualityNames[i]);
            }
        }
        qualitySettings.AddOptions(options);

        for (int ib = 0; ib < options.Count; ib++)
        {
            if (ib == QualitySettings.GetQualityLevel())
            {
                qualitySettings.value = ib;
            }
        }
    }

    public void SetFullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = res[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    #endregion

    #region AudioSettings

    //public void GetCurrentMasterVolume(Slider masterSlider)
    //{
    //    string group = audioManager.master.ToString();
    //    masterSlider.value = audioManager.master//PlayerPrefs.GetFloat(group);
    //}

    //public void SetMasterVolume(float value)
    //{
    //    string group = audioManager.master.ToString();
    //    AudioManager.single.audioMixer.SetFloat(group, Mathf.Log10(value) * 20);
    //    PlayerPrefs.SetFloat(group, value);
    //}

    //public void GetCurrentMusicVolume(Slider musicSlider)
    //{
    //    string group = audioManager.music.ToString();
    //    musicSlider.value = PlayerPrefs.GetFloat(group);
    //}

    //public void SetMusicVolume(float value)
    //{
    //    string group = audioManager.music.ToString();
    //    AudioManager.single.audioMixer.SetFloat(group, Mathf.Log10(value) * 20);
    //    PlayerPrefs.SetFloat(group, value);
    //}

    //public void GetCurrentSfxVolume(Slider sfxSlider)
    //{
    //    string group = audioManager.sfx.ToString();
    //    sfxSlider.value = PlayerPrefs.GetFloat(group);
    //}

    //public void SetSfxVolume(float value)
    //{
    //    string group = audioManager.sfx.ToString();
    //    AudioManager.single.audioMixer.SetFloat(group, Mathf.Log10(value) * 20);
    //    PlayerPrefs.SetFloat(group, value);
    //}


    #endregion

    #region KeyBinds

    private bool runningToggle;
    public void SetRunningToggle(bool toggle)
    {
         runningToggle = toggle;
    }

    public bool GetRunningToggle()
    {
        return runningToggle;
    }
    #endregion
}
