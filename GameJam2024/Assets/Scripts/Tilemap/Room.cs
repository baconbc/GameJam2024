using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private List<GameObject> doors = new();

    private List<GameObject> livingEnemies = new();
    [SerializeField] private List<SpawnInstruction> instructions = new();
    // list of class objects

    private GameObject fog;
    private GameObject triggers;

    private AudioSource doorSound;

    private bool RoomActive = false;
    private bool CompletedRoom = false;

    void Awake()
    {
        fog = transform.GetChild(0).gameObject;
        fog.SetActive(true);
        triggers = transform.GetChild(1).gameObject;


        doorSound = GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        if (!CompletedRoom && RoomActive && livingEnemies.Count > 0)
        {
            for (int i = 0; i < livingEnemies.Count; i++)
            {
                if (livingEnemies[i] == null)
                {
                    livingEnemies.RemoveAt(i);
                    i--;
                }
            }
        }
        if(RoomActive && !CompletedRoom && livingEnemies.Count == 0)
        {
            BeatRoom();
            print("beat!");
        }
    }
    public void StartRoom()
    {
        RoomActive = true;
        CloseDoors();
        DisableFog();
        DisableTriggers();
        SpawnAllEnemies();
        print("started");
    }
    public void BeatRoom()
    {
        OpenDoors();
        CompletedRoom = true;
    }

    public void UnloadRoom()
    {
        EnableFog();
        RoomActive = false;
    }


    public void SpawnEnemy(Vector2 location, GameObject enemyType)
    {
        location += (Vector2)transform.position;
        livingEnemies.Add(Instantiate(enemyType, location, Quaternion.Euler(0, 0, 0)));
    }

    public void SpawnEnemyGroup(SpawnInstruction instruction)
    {
        foreach (Vector2 location in instruction.spawnLocations) 
        {
            SpawnEnemy(location, instruction.enemyType);
        }
    }

    public void SpawnAllEnemies()
    {
        foreach (SpawnInstruction instruction in instructions)
        {
            SpawnEnemyGroup(instruction);
        }
    }

    public void DisableTriggers()
    {
        triggers.SetActive(false);
    }

    public void EnableTriggers()
    {
        triggers.SetActive(true);
    }

    public void CloseDoors()
    {
        doorSound.Play();
        foreach (GameObject door in doors)
        {
            door.GetComponent<Door>().CloseDoor();
        }
    }

    public void OpenDoors()
    {
        doorSound.Play();
        foreach (GameObject door in doors)
        {
            door.GetComponent<Door>().OpenDoor();
        }
    }

    public void DisableFog()
    {
        fog.SetActive(false);
    }

    public void EnableFog()
    {
        fog.SetActive(true);
    }
}
