using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RespawnMenu : MonoBehaviour {

    public GameObject RespawnUI; //used to enable and disable the RespawnUI canvas

    //public bool activateRespawnMenu = false;
    public GameObject checkpointController;

    void Start()
    {
        RespawnUI.SetActive(false); //At start of game, UI is disabled
        checkpointController = GameObject.FindWithTag("CheckpointController");
    }

    void Update()
    {
        if (RespawnUI.activeInHierarchy)
        {
            Cursor.visible = true;
            Time.timeScale = System.Convert.ToSingle(0.2); // timeScale sets the time to 0 so nothing happens (pauses)

        }
    }
    
    /// <summary>
    /// Respawn Menu Functions
    /// </summary>

    public void Respawn()
    {
        RespawnUI.SetActive(false);
        checkpointController.GetComponent<CheckpointController>().Respawn();
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
