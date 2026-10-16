using System;
using Qud.API;
using XRL.Rules;
using XRL.Wish;

namespace XRL.World.Parts
{
	// Token: 0x02000E5D RID: 3677
	[HasWishCommand]
	[Serializable]
	public class ConvertSpawner : IPart
	{
		// Token: 0x060094BE RID: 38078 RVA: 0x0035C3D1 File Offset: 0x0035A5D1
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x060094BF RID: 38079 RVA: 0x0035C3D4 File Offset: 0x0035A5D4
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeObjectCreatedEvent.ID;
		}

		// Token: 0x060094C0 RID: 38080 RVA: 0x0035C3EC File Offset: 0x0035A5EC
		public override bool HandleEvent(BeforeObjectCreatedEvent E)
		{
			GameObject gameObject;
			if (this.Faction == "Kyakukya")
			{
				gameObject = ConvertSpawner.CreateKyakukyanConvert(null, this.DoesWander);
			}
			else if (this.Faction == "YdFreehold")
			{
				gameObject = ConvertSpawner.CreateYdFreeholdConvert(null, this.DoesWander);
			}
			else
			{
				gameObject = ConvertSpawner.CreateMechanimistConvert(null, this.DoesWander, this.IsPilgrim, this.ParentObject.HasTag("IsLibrarian"));
			}
			gameObject.FireEvent("VillageInit");
			gameObject.SetIntProperty("Social", 1, false);
			gameObject.SetStringProperty("SpawnedFrom", this.ParentObject.Blueprint, false);
			E.ReplacementObject = gameObject;
			return base.HandleEvent(E);
		}

		// Token: 0x060094C1 RID: 38081 RVA: 0x0035C4A0 File Offset: 0x0035A6A0
		public static GameObject CreateMechanimistConvert(GameObject ParentObject = null, bool DoesWander = true, bool IsPilgrim = false, bool IsLibrarian = false)
		{
			GameObject gameObject = ParentObject;
			if (ParentObject == null)
			{
				do
				{
					gameObject = ConvertSpawner.GetBaseObject(IsLibrarian);
				}
				while (gameObject.Brain == null || gameObject.Brain.Allegiance.ContainsKey("Mechanimists"));
			}
			gameObject.Brain.Allegiance.Clear();
			gameObject.Brain.Allegiance.Add("Mechanimists", 100);
			gameObject.Brain.Allegiance.Hostile = false;
			if (DoesWander)
			{
				gameObject.Brain.Wanders = true;
				gameObject.Brain.WandersRandomly = true;
				gameObject.AddPart<AIShopper>(new AIShopper(), true, false);
			}
			else
			{
				gameObject.Brain.Wanders = false;
				gameObject.Brain.WandersRandomly = false;
				gameObject.AddPart<AISitting>(new AISitting(), true, false);
			}
			gameObject.ReceiveObject("Canticles3", false, 0, 0, null, null, null, null);
			ConversationScript part = gameObject.GetPart<ConversationScript>();
			if (IsPilgrim)
			{
				gameObject.RequirePart<AIPilgrim>(false);
				if (part != null)
				{
					part.Append = "\n\nGlory to Shekhinah.~\n\nHumble before my Fathers, I walk.~\n\nShow mercy to a weary pilgrim.~\n\nPraise be upon Nisroch, who shelters us stiltseekers.";
				}
			}
			else
			{
				gameObject.RemovePart<AIPilgrim>();
				if (part != null)
				{
					part.Append = "\n\nGlory to Shekhinah.~\n\nMay the ground shake but the Six Day Stilt never tumble!~\n\nPraise our argent Fathers! Wisest of all beings.";
				}
			}
			if (IsLibrarian)
			{
				gameObject.AddPart<MechanimistLibrarian>(new MechanimistLibrarian(), true, false);
				gameObject.SetStringProperty("Mayor", "Mechanimists", false);
			}
			else
			{
				gameObject.RequirePart<SocialRoles>(false).RequireRole("Mechanimist convert");
			}
			return gameObject;
		}

		// Token: 0x060094C2 RID: 38082 RVA: 0x0035C5E4 File Offset: 0x0035A7E4
		public static GameObject CreateKyakukyanConvert(GameObject ParentObject = null, bool DoesWander = true)
		{
			GameObject gameObject = ParentObject;
			if (ParentObject == null)
			{
				do
				{
					gameObject = ConvertSpawner.GetBaseObject_Kyakukya();
				}
				while (gameObject.Brain == null || gameObject.Brain.Allegiance.ContainsKey("Kyakukya"));
			}
			gameObject.Brain.Allegiance.Clear();
			gameObject.Brain.Allegiance.Add("Kyakukya", 100);
			gameObject.Brain.Allegiance.Hostile = false;
			if (DoesWander)
			{
				gameObject.Brain.Wanders = true;
				gameObject.Brain.WandersRandomly = true;
			}
			else
			{
				gameObject.Brain.Wanders = false;
				gameObject.Brain.WandersRandomly = false;
				gameObject.AddPart<AISitting>(new AISitting(), true, false);
			}
			gameObject.RemovePart<AIPilgrim>();
			gameObject.ReceiveObject("Grave Goods", false, 0, 0, null, null, null, null);
			gameObject.ReceiveObject("Plump Mushroom", Stat.Random(2, 5), false, 0, 0, null, null, null, null);
			ConversationScript part = gameObject.GetPart<ConversationScript>();
			if (part != null)
			{
				part.Append = "\n\nSix fingers to the earthen lips.~\n\nPlease you to seek him.~\n\nCome gather! -and weave for the passing.~\n\nOur roots loosen jewels from the soil.~\n\nBe ape-still and muse.~\n\nWhat will you strum and dust for Saad?";
			}
			gameObject.RequirePart<SocialRoles>(false).RequireRole("worshipper of Oboroqoru");
			return gameObject;
		}

