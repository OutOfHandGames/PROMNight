using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenuManager : MonoBehaviour {
    public GameObject pauseMenuScreen;
    public GameObject settingMenuScreen;
    public string mainMenuScene = "MainMenu";
    bool isPaused;
    bool settingsEnabled;
    GameManager gameManager;

    void Start()
    {
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
