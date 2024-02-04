using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSignals
{
	public static readonly Signal TEST = new("Test");

	public static readonly Signal UpdatePlayerHealth = new("UpdatePlayerHealth");

    public static readonly Signal PlayerDeath = new("PlayerDeath");
}
