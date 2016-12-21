using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadScene : MonoBehaviour {

    public string sceneName = "";

    void OnTriggerStay2D(Collider2D target)
    {
        if(target.gameObject.tag == "Player")
        {
            SceneManager.LoadSceneAsync(sceneName);
        }
    }
}
