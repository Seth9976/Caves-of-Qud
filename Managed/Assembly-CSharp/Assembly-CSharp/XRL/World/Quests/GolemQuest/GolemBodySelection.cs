using System;
using System.Collections.Generic;
using Qud.API;
using XRL.UI;
using XRL.Wish;
using XRL.World.AI;
using XRL.World.Parts;
using XRL.World.Units;

namespace XRL.World.Quests.GolemQuest
{
	// Token: 0x02000B2C RID: 2860
	[HasWishCommand]
	[Serializable]
	public class GolemBodySelection : GolemGameObjectSelection
	{
		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x060072E4 RID: 29412 RVA: 0x002B7449 File Offset: 0x002B5649
		public override string ID
		{
			get
			{
				return "Body";
			}
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x060072E5 RID: 29413 RVA: 0x002B7450 File Offset: 0x002B5650
		public override string DisplayName
		{
			get
			{
				return "body";
			}
		}

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x060072E6 RID: 29414 RVA: 0x002B7457 File Offset: 0x002B5657
		public override char Key
		{
			get
			{
				return 'b';
			}
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x060072E7 RID: 29415 RVA: 0x002B745B File Offset: 0x002B565B
		public override bool IsCarried
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x060072E8 RID: 29416 RVA: 0x002B745E File Offset: 0x002B565E
		public GameObjectBlueprint BodyBlueprint
		{
			get
			{
				return GolemBodySelection.GetBodyBlueprintFor(this.Material);
			}
		}

		// Token: 0x060072E9 RID: 29417 RVA: 0x002B746B File Offset: 0x002B566B
		public override bool IsValid(GameObject Object)
		{
			return base.IsValid(Object) && ((Object.IsPlayerLed() && Object.Level >= 30) || Object.HasIntProperty("GolemChassis"));
		}

		// Token: 0x060072EA RID: 29418 RVA: 0x002B7498 File Offset: 0x002B5698
		public override void Apply(GameObject Object)
		{
			base.Apply(Object);
			Object.GetStat("AV").BaseValue += 10;
			Object.GetStat("Strength").BoostStat(3);
			Object.GetStat("Toughness").BoostStat(2);
		}

		// Token: 0x060072EB RID: 29419 RVA: 0x002B74E7 File Offset: 0x002B56E7
		public override IEnumerable<GameObjectUnit> YieldEffectsOf(GameObject Object)
		{
			GameObjectBlueprint bodyBlueprintFor = GolemBodySelection.GetBodyBlueprintFor(Object);
			yield return new GameObjectPlaceholderUnit
			{
				Description = bodyBlueprintFor.CachedDisplayNameStripped
			};
			yield break;
		}

		// Token: 0x060072EC RID: 29420 RVA: 0x002B74F8 File Offset: 0x002B56F8
		public static Dictionary<string, string> GetBodyBySpecies()
		{
			if (GolemBodySelection.BodyBySpecies != null)
			{
				return GolemBodySelection.BodyBySpecies;
			}
			GolemBodySelection.BodyBySpecies = new Dictionary<string, string>();
			Dictionary<string, List<string>> dictionary = null;
			foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.BlueprintList)
			{
				if (gameObjectBlueprint.HasTag("Golem", false) && !gameObjectBlueprint.IsBaseBlueprint())
				{
					string text = gameObjectBlueprint.GetPropertyOrTag("SpeciesOverride").Coalesce(gameObjectBlueprint.GetPropertyOrTag("Species").Coalesce(gameObjectBlueprint.GetPropertyOrTag("Class")));
					if (text.IsNullOrEmpty())
					{
						MetricsManager.LogWarning("Blueprint '" + gameObjectBlueprint.Name + "' defined as golem body but lacks a Species or Class tag.");
					}
					else
					{
						if (gameObjectBlueprint.GetxTag("TextFragments", "PoeticFeatures", null).IsNullOrEmpty())
						{
							MetricsManager.LogWarning("Blueprint '" + gameObjectBlueprint.Name + "' defined as golem body but lacks a PoeticFeatures xtag.");
						}
						string text2;
						if (GolemBodySelection.BodyBySpecies.TryGetValue(text, out text2) && text2 != gameObjectBlueprint.Name)
						{
							if (dictionary == null)
							{
								dictionary = new Dictionary<string, List<string>>();
							}
							List<string> list;
							if (!dictionary.TryGetValue(text, out list))
							{
								Dictionary<string, List<string>> dictionary2 = dictionary;
								string text3 = text;
								List<string> list2 = new List<string>();
								list2.Add(text2);
								list = list2;
								dictionary2[text3] = list2;
							}
							if (!list.Contains(gameObjectBlueprint.Name))
							{
								list.Add(gameObjectBlueprint.Name);
							}
						}
						else
						{
							GolemBodySelection.BodyBySpecies[text] = gameObjectBlueprint.Name;
						}
					}
				}
			}
			if (!dictionary.IsNullOrEmpty<KeyValuePair<string, List<string>>>())
			{
				foreach (KeyValuePair<string, List<string>> keyValuePair in dictionary)
				{
					MetricsManager.LogError("Duplicate golem species mapping: " + keyValuePair.Key + " used by " + string.Join(", ", keyValuePair.Value));
				}
			}
			return GolemBodySelection.BodyBySpecies;
		}

		// Token: 0x060072ED RID: 29421 RVA: 0x002B770C File Offset: 0x002B590C
		public static GameObjectBlueprint GetBodyBlueprintFor(GameObject Object)
		{
			return GolemBodySelection.GetBodyBlueprintFor(Object.Property, Object.GetBlueprint(true).Tags);
		}

		// Token: 0x060072EE RID: 29422 RVA: 0x002B7725 File Offset: 0x002B5925
		public static GameObjectBlueprint GetBodyBlueprintFor(GameObjectBlueprint Blueprint)
		{
			return GolemBodySelection.GetBodyBlueprintFor(Blueprint.Props, Blueprint.Tags);
		}

		// Token: 0x060072EF RID: 29423 RVA: 0x002B7738 File Offset: 0x002B5938
		public static GameObjectBlueprint GetBodyBlueprintFor(Dictionary<string, string> Properties, Dictionary<string, string> Tags)
		{
			Dictionary<string, string> bodyBySpecies = GolemBodySelection.GetBodyBySpecies();
			string text = ((Properties != null) ? Properties.GetValue("SpeciesOverride", null).Coalesce((Tags != null) ? Tags.GetValue("SpeciesOverride", null) : null) : null);
			string text2;
			GameObjectBlueprint gameObjectBlueprint;
			if (!text.IsNullOrEmpty() && bodyBySpecies.TryGetValue(text, out text2) && GameObjectFactory.Factory.Blueprints.TryGetValue(text2, out gameObjectBlueprint))
			{
				return gameObjectBlueprint;
			}
			text = ((Properties != null) ? Properties.GetValue("Species", null).Coalesce((Tags != null) ? Tags.GetValue("Species", null) : null) : null);
			if (!text.IsNullOrEmpty() && bodyBySpecies.TryGetValue(text, out text2) && GameObjectFactory.Factory.Blueprints.TryGetValue(text2, out gameObjectBlueprint))
			{
				return gameObjectBlueprint;
			}
			text = ((Properties != null) ? Properties.GetValue("Class", null).Coalesce((Tags != null) ? Tags.GetValue("Class", null) : null) : null);
			if (!text.IsNullOrEmpty() && bodyBySpecies.TryGetValue(text, out text2) && GameObjectFactory.Factory.Blueprints.TryGetValue(text2, out gameObjectBlueprint))
			{
				return gameObjectBlueprint;
			}
			return GameObjectFactory.Factory.Blueprints.GetValue("Oddity Golem", null);
		}

		// Token: 0x060072F0 RID: 29424 RVA: 0x002B7859 File Offset: 0x002B5A59
		[WishCommand("golemquest:chassis", null)]
		private static void WishChassis()
		{
			GolemBodySelection.CreateChassis(The.ActiveZone);
		}

		// Token: 0x060072F1 RID: 29425 RVA: 0x002B7868 File Offset: 0x002B5A68
		public static void CreateChassis(Zone Z)
		{
			Cell cell = Z.GetCell(79, 24);
			GameObjectFactory factory = GameObjectFactory.Factory;
			if (cell.HasObjectWithIntProperty("GolemChassis"))
			{
				return;
			}
			foreach (KeyValuePair<string, string> keyValuePair in GolemBodySelection.GetBodyBySpecies())
			{
				GamePartBlueprint part = factory.Blueprints[keyValuePair.Value].GetPart("Render");
				GameObject gameObject = factory.CreateSampleObject("Object", null);
				Render render = gameObject.RequirePart<Render>(false);
				part.InitializePartInstance(render);
				render.Visible = false;
				render.DisplayName = render.DisplayName.Replace("golem", "chassis");
				gameObject.SetIntProperty("GolemChassis", 1, false);
				gameObject.SetStringProperty("SpeciesOverride", keyValuePair.Key, false);
				cell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			}
		}

		// Token: 0x060072F2 RID: 29426 RVA: 0x002B7978 File Offset: 0x002B5B78
		[WishCommand("golemquest:body", null)]
		private static void Wish()
		{
			GolemBodySelection.WishCompanion(EncountersAPI.GetACreature(null));
		}

		// Token: 0x060072F3 RID: 29427 RVA: 0x002B7988 File Offset: 0x002B5B88
		[WishCommand("golemquest:body", null)]
		private static void WishSpec(string Value)
		{
			if (Value == "animated")
			{
				GolemBodySelection.WishCompanion(EncountersAPI.GetAnAnimatedObject());
				return;
			}
			GameObjectBlueprint gameObjectBlueprint;
			if (GameObjectFactory.Factory.Blueprints.TryGetValue(Value, out gameObjectBlueprint))
			{
				GameObject gameObject = GameObjectFactory.Factory.CreateObject(gameObjectBlueprint, 0, 0, null, null, null, null, null);
				if (!gameObjectBlueprint.HasPart("Brain"))
				{
					AnimateObject.Animate(gameObject, null, null);
				}
				GolemBodySelection.WishCompanion(gameObject);
				return;
			}
			Popup.ShowFail("No blueprint by ID '" + Value + "' found.", true, true, true);
		}

		// Token: 0x060072F4 RID: 29428 RVA: 0x002B7A08 File Offset: 0x002B5C08
		private static void WishCompanion(GameObject Body)
		{
			The.Player.CurrentCell.GetLocalEmptyAdjacentCells().GetRandomElement<Cell>(null).AddObject(Body, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			Body.GetStat("Level").BaseValue = 30;
			Body.Brain.Goals.Clear();
			Body.SetAlliedLeader<AllyWish>(The.Player, 0, false);
		}

		// Token: 0x0400352F RID: 13615
		public const string BASE_ID = "Body";

		// Token: 0x04003530 RID: 13616
		[NonSerialized]
		private static Dictionary<string, string> BodyBySpecies;
	}
}
