using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnterRoom : MonoBehaviour
{

    private GameObject fog;


    void Awake()
    {
        fog = transform.parent.transform.GetChild(0).gameObject;
        fog.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player") 
        {
            if (fog.activeSelf)
            {
                fog.SetActive(false);
                print("Set!");
            }
            else
            {
                print("Unset!");
                fog.SetActive(true);
            }
            
            
        }
    }
}
