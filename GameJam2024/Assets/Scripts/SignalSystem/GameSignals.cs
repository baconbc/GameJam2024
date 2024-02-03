using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IslandBoy
{
	public static class GameSignals
	{
		// SunMeter Signals
		public static readonly Signal DAY_START = new("DayStart");
		public static readonly Signal DAY_END = new("DayEnd");
		public static readonly Signal BED_TIME_EXECUTED = new("BedTimeExecuted");

		// Player Signals
		public static readonly Signal INVENTORY_OPEN = new("InventoryOpen");
		public static readonly Signal INVENTORY_CLOSE = new("InventoryClose");
		public static readonly Signal HOTBAR_SLOT_UPDATED = new("HotbarSlotUpdated");
		public static readonly Signal SLOT_CLICKED = new("SlotClicked");
		public static readonly Signal ITEM_ADDED = new("ItemAdded");
		public static readonly Signal ITEM_CRAFTED = new("ItemCrafted");
		public static readonly Signal CLICKABLE_CLICKED = new("SwingPerformed");
		public static readonly Signal PLAYER_DIED = new("PlayerDied");
		public static readonly Signal PLAYER_HP_CHANGED = new("PlayerHpChanged");
		public static readonly Signal PLAYER_NRG_CHANGED = new("PlayerNrgChanged");
		public static readonly Signal PLAYER_MP_CHANGED = new("PlayerMpChanged");
		public static readonly Signal PLAYER_DAMAGED = new("PlayerDamaged");
		public static readonly Signal ITEM_CHARGING = new("ItemCharging");
		public static readonly Signal MOUSE_SLOT_HAS_ITEM = new("MouseSlotHasItem");
		public static readonly Signal MOUSE_SLOT_GIVES_ITEM = new("MouseSlotGivesItem");
		public static readonly Signal FOCUS_SLOT_UPDATED = new("FocusSlotUpdated");
		public static readonly Signal CURSOR_ENTERED_NEW_TILE = new("CursorEnteredNewTile");
		public static readonly Signal ITEM_DEPLOYED = new("ItemDeployed");
		public static readonly Signal ENERGY_RESTORED = new("EnergyRestored");
		
		// Interact Signals
		public static readonly Signal DISPLAY_PROMPT = new("DisplayPrompt");
		public static readonly Signal DISPLAY_INTERACTABLE = new("DisplayInteractable");
		public static readonly Signal CRAFT_STATION_INTERACT = new("CraftStationInteract");
		

		// Pause Signals
		public static readonly Signal GAME_PAUSED = new("GamePaused");
		public static readonly Signal GAME_UNPAUSED = new("GameUnpaused");

		// World Signals
		public static readonly Signal CHANGE_SCENE = new("ChangeScene");
		public static readonly Signal RESIDENT_UPDATE = new("ResidentUpdate");
		public static readonly Signal MONSTER_KILLED = new("MonsterKilled");
		public static readonly Signal MONSTER_HEART_CLEARED = new("MonsterHeartCleared");
		public static readonly Signal CONFINER_UPDATED = new("ConfinerUpdated");

		// Shift-click Signals
		public static readonly Signal ADD_ITEMS_TO_CHEST = new("AddItemsToChest"); // BROOKE
		public static readonly Signal ADD_ITEM_TO_INVENTORY_FROM_CHEST = new("AddItemToInventoryFromChest"); // BROOKE

		// Fishing Signals
		public static readonly Signal FISHING_MINIGAME_END = new("FishingMinigameEnd");
	}
}
