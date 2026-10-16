using System;
using XRL.Wish;
using XRL.World.Parts;
using XRL.World.ZoneParts;

namespace XRL.World.Quests
{
	// Token: 0x02000B24 RID: 2852
	[HasWishCommand]
	public static class GritGateScripts
	{
		// Token: 0x060072A1 RID: 29345 RVA: 0x002B672A File Offset: 0x002B492A
		public static void PromoteToApprentice()
		{
			The.Game.PlayerReputation.PromoteIfBelow(GritGateScripts.FACTION, "apprentice", true, true);
			GritGateScripts.OpenRank1Doors();
			GritGateScripts.IdentifyByTag("GritGateApprenticeIdentify");
		}

		// Token: 0x060072A2 RID: 29346 RVA: 0x002B6757 File Offset: 0x002B4957
		public static void PromoteToJourneyfriend()
		{
			The.Game.PlayerReputation.PromoteIfBelow(GritGateScripts.FACTION, "journeyfriend", true, true);
			GritGateScripts.OpenRank2Doors();
			GritGateScripts.IdentifyByTag("GritGateJourneyfriendIdentify");
		}

		// Token: 0x060072A3 RID: 29347 RVA: 0x002B6784 File Offset: 0x002B4984
		public static void PromoteToDisciple()
		{
			The.Game.PlayerReputation.PromoteIfBelow(GritGateScripts.FACTION, "disciple", true, true);
			GritGateScripts.OpenRank2Doors();
			GritGateScripts.IdentifyByTag("GritGateDiscipleIdentify");
		}

		// Token: 0x060072A4 RID: 29348 RVA: 0x002B67B1 File Offset: 0x002B49B1
		public static void PromoteToMeyvn()
		{
			The.Game.PlayerReputation.PromoteIfBelow(GritGateScripts.FACTION, "meyvn", true, true);
			GritGateScripts.OpenRank2Doors();
			GritGateScripts.IdentifyByTag("GritGateMeyvnIdentify");
		}

		// Token: 0x060072A5 RID: 29349 RVA: 0x002B67E0 File Offset: 0x002B49E0
		public static void IdentifyByTag(string Tag)
		{
			GameObject player = The.Player;
			if (player == null)
			{
				return;
			}
			Zone currentZone = player.CurrentZone;
			if (currentZone == null)
			{
				return;
			}
			currentZone.ForeachObject(delegate(GameObject GO)
			{
				if (GO.HasTagOrProperty(Tag))
				{
					GO.MakeUnderstood(false);
					GO.ForeachInventoryAndEquipment(delegate(GameObject OGO)
					{
						OGO.MakeUnderstood(false);
					});
				}
			});
		}

		// Token: 0x060072A6 RID: 29350 RVA: 0x002B6820 File Offset: 0x002B4A20
		public static void CheckGritGateDoors()
		{
			int num = The.Game.GetIntGameState("GritGateDoorAccess", -1);
			if (num == -1)
			{
				num = Math.Min(The.Game.PlayerReputation.GetFactionStanding(GritGateScripts.FACTION), 2);
			}
			if (num >= 0)
			{
				GritGateScripts.OpenGritGateDoors(num);
			}
		}

		// Token: 0x060072A7 RID: 29351 RVA: 0x002B6868 File Offset: 0x002B4A68
		public static void OpenGritGateDoors(int Rank)
		{
			Zone zone = The.ZoneManager.GetZone("JoppaWorld.22.14.1.0.13");
			for (int i = 0; i <= Rank; i++)
			{
				string text = "GritGateDoorRank" + i.ToString();
				foreach (GameObject gameObject in zone.YieldObjects())
				{
					if (gameObject.HasTagOrProperty(text))
					{
						Door door;
						if (gameObject.TryGetPart<Door>(out door))
						{
							door.Locked = false;
							if (!door.Open)
							{
								door.PerformOpen(null, null, false);
							}
						}
						ForceProjector forceProjector;
						if (gameObject.TryGetPart<ForceProjector>(out forceProjector))
						{
							forceProjector.AddAllowPassage("Player");
						}
					}
				}
			}
			int intGameState = The.Game.GetIntGameState("GritGateDoorAccess", -1);
			if (Rank > intGameState)
			{
				The.Game.SetIntGameState("GritGateDoorAccess", Rank);
			}
		}

		// Token: 0x060072A8 RID: 29352 RVA: 0x002B6958 File Offset: 0x002B4B58
		public static void OpenRank0Doors()
		{
			GritGateScripts.OpenGritGateDoors(0);
		}

		// Token: 0x060072A9 RID: 29353 RVA: 0x002B6960 File Offset: 0x002B4B60
		[WishCommand("GritGateDoors", null)]
		public static void OpenRank1Doors()
		{
			GritGateScripts.OpenGritGateDoors(1);
		}

		// Token: 0x060072AA RID: 29354 RVA: 0x002B6968 File Offset: 0x002B4B68
		[WishCommand("GritGateDoors+", null)]
		public static void OpenRank2Doors()
		{
			GritGateScripts.OpenGritGateDoors(2);
		}

		// Token: 0x060072AB RID: 29355 RVA: 0x002B6970 File Offset: 0x002B4B70
		public static void BeginInvasion()
		{
			if (!The.Game.HasGameState("CallToArmsStarted"))
			{
				The.Player.CurrentZone.RequirePart<ScriptCallToArms>(false);
			}
		}

		// Token: 0x04003529 RID: 13609
		public static readonly string FACTION = "Barathrumites";
	}
}
