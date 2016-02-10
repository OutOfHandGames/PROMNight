using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections;

public class PauseMenuManager : MonoBehaviour {
    public GameObject pauseMenuScreen;
    public GameObject settingMenuScreen;
    public string mainMenuScene = "MainMenu";
    public AudioMixer aMixer;
    public Text musicOnText;
    public Text sfxOnText;
    bool isPaused;
    bool settingsEnabled;

    bool musicOn;
    bool sfxOn;
    GameManager gameManager;

    void Start()
    {
        musicOn = true;
        sfxOn = true;
        isPaused = false;
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            runPauseMenu();
        }
    }

    public void runPauseMenu()
    {
        isPaused = !isPaused;

        pauseMenuScreen.SetActive(isPaused);

        MonoBehaviour[] components = gameManager.GetComponents<MonoBehaviour>();
        Entity[] allEntities = GameObject.FindObjectsOfType<Entity>();
        foreach (Entity e in allEntities)
        {
            e.GetComponentInChildren<Animator>().enabled = !isPaused;
        }
        foreach(MonoBehaviour m in components)
        {
            m.enabled = !isPaused;
        }
    }

    /// <summary>
    /// Settings Scrtips go here
    /// </summary>
    public void OnSettingsClicked()
    {
        runSettings();
    }

    void runSettings()
    {
        settingsEnabled = !settingsEnabled;
        pauseMenuScreen.SetActive(!settingsEnabled);
        settingMenuScreen.SetActive(settingsEnabled);

    }

    public void OnSoundClicked()
    {
        musicOn = !musicOn;
        float f = -80;
        if (musicOn)
        {
            aMixer.ClearFloat("MusicVolume");
            musicOnText.text = "Music: On";
        }
        else
        {
            aMixer.SetFloat("MusicVolume", f);
            musicOnText.text = "Music: Off";

        }

    }

    public void OnSoundFXClicked()
    {
        sfxOn = !sfxOn;
        if (sfxOn)
        {
            aMixer.ClearFloat("SFXVolume");
            sfxOnText.text = "SoundFX: On";
            
        }
        else
        {
            aMixer.SetFloat("SFXVolume", -80);
            sfxOnText.text = "SoundFX: Off";
        }
    }

    public void OnClickMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    public void OnClickQuitGame()
    {
        Application.Quit();
    }
}
