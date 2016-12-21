using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {

    public static GameControl control;
    public Health playerHealth; //gets the Health component of player
    public Transform player; //gets the Rect Transform component of player
    //public GameObject player;

    /// <summary>
    /// Highlander - There can be only one! Will make a GameControl object if one does not already exist. If one is present, will destroy any new ones
    /// </summary>
    void Awake()
    {
        // if there isn't a GameControl object already, will make the one and only one
        if (control == null)
        {
            DontDestroyOnLoad(gameObject); //GameControl object will persist through scenes if we go that route
            control = this;
        }
        else if(control != this)
        {
            //if there is another GameControl
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //GameObject  player = GameObject.FindGameObjectWithTag("Player");
        //playerPosition 
    }

    public void Save()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat"); //unity has a persistent path that it saves to
        Debug.Log("File saved to: " + Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData();

        //Getting data to save
        //data.player = GameObject.FindGameObjectWithTag("Player");
        data.sceneID = SceneManager.GetActiveScene().buildIndex;
        data.health = playerHealth.currentHealth;
        data.playerPositionX = player.position.x;
        data.playerPositionY = player.position.y;
        data.playerPositionZ = player.position.z;
        Debug.Log("Saved player health value: " + data.health);

        //Makes the magic happen...
        binaryFormatter.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = binaryFormatter.Deserialize(file) as PlayerData;
            file.Close();

            SceneManager.LoadScene(data.sceneID);

            
            Instantiate(player);

            playerHealth = player.GetComponent<Health>();
            playerHealth.currentHealth = data.health;
            Debug.Log("Player health: " + playerHealth.currentHealth);
            playerHealth.bar.GetComponent<BarScript>().UpdateValue(playerHealth.currentHealth);

            player.position = new Vector3(data.playerPositionX, data.playerPositionY, data.playerPositionZ);
            
        }
    }

}

//This class will be what gets serialized to fit into the binary format
[Serializable]
class PlayerData
{
    //Keep track of which scene the player was on
    public int sceneID;

    //public GameObject player;

    //Keep track of player's health at save
    public float health;

    //Cannot serialize a Transform or Vectors, so have to take their individual parts
    public float playerPositionX;
    public float playerPositionY;
    public float playerPositionZ;
    
}
