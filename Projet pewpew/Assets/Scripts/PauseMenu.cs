using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    public KeysConfig cfg;

    private Weapon weapon;
    private PlayerController playercontroller;

    public GameObject gameUI;
    public GameObject menuUI;
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;
    public GameObject videoMenuUI;
    public GameObject keyMenuUI;
    
    private KeyCode newKey;
    private bool waitKey;

    public AudioMixer audiomixer;

    public Dropdown resolutionDropdown;

    Resolution[] resolutions;

    private static bool GameIsPaused = false;

    void Start()
    {
        gameUI.SetActive(true);
        menuUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(false);
        videoMenuUI.SetActive(false);
        keyMenuUI.SetActive(false);

        weapon = GameObject.FindWithTag("weapon").GetComponent<Weapon>();
        playercontroller = GameObject.Find("Player").GetComponent<PlayerController>();

        waitKey = false;
        /*
        for (int i = 0; i < transform.Find("KeyBindingContent").childCount; i++)
        {
            if (i == 0)
                transform.Find("KeyBindingContent").GetChild(i).GetComponentsInChildren<Text>()[1].text = KeysConfig.GM.Forward.ToString();
            if (i == 1)
                transform.Find("KeyBindingContent").GetChild(i).GetComponentsInChildren<Text>()[1].text = KeysConfig.GM.Backward.ToString();
            if (i == 2)
                transform.Find("KeyBindingContent").GetChild(i).GetComponentsInChildren<Text>()[1].text = KeysConfig.GM.Left.ToString();
            if (i == 3)
                transform.Find("KeyBindingContent").GetChild(i).GetComponentsInChildren<Text>()[1].text = KeysConfig.GM.Right.ToString();
            if (i == 4)
                transform.Find("KeyBindingContent").GetChild(i).GetComponentsInChildren<Text>()[1].text = KeysConfig.GM.Jump.ToString();
            if (i == 5)
                transform.Find("KeyBindingContent").GetChild(i).GetComponentsInChildren<Text>()[1].text = KeysConfig.GM.Sprint.ToString();
            if (i == 6)
                transform.Find("KeyBindingContent").GetChild(i).GetComponentsInChildren<Text>()[1].text = KeysConfig.GM.Crouch.ToString();
            if (i == 7)
                transform.Find("KeyBindingContent").GetChild(i).GetComponentsInChildren<Text>()[1].text = KeysConfig.GM.WpnFire.ToString();
            if (i == 8)
                transform.Find("KeyBindingContent").GetChild(i).GetComponentsInChildren<Text>()[1].text = KeysConfig.GM.WpnAim.ToString();
            if (i == 9)
                transform.Find("KeyBindingContent").GetChild(i).GetComponentsInChildren<Text>()[1].text = KeysConfig.GM.WpnReload.ToString();
        }
        */

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " @ " + resolutions[i].refreshRate;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void StartAssignment(string keyName)
    {
        if (!waitKey)
            StartCoroutine(KeyAssignment(keyName));
    }

    public IEnumerator KeyAssignment(string keyName)
    {
        waitKey = true;

        yield return StartCoroutine(WaitForKey());

        switch (keyName)
        {
            case "forward":
                KeysConfig.GM.Forward = newKey;
                GameObject.FindGameObjectsWithTag("KeyBinding")[0].GetComponentsInChildren<Text>()[1].text = KeysConfig.GM.Forward.ToString();
                PlayerPrefs.SetString("forwardKey", KeysConfig.GM.Forward.ToString());
                break;

            case "backward":
                KeysConfig.GM.Backward = newKey;
                GameObject.FindGameObjectsWithTag("KeyBinding")[1].GetComponentsInChildren<Text>()[1].text = KeysConfig.GM.Backward.ToString();
                PlayerPrefs.SetString("backwardKey", KeysConfig.GM.Backward.ToString());
                break;
            case "left":
                KeysConfig.GM.Left = newKey;
                GameObject.FindGameObjectsWithTag("KeyBinding")[2].GetComponentsInChildren<Text>()[1].text = KeysConfig.GM.Left.ToString();
                PlayerPrefs.SetString("leftKey", KeysConfig.GM.Left.ToString());
                break;

            case "right":
                KeysConfig.GM.Right = newKey;
                GameObject.FindGameObjectsWithTag("KeyBinding")[3].GetComponentsInChildren<Text>()[1].text = KeysConfig.GM.Right.ToString();
                PlayerPrefs.SetString("rightKey", KeysConfig.GM.Right.ToString());
                break;
            case "jump":
                KeysConfig.GM.Jump = newKey;
                GameObject.FindGameObjectsWithTag("KeyBinding")[4].GetComponentsInChildren<Text>()[1].text = KeysConfig.GM.Jump.ToString();
                PlayerPrefs.SetString("jumpKey", KeysConfig.GM.Jump.ToString());
                break;

            case "sprint":
                KeysConfig.GM.Sprint = newKey;
                GameObject.FindGameObjectsWithTag("KeyBinding")[5].GetComponentsInChildren<Text>()[1].text = KeysConfig.GM.Sprint.ToString();
                PlayerPrefs.SetString("sprintKey", KeysConfig.GM.Sprint.ToString());
                break;
            case "crouch":
                KeysConfig.GM.Crouch = newKey;
                GameObject.FindGameObjectsWithTag("KeyBinding")[6].GetComponentsInChildren<Text>()[1].text = KeysConfig.GM.Crouch.ToString();
                PlayerPrefs.SetString("crouchKey", KeysConfig.GM.Crouch.ToString());
                break;

            case "fire":
                KeysConfig.GM.WpnFire = newKey;
                GameObject.FindGameObjectsWithTag("KeyBinding")[7].GetComponentsInChildren<Text>()[1].text = KeysConfig.GM.WpnFire.ToString();
                PlayerPrefs.SetString("wpnFireKey", KeysConfig.GM.WpnFire.ToString());
                break;
            case "aim":
                KeysConfig.GM.WpnAim = newKey;
                GameObject.FindGameObjectsWithTag("KeyBinding")[8].GetComponentsInChildren<Text>()[1].text = KeysConfig.GM.WpnAim.ToString();
                PlayerPrefs.SetString("wpnAimKey", KeysConfig.GM.WpnAim.ToString());
                break;

            case "reload":
                KeysConfig.GM.WpnReload = newKey;
                GameObject.FindGameObjectsWithTag("KeyBinding")[9].GetComponentsInChildren<Text>()[1].text = KeysConfig.GM.WpnReload.ToString();
                PlayerPrefs.SetString("wpnReloadKey", KeysConfig.GM.WpnReload.ToString());
                break;
        }
        yield return null;
    }

    public IEnumerator WaitForKey()
    {
        while (!Input.anyKey)
            yield return null;

        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode))
            {
                newKey = kcode;
                waitKey = false;
            }
        }
    }

    public void Resume()
    {
        menuUI.SetActive(false);
        GameIsPaused = false;
        weapon.canFire = true;
        playercontroller.canMove = true;
    }

    public void Pause()
    {
        menuUI.SetActive(true);
        pauseMenuUI.SetActive(true);
        GameIsPaused = true;
        weapon.canFire = false;
        playercontroller.canMove = false;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }

    public void Settings()
    {
        settingsMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
    }

    public void VideoSettings()
    {
        videoMenuUI.SetActive(true);
        keyMenuUI.SetActive(false);
    }

    public void KeySettings()
    {
        keyMenuUI.SetActive(true);
        videoMenuUI.SetActive(false);
    }

    public void SettingsReturn()
    {
        pauseMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);
        videoMenuUI.SetActive(false);
        keyMenuUI.SetActive(false);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float volume)
    {
        Debug.Log(volume);
        audiomixer.SetFloat("volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        Debug.Log(qualityIndex);
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}