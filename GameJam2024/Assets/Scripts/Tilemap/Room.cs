using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] List<GameObject> doors = new();

    private GameObject fog;
    private GameObject triggers;

    private AudioSource doorSound;


    private bool EnteredRoom = false;

    void Awake()
    {
        fog = transform.GetChild(0).gameObject;
        fog.SetActive(true);
        triggers = transform.GetChild(1).gameObject;


        doorSound = GetComponent<AudioSource>();
    }

    public void StartRoom()
    {
        CloseDoors();
        DisableFog();
        DisableTriggers();
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
