using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ConsoleLib.Console;
using Cysharp.Text;
using Qud.API;
using XRL.Language;
using XRL.Liquids;
using XRL.UI;
using XRL.Wish;
using XRL.World.AI;
using XRL.World.Parts;
using XRL.World.Skills;
using XRL.World.Units;

namespace XRL.World.Quests.GolemQuest
{
	// Token: 0x02000B33 RID: 2867
	[HasWishCommand]
	[Serializable]
	public abstract class GolemQuestSelection
	{
		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x060073A3 RID: 29603 RVA: 0x002B9C54 File Offset: 0x002B7E54
		public GolemQuestSystem System
		{
			get
			{
				GolemQuestSystem golemQuestSystem;
				if ((golemQuestSystem = this._System) == null)
				{
					golemQuestSystem = (this._System = GolemQuestSystem.Get());
				}
				return golemQuestSystem;
			}
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x060073A4 RID: 29604
		public abstract string ID { get; }

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x060073A5 RID: 29605
		public abstract string DisplayName { get; }

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x060073A6 RID: 29606
		public abstract char Key { get; }

		// Token: 0x060073A7 RID: 29607
		public abstract bool IsValid();

		// Token: 0x060073A8 RID: 29608
		public abstract void Pick();

		// Token: 0x060073A9 RID: 29609
		public abstract void Apply(GameObject Object);

		// Token: 0x060073AA RID: 29610
		public abstract IEnumerable<GameObjectUnit> YieldAllEffects();

		// Token: 0x060073AB RID: 29611
		public abstract IEnumerable<GameObjectUnit> YieldRandomEffects();

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x060073AC RID: 29612 RVA: 0x002B9C79 File Offset: 0x002B7E79
		public string Title
		{
			get
			{
				return "Choose " + Grammar.A(this.DisplayName, false);
			}
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x060073AD RID: 29613 RVA: 0x002B9C91 File Offset: 0x002B7E91
		public string Mark
		{
			get
			{
				if (!this.IsValid())
				{
					return "{{red|[X]}}";
				}
				return "{{green|[û]}}";
			}
		}

		// Token: 0x060073AE RID: 29614 RVA: 0x002B9CA8 File Offset: 0x002B7EA8
		public string GetOptionText()
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.Append("Choose ");
			Grammar.A(this.DisplayName, stringBuilder, false);
			string optionChoice = this.GetOptionChoice();
			if (optionChoice == null)
			{
				stringBuilder.Compound("ÿÿÿÿ{{K|<make a selection>}}", '\n');
			}
			else
			{
				stringBuilder.Compound(this.Mark, "\n ");
				if (this.IsValid())
				{
					stringBuilder.Compound(optionChoice);
				}
				else
				{
					stringBuilder.Compound("{{K|").Append(ColorUtility.StripFormatting(optionChoice)).Append("}}");
				}
				this.AppendEffects(stringBuilder, 1);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060073AF RID: 29615
		public abstract string GetOptionChoice();

		// Token: 0x060073B0 RID: 29616 RVA: 0x002B9D42 File Offset: 0x002B7F42
		public virtual void AppendEffects(StringBuilder SB, int Indent = 0)
		{
		}

		// Token: 0x060073B1 RID: 29617 RVA: 0x002B9D44 File Offset: 0x002B7F44
		public virtual void Save(SerializationWriter Writer)
		{
			Type type = base.GetType();
			FieldAttributes attr = FieldAttributes.Static | FieldAttributes.InitOnly | FieldAttributes.Literal | FieldAttributes.NotSerialized;
			FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
			Writer.Write(fields.Count<FieldInfo>((FieldInfo x) => (x.Attributes & attr) == FieldAttributes.PrivateScope));
			foreach (FieldInfo fieldInfo in fields)
			{
				if ((fieldInfo.Attributes & attr) <= FieldAttributes.PrivateScope)
				{
					Writer.Write(fieldInfo.Name);
					Writer.WriteObject(fieldInfo.GetValue(this));
				}
			}
		}

		// Token: 0x060073B2 RID: 29618 RVA: 0x002B9DCC File Offset: 0x002B7FCC
		public virtual void Load(SerializationReader Reader)
		{
			Type type = base.GetType();
			int num = Reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				string text = Reader.ReadString();
				object obj = Reader.ReadObject();
				try
				{
					FieldInfo field = type.GetField(text, BindingFlags.Instance | BindingFlags.Public);
					if (field != null)
					{
						field.SetValue(this, obj);
					}
				}
				catch (Exception ex)
				{
					MetricsManager.LogException("Error deserializing golem selection field", ex, "game_exception");
				}
			}
		}

		// Token: 0x060073B3 RID: 29619 RVA: 0x002B9E40 File Offset: 0x002B8040
		public static void ProcessDescription(GameObject Object, GameObject Source = null)
		{
			Description part = Object.GetPart<Description>();
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = ZString.CreateStringBuilder())
			{
				utf16ValueStringBuilder.Append(part._Short);
				if (Source == null)
				{
					Source = Object;
				}
				string text = Source.GetStringProperty("ArmamentSkill", null).Coalesce("Cudgel");
				SkillEntry value = SkillFactory.Factory.SkillByClass.GetValue(text, null);
				utf16ValueStringBuilder.Replace("=armament.skill=", Grammar.Pluralize(value.Name.ToLowerInvariant()));
				utf16ValueStringBuilder.Replace("=armament.skill.snippet=", value.Snippet);
				string text2 = Source.GetStringProperty("AtzmusSourceDisplayName", null);
				string text3 = Source.GetStringProperty("AtzmusSourceIndefiniteArticle", null).Coalesce("");
				if (text2.IsNullOrEmpty())
				{
					GameObjectBlueprint acreatureBlueprintModel = EncountersAPI.GetACreatureBlueprintModel(null);
					text2 = acreatureBlueprintModel.DisplayName();
					text3 = (acreatureBlueprintModel.HasProperName() ? "" : Grammar.IndefiniteArticle(text2, false));
				}
				utf16ValueStringBuilder.Replace("=atzmus.creature=", text2);
				utf16ValueStringBuilder.Replace("=atzmus.creature.an=", text3 + text2);
				string stringProperty = Source.GetStringProperty("BodyBlueprint", null);
				GameObjectBlueprint value2 = GameObjectFactory.Factory.Blueprints.GetValue(stringProperty, null);
				string text4 = ((value2 != null) ? value2.GetxTag("TextFragments", "PoeticFeatures", null) : null);
				if (text4 == null)
				{
					text4 = Source.GetBlueprint(true).GetxTag("TextFragments", "PoeticFeatures", null).Coalesce("artfulness,gentle stillness,childlike wonder");
				}
				utf16ValueStringBuilder.Replace("=body.features=", Grammar.MakeTheList(text4.Split(',', StringSplitOptions.None), false));
				BaseLiquid baseLiquid = LiquidVolume.GetLiquid(Source.GetStringProperty("CatalystLiquid", null).Coalesce("Water")) ?? LiquidVolume.Liquids.GetRandomElement<KeyValuePair<string, BaseLiquid>>(null).Value;
				utf16ValueStringBuilder.Replace("=catalyst=", ((baseLiquid != null) ? baseLiquid.Name : null) ?? "water");
				string text5 = Source.GetStringProperty("HamsaDisplayName", null);
				string text6 = Source.GetStringProperty("HamsaIndefiniteArticle", null).Coalesce("");
				if (text5.IsNullOrEmpty())
				{
					GameObjectBlueprint anItemBlueprintModel = EncountersAPI.GetAnItemBlueprintModel(null);
					text5 = anItemBlueprintModel.DisplayName();
					text6 = (anItemBlueprintModel.HasProperName() ? "" : Grammar.IndefiniteArticle(text5, false));
				}
				utf16ValueStringBuilder.Replace("=hamsa=", text5);
				utf16ValueStringBuilder.Replace("=hamsa.an=", text6 + text5);
				part._Short = utf16ValueStringBuilder.ToString();
			}
		}

		// Token: 0x060073B4 RID: 29620 RVA: 0x002BA0CC File Offset: 0x002B82CC
		protected static bool IsValidHolder(GameObject Object)
		{
			return GolemQuestSystem.IsValidHolder(Object);
		}

		// Token: 0x060073B5 RID: 29621 RVA: 0x002BA0D4 File Offset: 0x002B82D4
		protected List<GameObject> GetValidHolders()
		{
			return this.System.GetValidHolders();
		}

		// Token: 0x060073B6 RID: 29622 RVA: 0x002BA0E4 File Offset: 0x002B82E4
		[WishCommand("golemquest:alleffects", null)]
		public static void WishAllEffectsGolem()
		{
			Popup.Suppress = true;
			GolemQuestSelection.WISH_ALL = true;
			try
			{
				GolemQuestSystem golemQuestSystem = GolemQuestSystem.Require();
				GameObject gameObject = (golemQuestSystem.Body.Material = EncountersAPI.GetACreature(null));
				GameObject gameObject2 = The.Player.CurrentCell.getClosestPassableCell().AddObject(Vehicle.CreateOwnedBy(GolemBodySelection.GetBodyBlueprintFor(gameObject).Name, The.Player, new bool?(true), new bool?(true), true), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				gameObject2.SetAlliedLeader<AllyConstructed>(The.Player, 0, false);
				foreach (KeyValuePair<string, GolemQuestSelection> keyValuePair in golemQuestSystem.Selections)
				{
					keyValuePair.Value.Apply(gameObject2);
				}
				golemQuestSystem.Body.Material = null;
				gameObject.Destroy(null, false, false, null);
			}
			finally
			{
				Popup.Suppress = false;
				GolemQuestSelection.WISH_ALL = false;
			}
		}

		// Token: 0x060073B7 RID: 29623 RVA: 0x002BA1EC File Offset: 0x002B83EC
		[WishCommand("golemquest:random", null)]
		public static void WishRandomEffectsGolem(string Param)
		{
			Popup.Suppress = true;
			GolemQuestSelection.WISH_RANDOM = true;
			try
			{
				int num;
				GameObjectBlueprint gameObjectBlueprint;
				if (int.TryParse(Param, out num))
				{
					for (int i = 0; i < num; i++)
					{
						GolemQuestSelection.CreateWishGolem(null, null);
					}
				}
				else if (GameObjectFactory.Factory.Blueprints.TryGetValue(Param, out gameObjectBlueprint))
				{
					GolemQuestSelection.CreateWishGolem(null, GameObjectFactory.Factory.CreateObject(gameObjectBlueprint, 0, 0, null, null, null, null, null));
				}
			}
			finally
			{
				Popup.Suppress = false;
				GolemQuestSelection.WISH_RANDOM = false;
			}
		}

		// Token: 0x060073B8 RID: 29624 RVA: 0x002BA274 File Offset: 0x002B8474
		[WishCommand("golemquest:random", null)]
		public static void WishRandomEffectsGolem()
		{
			Popup.Suppress = true;
			GolemQuestSelection.WISH_RANDOM = true;
			try
			{
				GolemQuestSelection.CreateWishGolem(null, null);
			}
			finally
			{
				Popup.Suppress = false;
				GolemQuestSelection.WISH_RANDOM = false;
			}
		}

		// Token: 0x060073B9 RID: 29625 RVA: 0x002BA2B4 File Offset: 0x002B84B4
		[WishCommand("golemquest:finishbuild", null)]
		public static void WishFinishGolem()
		{
			GameObject firstObjectWithPart = The.Player.CurrentZone.GetFirstObjectWithPart("GolemQuestMound");
			GolemQuestMound golemQuestMound = ((firstObjectWithPart != null) ? firstObjectWithPart.GetPart<GolemQuestMound>() : null);
			if (golemQuestMound != null && golemQuestMound.CompleteTurn > 0L)
			{
				golemQuestMound.CompleteTurn = The.Game.TimeTicks;
				golemQuestMound.CheckCompletion();
				return;
			}
			Popup.Show("No mound of scrap and clay was found to complete.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
		}

		// Token: 0x060073BA RID: 29626 RVA: 0x002BA31B File Offset: 0x002B851B
		[WishCommand("golemquest:finalmound", null)]
		public static void WishFinalMound()
		{
			GolemQuestSelection.PlaceFinalMound(null);
		}

		// Token: 0x060073BB RID: 29627 RVA: 0x002BA324 File Offset: 0x002B8524
		public static void PlaceFinalMound(Cell Cell = null)
		{
			if (Cell == null)
			{
				Cell = The.PlayerCell.GetFirstEmptyAdjacentCell();
			}
			GameObject gameObject = Cell.ParentZone.FindObject("Golem Mound");
			if (gameObject == null)
			{
				gameObject = Cell.AddObject("Golem Mound", null, null, null, null);
			}
			else
			{
				Cell = gameObject.CurrentCell;
			}
			gameObject.SetIntProperty("Wish", 1, false);
			GameObject gameObject2 = Cell.GetFirstEmptyAdjacentCell().AddObject("RelicChest", null, null, null, null);
			gameObject2.Render.Visible = false;
			GolemArmamentSelection.ReceiveMaterials(gameObject2);
			GolemHamsaSelection.ReceiveMaterials(gameObject2);
			GolemAscensionSelection.ReceiveMaterials(gameObject2);
			GolemAtzmusSelection.ReceiveMaterials(gameObject2);
			GolemCatalystSelection.ReceiveMaterials(gameObject2);
			GolemIncantationSelection.CreateAccomplishments();
			GolemBodySelection.CreateChassis(Cell.ParentZone);
		}

		// Token: 0x060073BC RID: 29628 RVA: 0x002BA3CC File Offset: 0x002B85CC
		public static GameObject CreateWishGolem(Cell Cell = null, GameObject Body = null)
		{
			if (Cell == null)
			{
				Cell = The.Player.CurrentCell.getClosestPassableCell();
			}
			GolemQuestSystem golemQuestSystem = GolemQuestSystem.Require();
			GameObject gameObject = (golemQuestSystem.Body.Material = Body ?? EncountersAPI.GetACreature(null));
			GameObject gameObject2 = Cell.AddObject(Vehicle.CreateOwnedBy(GolemBodySelection.GetBodyBlueprintFor(gameObject).Name, The.Player, new bool?(true), new bool?(true), true), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			GolemQuestSelection.ProcessDescription(gameObject2, null);
			gameObject2.Inventory.RemoveAll((GameObject x) => !x.IsNatural(), true);
			gameObject2.SetAlliedLeader<AllyConstructed>(The.Player, 0, false);
			foreach (KeyValuePair<string, GolemQuestSelection> keyValuePair in golemQuestSystem.Selections)
			{
				keyValuePair.Value.Apply(gameObject2);
			}
			gameObject2.Brain.PerformReequip(false, true, false, null);
			golemQuestSystem.Body.Material = null;
			gameObject.Destroy(null, false, false, null);
			return gameObject2;
		}

		// Token: 0x04003544 RID: 13636
		public static bool WISH_ALL;

		// Token: 0x04003545 RID: 13637
		public static bool WISH_RANDOM;

		// Token: 0x04003546 RID: 13638
		public const string MAKE_SELECTION = "ÿÿÿÿ{{K|<make a selection>}}";

		// Token: 0x04003547 RID: 13639
		public const string NO_REQUIRED = "You have nothing that meets the requirement of the ";

		// Token: 0x04003548 RID: 13640
		[NonSerialized]
		private GolemQuestSystem _System;
	}
}
