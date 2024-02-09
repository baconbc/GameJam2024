using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Room : MonoBehaviour
{
    [SerializeField] private List<GameObject> doors = new();

    private List<GameObject> livingEnemies = new();
    [SerializeField] private List<SpawnInstruction> instructions = new();
    // list of class objects

    private GameObject fog;
    private GameObject triggers;

    private AudioSource doorSound;

    [SerializeField] private AudioClip openSound;
    private AudioClip closeSound;

    private bool RoomActive = false;
    private bool CompletedRoom = false;

    [SerializeField] bool isFinalRoom = false;
    [SerializeField] bool isWaterRoom = false;
    [SerializeField] bool isFireRoom = false;
    [SerializeField] bool isWindRoom = false;
    [SerializeField] bool isEarthRoom = false;
    private bool water = false;
    private bool fire = false;
    private bool earth = false;
    private bool wind = false;
    private bool openedfinal = false;

    void Awake()
    {
        fog = transform.GetChild(0).gameObject;
        fog.SetActive(true);
        triggers = transform.GetChild(1).gameObject;

        GameSignals.PlayerDeath.AddListener(PlayerDeath);

        doorSound = GetComponent<AudioSource>();

        closeSound = doorSound.clip;

        GameSignals.Fire.AddListener(Fire);
        GameSignals.Earth.AddListener(Earth);
        GameSignals.Water.AddListener(Water);
        GameSignals.Wind.AddListener(Wind);
    }

    private void Start()
    {
        if (isFinalRoom)
        {
            //CloseDoors();
            CloseDoorsNoSound();
        }
    }

    void OnDestroy()
    {
        GameSignals.PlayerDeath.RemoveListener(PlayerDeath);
        GameSignals.Fire.RemoveListener(Fire);
        GameSignals.Earth.RemoveListener(Earth);
        GameSignals.Water.RemoveListener(Water);
        GameSignals.Wind.RemoveListener(Wind);
    }

    private void PlayerDeath(ISignalParameters parameters)
    {
        if (!CompletedRoom && RoomActive)
        {
            //ResetRoom();
            ResetRoomNoSound();
        }
    }

    private void ResetRoom()
    {
        foreach (GameObject enemy in livingEnemies) Destroy(enemy);
        livingEnemies.Clear();
        OpenDoors();
        UnloadRoom();
        EnableTriggers();
    }

    private void ResetRoomNoSound()
    {
        foreach (GameObject enemy in livingEnemies) Destroy(enemy);
        livingEnemies.Clear();
        OpenDoorsNoSound();
        UnloadRoom();
        EnableTriggers();
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

        //if (isFinalRoom && fire && water && earth && wind && !openedfinal) // UNCOMMENT THIS WHEN ALL 4 ROOMS WORK
        if (isFinalRoom && fire && water && !openedfinal)
        {
            openedfinal = true;
            OpenDoors();
            print("A mysterious door has unlocked...");
        }
    }
    public void StartRoom()
    {
        print("bro");
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
        if (isWaterRoom)
        {
            Signal signal = GameSignals.Water;
            signal.Dispatch();
            ResetPlayerHealth();
        }
        else if (isFireRoom)
        {
            Signal signal = GameSignals.Fire;
            signal.Dispatch();
            ResetPlayerHealth();
        }
        if (isEarthRoom)
        {
            Signal signal = GameSignals.Earth;
            signal.Dispatch();
            ResetPlayerHealth();
        }
        else if (isWindRoom)
        {
            Signal signal = GameSignals.Wind;
            signal.Dispatch();
            ResetPlayerHealth();
        }
        else if (isFinalRoom)
        {
            Debug.Log("YOU WIN!!");
            SceneManager.LoadScene("WinScene");
        }
    }

    public void ResetPlayerHealth()
    {
        Signal signal = GameSignals.ResetPlayerHealth;
        signal.Dispatch();
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
        //doorSound.Play();
        AudioManager.Instance.Play("DoorClose");
        foreach (GameObject door in doors)
        {
            door.GetComponent<Door>().CloseDoor();
        }
    }

    public void CloseDoorsNoSound()
    {
        foreach (GameObject door in doors)
        {
            door.GetComponent<Door>().CloseDoor();
        }
    }

    public void OpenDoors()
    {
        //doorSound.clip = openSound;
        //doorSound.Play();
        AudioManager.Instance.Play("DoorOpen");
        foreach (GameObject door in doors)
        {
            door.GetComponent<Door>().OpenDoor();
        }
    }

    public void OpenDoorsNoSound()
    {
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

    private void Water(ISignalParameters parameters)
    {
        water = true;
    }

    private void Earth(ISignalParameters parameters)
    {
        earth = true;
    }

    private void Wind(ISignalParameters parameters)
    {
        wind = true;
    }

    private void Fire(ISignalParameters parameters)
    {
        fire = true;
    }
}
