using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnterRoom : MonoBehaviour
{
    internal Room RoomScript;

    private void Awake()
    {
        RoomScript = transform.parent.transform.parent.gameObject.GetComponent<Room>(); ;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player") 
        {
            RoomScript.StartRoom();
        }
    }
}
