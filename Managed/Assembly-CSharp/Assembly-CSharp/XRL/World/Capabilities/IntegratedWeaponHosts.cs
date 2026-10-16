using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using XRL.UI;
using XRL.Wish;
using XRL.World.AI;
using XRL.World.Parts;

namespace XRL.World.Capabilities
{
	// Token: 0x020009EB RID: 2539
	[HasWishCommand]
	public static class IntegratedWeaponHosts
	{
		// Token: 0x06006C5C RID: 27740 RVA: 0x00230F58 File Offset: 0x0022F158
		public static GameObject GenerateTurret(GameObject weapon, GameObject owner = null, bool overrideSupply = false)
		{
			GameObjectBlueprint gameObjectBlueprint = IntegratedWeaponHosts.FindTurretBlueprint(weapon);
			weapon.RemoveFromContext(null);
			int num = Tier.Constrain((weapon != null) ? weapon.GetTier() : 3);
			GameObject gameObject;
			if (gameObjectBlueprint != null)
			{
				List<GameObject> list = new List<GameObject> { weapon };
				gameObject = GameObject.Create(gameObjectBlueprint, 0, 0, null, null, null, "DeployTurret", list);
				if (list.Count > 0)
				{
					weapon.Obliterate(null, false, null);
					weapon = null;
				}
				gameObject.RemoveIntProperty("TurretWarmup");
			}
			else
			{
				gameObject = GameObject.Create("TinkerTurret", 0, 0, null, null, null, "DeployTurret", null);
				gameObject.Inventory.AddObject(weapon, null, false, false, true, null);
				gameObject.DisplayName = IntegratedWeaponHosts.GetTurretNameFromWeapon(weapon);
				gameObject.Render.Tile = IntegratedWeaponHosts.GetTurretTileFromWeapon(weapon);
				gameObject.Render.RenderString = num.ToString();
				gameObject.Render.SetForegroundColor(weapon.Render.GetForegroundColor());
				string propertyOrTag = weapon.GetPropertyOrTag("IntegratedWeaponHostDetailColor", null);
				if (!string.IsNullOrEmpty(propertyOrTag) && propertyOrTag.Length == 1)
				{
					gameObject.Render.DetailColor = propertyOrTag;
				}
				gameObject.GetStat("Hitpoints").BaseValue = num * 5;
				gameObject.GetStat("AV").BaseValue = num * 3 / 2;
				if (weapon.IsGiganticEquipment)
				{
					gameObject.IsGiganticCreature = true;
				}
			}
			PowerSwitch part = gameObject.GetPart<PowerSwitch>();
			if (part != null)
			{
				if (owner != null && owner.IsPlayer())
				{
					part.SecurityClearance = 0;
				}
				else
				{
					part.SecurityClearance = -Math.Min(Math.Max(num, 1), 5);
				}
			}
			gameObject.RemovePart<RandomLoot>();
			if (weapon != null)
			{
				weapon.FireEvent(Event.New("PrepIntegratedHostToReceiveAmmo", "Host", gameObject));
			}
			else
			{
				gameObject.FireEventOnBodyparts(Event.New("PrepIntegratedHostToReceiveAmmo", "Host", gameObject));
			}
			gameObject.GetStat("XPValue").BaseValue = 0;
			if (owner != null)
			{
				int num2 = owner.Stat("Level", 0);
				if (num2 > gameObject.Stat("Level", 0))
				{
					gameObject.GetStat("Level").BaseValue = num2;
					gameObject.GetStat("XP").BaseValue = Leveler.GetXPForLevel(num2);
				}
				if (gameObject.HasStat("Agility"))
				{
					gameObject.GetStat("Agility").BaseValue = IntegratedWeaponHosts.GetTurretAgilityByDeployer(owner);
				}
				gameObject.SetAlliedLeader<AllyConstructed>(owner, 0, false);
			}
			gameObject.Brain.PerformEquip(true, true, null);
			if (owner != null)
			{
				Event @event = Event.New("SupplyIntegratedHostWithAmmo", "Host", gameObject, "Owner", owner);
				if (owner.IsPlayer() && !overrideSupply)
				{
					@event.SetParameter("TrackSupply", 1);
				}
				if (weapon != null)
				{
					weapon.FireEvent(@event);
				}
				else
				{
					gameObject.FireEventOnBodyparts(@event);
				}
				if (owner.IsPlayer() && !overrideSupply && @event.HasFlag("AnySupplyHandler") && !@event.HasFlag("AnySupplies"))
				{
					Popup.Show(string.Concat(new string[] { "You have no ammunition to supply ", gameObject.the, gameObject.DisplayNameOnly, " with. ", gameObject.it, " may be ineffective unless stocked." }), null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
			}
			return gameObject;
		}

		// Token: 0x06006C5D RID: 27741 RVA: 0x00231268 File Offset: 0x0022F468
		public static GameObjectBlueprint FindTurretBlueprint(GameObject weapon)
		{
			foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.BlueprintList)
			{
				if (IntegratedWeaponHosts.IsBlueprintMatchingTurret(gameObjectBlueprint, weapon))
				{
					return gameObjectBlueprint;
				}
			}
			return null;
		}

		// Token: 0x06006C5E RID: 27742 RVA: 0x002312C8 File Offset: 0x0022F4C8
		private static bool IsBlueprintMatchingTurret(GameObjectBlueprint BP, GameObject weapon)
		{
			if (!BP.DescendsFrom("TinkerTurret"))
			{
				return false;
			}
			if (BP.Inventory == null)
			{
				return false;
			}
			if (BP.HasTag("WeaponEmplacement", false))
			{
				return false;
			}
			bool flag = false;
			foreach (InventoryObject inventoryObject in BP.Inventory)
			{
				if (inventoryObject.Blueprint == weapon.Blueprint)
				{
					if (flag)
					{
						return false;
					}
					flag = true;
				}
				else
				{
					GameObjectBlueprint gameObjectBlueprint;
					if (!GameObjectFactory.Factory.Blueprints.TryGetValue(inventoryObject.Blueprint, out gameObjectBlueprint))
					{
						return false;
					}
					if (gameObjectBlueprint.HasPart("MissileWeapon"))
					{
						return false;
					}
				}
			}
			return flag;
		}

		// Token: 0x06006C5F RID: 27743 RVA: 0x00231390 File Offset: 0x0022F590
		public static string GetTurretNameFromWeapon(GameObject weapon)
		{
			string tagOrStringProperty = weapon.GetTagOrStringProperty("TurretName", null);
			if (!string.IsNullOrEmpty(tagOrStringProperty))
			{
				return tagOrStringProperty;
			}
			string text = IntegratedWeaponHosts.GenericTurretNameTweaks(weapon.Render.DisplayName);
			if (!text.Contains("turret"))
			{
				text += " turret";
			}
			return text;
		}

		// Token: 0x06006C60 RID: 27744 RVA: 0x002313E0 File Offset: 0x0022F5E0
		private static string GenericTurretNameTweaks(string Name)
		{
			return Name.Replace("rifle", "turret").Replace(" gun", "").Replace(" pistol", "")
				.Replace(" pump", "")
				.Replace(" tube", "")
				.Replace(" rack", "");
		}

		// Token: 0x06006C61 RID: 27745 RVA: 0x00231448 File Offset: 0x0022F648
		public static string GetTurretTileFromWeapon(GameObject weapon)
		{
			MissileWeapon part = weapon.GetPart<MissileWeapon>();
			if (weapon.HasTagOrProperty("Bow"))
			{
				return "Creatures/bow-turret.png";
			}
			if (weapon.HasPart<LiquidAmmoLoader>())
			{
				return "Creatures/sw_flamethrower_turret.png";
			}
			if (weapon.HasPart<EnergyAmmoLoader>())
			{
				if (part != null && part.ShotsPerAction > 1)
				{
					return "Creatures/sw_chainlaser_emplacement.bmp";
				}
				return "Creatures/sw_laser_turret.bmp";
			}
			else
			{
				if (part != null && part.Skill == "HeavyWeapon" && part.ShotsPerAction == 1)
				{
					return "Creatures/sw_rocket_turret.bmp";
				}
				if (part != null && part.ShotsPerAction > 1)
				{
					return "Creatures/sw_chaingun_turret.bmp";
				}
				return "Creatures/sw_rifle_turret.bmp";
			}
		}

		// Token: 0x06006C62 RID: 27746 RVA: 0x002314D8 File Offset: 0x0022F6D8
		public static int GetTurretAgilityByDeployer(GameObject who)
		{
			if (who.HasSkill("Tinkering_Tinker3"))
			{
				return 34;
			}
			if (who.HasSkill("Tinkering_Tinker2"))
			{
				return 32;
			}
			if (who.HasSkill("Tinkering_Tinker1"))
			{
				return 30;
			}
			return 28;
		}

		// Token: 0x06006C63 RID: 27747 RVA: 0x0023150C File Offset: 0x0022F70C
		[WishCommand(null, null, Regex = "^turret:\\s*(.*?)\\s*$")]
		public static bool HandleTurretWish(Match match)
		{
			string value = match.Groups[1].Value;
			try
			{
				GameObject gameObject = IntegratedWeaponHosts.GenerateTurret(GameObject.Create(value), null, false);
				(The.Player.CurrentCell.GetFirstEmptyAdjacentCell() ?? The.Player.CurrentCell).AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				gameObject.MakeActive(false);
			}
			catch (Exception ex)
			{
				Popup.ShowFail("Could not generate turret from blueprint \"" + value + "\"\n\n" + ex.ToString(), true, true, true);
			}
			return true;
		}

		// Token: 0x0400319C RID: 12700
		[NonSerialized]
		public const string DeployTurretContext = "DeployTurret";
	}
}
