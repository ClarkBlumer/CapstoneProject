using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject PauseUI; //used to enable and disable the pauseUI canvas

    private bool paused = false;

    void Start()
    {
        PauseUI.SetActive(false); //At start of game, UI is disabled
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            paused = !paused; //toggles the bool to opposite state

        }
        if (paused)
        {
            PauseUI.SetActive(true);
            Time.timeScale = 0; // timeScale sets the time to 0 so nothing happens (pauses)
        }

        if (!paused)
        {
            PauseUI.SetActive(false);
            Time.timeScale = 1; //Sets it back to normal time. less than 1 creates a slow mo effect
        }
    }

    /// <summary>
    /// Pause Menu Functions
    /// </summary>

    public void Resume()
    {
        paused = false;
    }

    public void Restart()
    {
        //Application.LoadLevel(Application.loadedLevel); *obsolete apparently* thanks VS + Unity
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Save()
    {
        GameControl.control.Save();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        
    }

    public void Quit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit(); 
        #endif
    }
}
