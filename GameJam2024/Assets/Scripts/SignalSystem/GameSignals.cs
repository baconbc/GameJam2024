using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSignals
{
	public static readonly Signal TEST = new("Test");

	public static readonly Signal UpdatePlayerHealth = new("UpdatePlayerHealth");

    public static readonly Signal PlayerDeath = new("PlayerDeath");
	public static readonly Signal ResetPlayerHealth = new("ResetPlayerHealth");
	public static readonly Signal RegenPlayerHealth = new("RegenPlayerHealth");

	public static readonly Signal Earth = new("Earth");
	public static readonly Signal Water = new("Water");
	public static readonly Signal Fire = new("Fire");
	public static readonly Signal Wind = new("Wind");
}