		// Token: 0x060094C3 RID: 38083 RVA: 0x0035C6F4 File Offset: 0x0035A8F4
		public static GameObject CreateYdFreeholdConvert(GameObject ParentObject = null, bool DoesWander = true)
		{
			GameObject gameObject = ParentObject;
			if (ParentObject == null)
			{
				do
				{
					gameObject = ConvertSpawner.GetBaseObject(false);
				}
				while (gameObject.Brain == null || gameObject.Brain.Allegiance.ContainsKey("YdFreehold"));
			}
			gameObject.Brain.Allegiance.Clear();
			gameObject.Brain.Allegiance.Add("YdFreehold", 100);
			gameObject.Brain.Allegiance.Hostile = false;
			gameObject.Reefer = true;
			if (DoesWander)
			{
				gameObject.Brain.Wanders = true;
				gameObject.Brain.WandersRandomly = true;
				gameObject.SetIntProperty("WanderUpStairs", 1, false);
				gameObject.SetIntProperty("WanderDownStairs", 1, false);
			}
			else
			{
				gameObject.Brain.Wanders = false;
				gameObject.Brain.WandersRandomly = false;
				gameObject.AddPart<AISitting>(new AISitting(), true, false);
			}
			gameObject.RemovePart<AIPilgrim>();
			gameObject.RequirePart<SocialRoles>(false).RequireRole("denizen of the Yd Freehold");
			return gameObject;
		}

		// Token: 0x060094C4 RID: 38084 RVA: 0x0035C7E4 File Offset: 0x0035A9E4
		private static GameObject GetBaseObject(bool IsLibrarian = false)
		{
			if (!IsLibrarian)
			{
				return EncountersAPI.GetALegendaryEligibleCreature((GameObjectBlueprint o) => !o.HasTag("ExcludeFromVillagePopulations", false));
			}
			return EncountersAPI.GetALegendaryEligibleCreatureWithAnInventory((GameObjectBlueprint o) => !o.HasTag("NoLibrarian", false) && !o.HasTag("ExcludeFromVillagePopulations", false));
		}

		// Token: 0x060094C5 RID: 38085 RVA: 0x0035C840 File Offset: 0x0035AA40
		private static GameObject GetBaseObject_Kyakukya()
		{
			int num = Stat.Roll(1, 100);
			if (num <= 40)
			{
				return EncountersAPI.GetALegendaryEligibleCreature((GameObjectBlueprint o) => !o.HasTag("ExcludeFromVillagePopulations", false) && o.HasTag("DynamicObjectsTable:Jungle_Creatures", false) && !o.InheritsFrom("Goatfolk"));
			}
			if (num <= 50)
			{
				return EncountersAPI.GetALegendaryEligibleCreature((GameObjectBlueprint o) => !o.HasTag("ExcludeFromVillagePopulations", false) && o.HasTag("DynamicObjectsTable:Jungle_Creatures", false));
			}
			if (num <= 65)
			{
				return EncountersAPI.GetALegendaryEligibleCreature((GameObjectBlueprint o) => !o.HasTag("ExcludeFromVillagePopulations", false) && o.HasTag("Humanoid", false) && !o.InheritsFrom("Goatfolk"));
			}
			return EncountersAPI.GetALegendaryEligibleCreature((GameObjectBlueprint o) => !o.HasTag("ExcludeFromVillagePopulations", false));
		}

		// Token: 0x060094C6 RID: 38086 RVA: 0x0035C8F8 File Offset: 0x0035AAF8
		[WishCommand("convert", null)]
		public static void Wish(string Param)
		{
			string text;
			string text2;
			Param.Split(':', out text, out text2);
			if (text2.IsNullOrEmpty())
			{
				text2 = text;
				text = "mechanimist";
			}
			WishResult wishResult = WishSearcher.SearchForBlueprint(text2);
			GameObject gameObject = GameObjectFactory.Factory.CreateObject(wishResult.Result, 0, 0, null, null, null, "Wish", null);
			if (text.Contains("freehold", StringComparison.OrdinalIgnoreCase))
			{
				gameObject = ConvertSpawner.CreateYdFreeholdConvert(gameObject, true);
			}
			else if (text.Contains("kyakukya", StringComparison.OrdinalIgnoreCase))
			{
				gameObject = ConvertSpawner.CreateKyakukyanConvert(gameObject, true);
			}
			else
			{
				gameObject = ConvertSpawner.CreateMechanimistConvert(gameObject, true, false, false);
			}
			gameObject.FireEvent("VillageInit");
			The.PlayerCell.getClosestEmptyCell().AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
		}

		// Token: 0x04003D88 RID: 15752
		public string Faction = "Mechanimists";

		// Token: 0x04003D89 RID: 15753
		public bool DoesWander = true;

		// Token: 0x04003D8A RID: 15754
		public bool IsPilgrim;

		// Token: 0x04003D8B RID: 15755
		public const int CHANCE_FOR_NONGOATFOLK_JUNGLE_CONVERT = 40;

		// Token: 0x04003D8C RID: 15756
		public const int CHANCE_FOR_ALL_JUNGLE_CONVERT = 10;

		// Token: 0x04003D8D RID: 15757
		public const int CHANCE_FOR_HUMANOID_CONVERT = 15;
	}
}
