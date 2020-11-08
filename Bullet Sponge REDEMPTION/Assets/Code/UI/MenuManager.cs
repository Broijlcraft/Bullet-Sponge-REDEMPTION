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
