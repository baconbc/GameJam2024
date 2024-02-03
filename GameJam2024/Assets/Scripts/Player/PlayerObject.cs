using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Reference", menuName = "New Reference/Player Reference")]
public class PlayerObject : ScriptableObject
{
	private Vector2 spawnPoint;
	private Vector2 playerPos;
	private Vector2 mousePos;
	private GameObject playerGameObject;
	private int health;

	public Vector2 Position { get { return playerPos; } set { playerPos = value; } } // setting this value does not change the player's actual position
	public Vector2 MousePos { get { return mousePos; } set { mousePos = value; } }
	public Vector2 SpawnPoint { get { return spawnPoint; } set { spawnPoint = value; } }
	public GameObject GameObject { get { return playerGameObject; } set { playerGameObject = value; } }
	public int Health { get { return health; } set { health = value; } }
}
