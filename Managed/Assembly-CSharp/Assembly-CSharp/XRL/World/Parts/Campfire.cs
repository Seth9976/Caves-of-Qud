using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using ConsoleLib.Console;
using HistoryKit;
using Qud.API;
using Qud.UI;
using UnityEngine;
using Wintellect.PowerCollections;
using XRL.Collections;
using XRL.Language;
using XRL.Rules;
using XRL.UI;
using XRL.World.Effects;
using XRL.World.Parts.Mutation;
using XRL.World.Parts.Skill;
using XRL.World.Skills.Cooking;

namespace XRL.World.Parts
{
	// Token: 0x02001229 RID: 4649
	[Serializable]
	public class Campfire : IActivePart
	{
		// Token: 0x0600B871 RID: 47217 RVA: 0x004316FA File Offset: 0x0042F8FA
		public Campfire()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B872 RID: 47218 RVA: 0x00431714 File Offset: 0x0042F914
		public Campfire(List<CookingRecipe> specificProcgenMeals)
			: this()
		{
			this.specificProcgenMeals = specificProcgenMeals;
		}

		// Token: 0x0600B873 RID: 47219 RVA: 0x00431723 File Offset: 0x0042F923
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600B874 RID: 47220 RVA: 0x00431728 File Offset: 0x0042F928
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == CanSmartUseEvent.ID || ID == CommandSmartUseEvent.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == GetCookingActionsEvent.ID || ID == GetInventoryActionsEvent.ID || ID == PooledEvent<GetPointsOfInterestEvent>.ID || ID == InventoryActionEvent.ID || ID == SingletonEvent<RadiatesHeatEvent>.ID;
		}

		// Token: 0x0600B875 RID: 47221 RVA: 0x00431781 File Offset: 0x0042F981
		public override bool HandleEvent(CanSmartUseEvent E)
		{
			return false;
		}

		// Token: 0x0600B876 RID: 47222 RVA: 0x00431784 File Offset: 0x0042F984
		public override bool HandleEvent(CommandSmartUseEvent E)
		{
			return !this.Cook() && base.HandleEvent(E);
		}

		// Token: 0x0600B877 RID: 47223 RVA: 0x00431798 File Offset: 0x0042F998
		public override bool HandleEvent(GetPointsOfInterestEvent E)
		{
			if (E.StandardChecks(this, E.Actor, null))
			{
				Cell currentCell = this.ParentObject.CurrentCell;
				if (currentCell != null && !currentCell.IsSolidForOtherThan(E.Actor, this.ParentObject, false))
				{
					string propertyOrTag = this.ParentObject.GetPropertyOrTag("PointOfInterestKey", null);
					bool flag = true;
					string text = null;
					if (!propertyOrTag.IsNullOrEmpty())
					{
						PointOfInterest pointOfInterest = E.Find(propertyOrTag);
						if (pointOfInterest != null)
						{
							if (this.ParentObject.DistanceTo(E.Actor) < pointOfInterest.GetDistanceTo(E.Actor))
							{
								E.Remove(pointOfInterest);
								text = "nearest";
							}
							else
							{
								flag = false;
								pointOfInterest.Explanation = "nearest";
							}
						}
					}
					if (flag)
					{
						E.Add(this.ParentObject, this.ParentObject.GetReferenceDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, false, false, null, false), text, propertyOrTag, null, null, null, 1, null, 0);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B878 RID: 47224 RVA: 0x00431884 File Offset: 0x0042FA84
		public override bool HandleEvent(RadiatesHeatEvent E)
		{
			return (this.ParentObject.HasTagOrProperty("CampfireHeatSelfOnly") || !base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null)) && base.HandleEvent(E);
		}

		// Token: 0x0600B879 RID: 47225 RVA: 0x004318D4 File Offset: 0x0042FAD4
		public override bool HandleEvent(EndTurnEvent E)
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell == null)
			{
				return true;
			}
			if (!this.ExtinguishBlueprint.IsNullOrEmpty())
			{
				GameObject firstObjectWithPart = currentCell.GetFirstObjectWithPart("LiquidVolume", new Predicate<GameObject>(Campfire.CanExtinguish));
				if (firstObjectWithPart != null)
				{
					this.Extinguish(null, firstObjectWithPart);
					return true;
				}
			}
			if (base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				if (this.ParentObject.Physics != null && this.ParentObject.Physics.Temperature < 600 && Stat.Random5(1, 100) <= 10)
				{
					this.ParentObject.TemperatureChange(150, this.ParentObject, false, false, false, false, 5, null, null);
				}
				if (currentCell.Objects.Count > 1 && !this.ParentObject.HasTagOrProperty("CampfireHeatSelfOnly"))
				{
					int phase = this.ParentObject.GetPhase();
					int i = 0;
					int count = currentCell.Objects.Count;
					while (i < count)
					{
						GameObject gameObject = currentCell.Objects[i];
						if (gameObject != this.ParentObject)
						{
							Physics physics = gameObject.Physics;
							if (physics != null && physics.Temperature < 600 && (!gameObject.IsCombat || Stat.Random5(1, 100) <= 10))
							{
								gameObject.TemperatureChange(150, this.ParentObject, false, false, false, false, phase, null, null);
							}
						}
						i++;
					}
				}
			}
			if (!this.ExtinguishBlueprint.IsNullOrEmpty() && this.ParentObject.IsFrozen())
			{
				this.Extinguish(null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B87A RID: 47226 RVA: 0x00431A9C File Offset: 0x0042FC9C
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			E.AddAction("Cook", "cook", "Cook", null, 'c', false, 10, 0, false, false, false, false, true, null, false);
			if (!this.ExtinguishBlueprint.IsNullOrEmpty())
			{
				E.AddAction("Extinguish", "extinguish", "ExtinguishCampfire", null, 'x', false, 0, 0, false, false, true, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x0600B87B RID: 47227 RVA: 0x00431B04 File Offset: 0x0042FD04
		public List<CookingRecipe> presetMeals
		{
			get
			{
				List<CookingRecipe> list;
				if ((list = this._presetMeals) == null)
				{
					list = (this._presetMeals = this.ParsePresetMeals());
				}
				return list;
			}
		}

		// Token: 0x0600B87C RID: 47228 RVA: 0x00431B2C File Offset: 0x0042FD2C
		protected List<CookingRecipe> ParsePresetMeals()
		{
			List<CookingRecipe> list = new List<CookingRecipe>();
			if (!this.PresetMeals.IsNullOrEmpty())
			{
				foreach (string text in this.PresetMeals.Split(',', StringSplitOptions.None))
				{
					CookingRecipe cookingRecipe = Activator.CreateInstance(ModManager.ResolveType("XRL.World.Skills.Cooking." + text, false, false, true)) as CookingRecipe;
					list.Add(cookingRecipe);
				}
			}
			else if (this.specificProcgenMeals != null)
			{
				list.AddRange(this.specificProcgenMeals);
			}
			return list;
		}

		// Token: 0x0600B87D RID: 47229 RVA: 0x00431BAB File Offset: 0x0042FDAB
		public static string EnabledDisplay(bool Enabled, string Display)
		{
			return (Enabled ? "" : "&K") + Display;
		}

		// Token: 0x0600B87E RID: 47230 RVA: 0x00431BC4 File Offset: 0x0042FDC4
		public override bool HandleEvent(GetCookingActionsEvent E)
		{
			bool flag = CookingGameState.instance.knownRecipies.Count > 0;
			bool flag2 = Campfire.IsHungry(The.Player);
			bool flag3 = The.Player.HasSkill("CookingAndGathering");
			List<GameObject> inventoryDirect = The.Player.GetInventoryDirect((GameObject go) => go.HasPart<PreservableItem>() && !go.HasTag("ChooseToPreserve") && !go.IsTemporary && !go.IsImportant());
			List<GameObject> inventoryDirect2 = The.Player.GetInventoryDirect((GameObject go) => go.HasPart<PreservableItem>() && go.HasTag("ChooseToPreserve") && !go.IsTemporary && go.Understood());
			string text4;
			if (this.presetMeals.Count > 0)
			{
				for (int i = 0; i < this.presetMeals.Count; i++)
				{
					CookingRecipe cookingRecipe = this.presetMeals[i];
					string text;
					if ((text = this.ParentObject.GetTag("PresetMealMessage", null)) == null)
					{
						text = "Eat " + ((cookingRecipe != null) ? cookingRecipe.GetDisplayName().Replace("&W", "&Y").Replace("{{W|", "{{Y|") : null);
					}
					string text2 = text;
					string text3 = Campfire.COOK_COMMAND_PRESET_MEAL + i.ToString();
					text4 = Campfire.COOK_COMMAND_PRESET_MEAL + i.ToString();
					char c = ((i >= 5) ? ' ' : ((char)(97 + i)));
					E.AddAction(text3, Campfire.EnabledDisplay(flag2, text2), text4, null, c, false, 100 + this.presetMeals.Count - i, 100 + this.presetMeals.Count - i, false, false, false, false, true, null, false);
				}
			}
			string cook_COMMAND_WHIP_UP = Campfire.COOK_COMMAND_WHIP_UP;
			text4 = Campfire.COOK_COMMAND_WHIP_UP;
			E.AddAction(cook_COMMAND_WHIP_UP, Campfire.EnabledDisplay(flag2, "Whip up a meal."), text4, null, 'm', false, 10, 90, false, false, false, false, true, null, false);
			E.AddAction(Campfire.COOK_COMMAND_CHOOSE, Campfire.EnabledDisplay(flag3 && flag2, "Choose ingredients to cook with."), Campfire.COOK_COMMAND_CHOOSE, null, 'i', false, 0, 80, false, false, false, false, true, null, false);
			E.AddAction(Campfire.COOK_COMMAND_RECIPE, Campfire.EnabledDisplay(flag3 && flag && flag2, "Cook from a recipe."), Campfire.COOK_COMMAND_RECIPE, null, 'r', false, 0, 70, false, false, false, false, true, null, false);
			E.AddAction(Campfire.COOK_COMMAND_PRESERVE, Campfire.EnabledDisplay(flag3 && inventoryDirect.Count > 0, "Preserve your fresh foods."), Campfire.COOK_COMMAND_PRESERVE, null, 'f', false, 0, 60, false, false, false, false, true, null, false);
			if (flag3 && inventoryDirect2.Count > 0)
			{
				E.AddAction(Campfire.COOK_COMMAND_PRESERVE_EXOTIC, Campfire.EnabledDisplay(true, "Preserve your exotic foods."), Campfire.COOK_COMMAND_PRESERVE_EXOTIC, null, 'x', false, 0, 50, false, false, false, false, true, null, false);
			}
			if (The.Player.HasSkill("Physic_Nostrums"))
			{
				E.AddAction(Campfire.COOK_COMMAND_STOP_BLEEDING, Campfire.EnabledDisplay(this.NostrumsCanStopBleeding(), "Stop bleeding."), Campfire.COOK_COMMAND_STOP_BLEEDING, null, 'b', false, 0, 40, false, false, false, false, true, null, false);
				E.AddAction(Campfire.COOK_COMMAND_TREAT_POISON, Campfire.EnabledDisplay(this.NostrumsCanTreatPoison(), "Treat poison."), Campfire.COOK_COMMAND_TREAT_POISON, null, 'p', false, 0, 30, false, false, false, false, true, null, false);
				E.AddAction(Campfire.COOK_COMMAND_TREAT_ILLNESS, Campfire.EnabledDisplay(this.NostrumsCanTreatIllness(), "Treat illness."), Campfire.COOK_COMMAND_TREAT_ILLNESS, null, 'l', false, 0, 20, false, false, false, false, true, null, false);
				E.AddAction(Campfire.COOK_COMMAND_TREAT_DISEASE_ONSET, Campfire.EnabledDisplay(this.NostrumsCanTreatDiseaseOnset(), "Treat disease onset."), Campfire.COOK_COMMAND_TREAT_DISEASE_ONSET, null, 'd', false, 0, 10, false, false, false, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B87F RID: 47231 RVA: 0x00431F24 File Offset: 0x00430124
		public override bool HandleEvent(InventoryActionEvent E)
		{
			bool flag;
			try
			{
				if (E.Command == "Cook")
				{
					if (!this.Cook())
					{
						return false;
					}
				}
				else if (E.Command == "ExtinguishCampfire")
				{
					if (!this.ExtinguishBlueprint.IsNullOrEmpty() && E.Actor.CheckFrozen(false, true, false, null))
					{
						this.Extinguish(E.Actor, null);
					}
				}
				else
				{
					if (E.Command == Campfire.COOK_COMMAND_WHIP_UP)
					{
						if (this.CookFromIngredients(true))
						{
							E.RequestInterfaceExit();
						}
						return true;
					}
					if (E.Command == Campfire.COOK_COMMAND_CHOOSE)
					{
						if (this.CookFromIngredients(false))
						{
							E.RequestInterfaceExit();
						}
						return true;
					}
					if (E.Command == Campfire.COOK_COMMAND_RECIPE)
					{
						if (this.CookFromRecipe())
						{
							E.RequestInterfaceExit();
						}
						return true;
					}
					if (E.Command == Campfire.COOK_COMMAND_PRESERVE)
					{
						this.Preserve();
						return true;
					}
					if (E.Command == Campfire.COOK_COMMAND_PRESERVE_EXOTIC)
					{
						this.PreserveExotic();
						return true;
					}
					if (E.Command.StartsWith(Campfire.COOK_COMMAND_PRESET_MEAL))
					{
						int num = Convert.ToInt32(E.Command.Substring(Campfire.COOK_COMMAND_PRESET_MEAL.Length));
						this.CookPresetMeal(num);
						E.RequestInterfaceExit();
						return true;
					}
					if (E.Command == Campfire.COOK_COMMAND_STOP_BLEEDING)
					{
						this.NostrumsStopBleeding();
						return true;
					}
					if (E.Command == Campfire.COOK_COMMAND_TREAT_POISON)
					{
						this.NostrumsTreatPoison();
						return true;
					}
					if (E.Command == Campfire.COOK_COMMAND_TREAT_ILLNESS)
					{
						this.NostrumsTreatIllness();
						return true;
					}
					if (E.Command == Campfire.COOK_COMMAND_TREAT_DISEASE_ONSET)
					{
						this.NostrumsTreatDiseaseOnset();
						return true;
					}
				}
				flag = base.HandleEvent(E);
			}
			finally
			{
				AbilityBar.UpdateActiveEffects();
			}
			return flag;
		}

		// Token: 0x0600B880 RID: 47232 RVA: 0x00432134 File Offset: 0x00430334
		public bool IsHalfIngredient(string type)
		{
			GameObjectBlueprint gameObjectBlueprint = GameObjectFactory.Factory.Blueprints["ProceduralCookingIngredient_" + type];
			return gameObjectBlueprint.GetTag("Triggers", "").IsNullOrEmpty() && gameObjectBlueprint.GetTag("Actions", "").IsNullOrEmpty();
		}

		// Token: 0x0600B881 RID: 47233 RVA: 0x0043218D File Offset: 0x0043038D
		public static bool HasTriggers(string type)
		{
			return !GameObjectFactory.Factory.Blueprints["ProceduralCookingIngredient_" + type].GetTag("Triggers", "").IsNullOrEmpty();
		}

		// Token: 0x0600B882 RID: 47234 RVA: 0x004321C2 File Offset: 0x004303C2
		public static bool HasActions(string type)
		{
			return !GameObjectFactory.Factory.Blueprints["ProceduralCookingIngredient_" + type].GetTag("Actions", "").IsNullOrEmpty();
		}

		// Token: 0x0600B883 RID: 47235 RVA: 0x004321F7 File Offset: 0x004303F7
		public static bool HasUnits(string type)
		{
			return !GameObjectFactory.Factory.Blueprints["ProceduralCookingIngredient_" + type].GetTag("Units", "").IsNullOrEmpty();
		}

		// Token: 0x0600B884 RID: 47236 RVA: 0x0043222C File Offset: 0x0043042C
		public ProceduralCookingEffect GenerateEffectFromTypeList(List<string> selectedIngredientTypes)
		{
			ProceduralCookingEffect proceduralCookingEffect = null;
			IEnumerable<string[]> enumerable = Algorithms.GeneratePermutations<string>(selectedIngredientTypes);
			List<List<string>> list = new List<List<string>>();
			foreach (string[] array in enumerable)
			{
				List<string> list2 = new List<string>();
				foreach (string text in array)
				{
					list2.Add(text);
				}
				list.Add(list2);
			}
			list.Add(null);
			list.ShuffleInPlace<List<string>>(null);
			for (int j = 0; j < list.Count; j++)
			{
				if (list[j] != null && list[j].Count == 0)
				{
					throw new InvalidOperationException("There weren't any types in the selected ingredient type permutations? (should be impossible)");
				}
				if (list[j] == null || list[j].Count == 1)
				{
					proceduralCookingEffect = ProceduralCookingEffect.CreateJustUnits(selectedIngredientTypes);
					break;
				}
				if (list[j].Count == 2)
				{
					if (Campfire.HasTriggers(list[j][0]) && Campfire.HasActions(list[j][1]))
					{
						proceduralCookingEffect = ProceduralCookingEffect.CreateTriggeredAction(list[j][0], list[j][1]);
						break;
					}
				}
				else if (Campfire.HasUnits(list[j][0]) && Campfire.HasTriggers(list[j][1]) && Campfire.HasActions(list[j][2]))
				{
					proceduralCookingEffect = ProceduralCookingEffect.CreateBaseAndTriggeredAction(list[j][0], list[j][1], list[j][2]);
					break;
				}
			}
			return proceduralCookingEffect;
		}

		// Token: 0x0600B885 RID: 47237 RVA: 0x004323F8 File Offset: 0x004305F8
		public List<ProceduralCookingEffect> GenerateEffectsFromTypeList(List<string> selectedIngredientTypes, int n)
		{
			List<ProceduralCookingEffect> list = new List<ProceduralCookingEffect>();
			IEnumerable<string[]> enumerable = Algorithms.GeneratePermutations<string>(selectedIngredientTypes);
			List<List<string>> list2 = new List<List<string>>();
			foreach (string[] array in enumerable)
			{
				List<string> list3 = new List<string>();
				foreach (string text in array)
				{
					list3.Add(text);
				}
				list2.Add(list3);
			}
			list2.Add(null);
			list2.ShuffleInPlace<List<string>>(null);
			for (int j = 0; j < list2.Count; j++)
			{
				if (list2[j] != null && list2[j].Count == 0)
				{
					throw new InvalidOperationException("There weren't any types in the selected ingredient type permutations? (should be impossible)");
				}
				if (list2[j] == null || list2[j].Count == 1)
				{
					ProceduralCookingEffect effect2 = ProceduralCookingEffect.CreateJustUnits(selectedIngredientTypes);
					if (!list.Any<ProceduralCookingEffect>((ProceduralCookingEffect e) => e.SameAs(effect2)))
					{
						list.Add(effect2);
						n--;
						if (n <= 0)
						{
							return list;
						}
					}
				}
				else if (list2[j].Count == 2)
				{
					if (Campfire.HasTriggers(list2[j][0]) && Campfire.HasActions(list2[j][1]))
					{
						ProceduralCookingEffectWithTrigger effect3 = ProceduralCookingEffect.CreateTriggeredAction(list2[j][0], list2[j][1]);
						if (!list.Any<ProceduralCookingEffect>((ProceduralCookingEffect e) => e.SameAs(effect3)))
						{
							list.Add(effect3);
							n--;
							if (n <= 0)
							{
								return list;
							}
						}
					}
				}
				else
				{
					if (Campfire.HasUnits(list2[j][0]) && Campfire.HasTriggers(list2[j][1]) && Campfire.HasActions(list2[j][2]))
					{
						ProceduralCookingEffectWithTrigger effect = ProceduralCookingEffect.CreateBaseAndTriggeredAction(list2[j][0], list2[j][1], list2[j][2]);
						if (!list.Any<ProceduralCookingEffect>((ProceduralCookingEffect e) => e.SameAs(effect)))
						{
							list.Add(effect);
							n--;
							if (n <= 0)
							{
								return list;
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600B886 RID: 47238 RVA: 0x0043267C File Offset: 0x0043087C
		private static void MakeFromStoredByPlayer(GameObject obj)
		{
			obj.SetIntProperty("FromStoredByPlayer", 1, false);
		}

		// Token: 0x0600B887 RID: 47239 RVA: 0x0043268C File Offset: 0x0043088C
		public static bool PerformPreserve(GameObject go, StringBuilder sb, GameObject who, bool Capitalize = true, bool Single = false)
		{
			bool flag = go.GetIntProperty("StoredByPlayer", 0) > 0;
			Action<GameObject> action = null;
			if (flag)
			{
				action = new Action<GameObject>(Campfire.MakeFromStoredByPlayer);
			}
			int num = go.Count;
			if (Single && num > 1)
			{
				go = go.RemoveOne();
				num = go.Count;
			}
			if (num == 1)
			{
				sb.Append(Capitalize ? go.A : go.a).Append(go.DisplayNameOnlyDirect);
			}
			else if (go.IsPlural)
			{
				sb.Append(num).Append(' ').Append(go.DisplayNameOnlyDirect);
			}
			else
			{
				sb.Append(num).Append(' ').Append(Grammar.Pluralize(go.DisplayNameOnlyDirect));
			}
			sb.Append(" into ");
			string result = go.GetPart<PreservableItem>().Result;
			int num2 = 0;
			PreservableItem part = go.GetPart<PreservableItem>();
			PreparedCookingIngredient part2 = go.GetPart<PreparedCookingIngredient>();
			int num3 = 1;
			if (part2 != null)
			{
				num3 = part2.charges;
			}
			if (part != null)
			{
				num3 = part.Number;
			}
			num3 *= go.Count;
			go.Obliterate(null, false, null);
			string result2 = part.Result;
			int num4 = num3;
			bool flag2 = false;
			bool flag3 = true;
			Action<GameObject> action2 = action;
			who.TakeObject(result2, num4, flag2, flag3, new int?(0), null, 0, 0, null, null, null, action2);
			num2 += num3;
			GameObject gameObject = GameObject.CreateSample(result);
			string tagOrStringProperty = gameObject.GetTagOrStringProperty("ServingType", "serving");
			string tagOrStringProperty2 = gameObject.GetTagOrStringProperty("ServingName", gameObject.DisplayNameOnlyDirect);
			sb.Append(num2).Append(' ').Append((num2 == 1) ? tagOrStringProperty : Grammar.Pluralize(tagOrStringProperty))
				.Append(" of ")
				.Append(tagOrStringProperty2);
			return true;
		}

		// Token: 0x0600B888 RID: 47240 RVA: 0x00432828 File Offset: 0x00430A28
		public static bool IsValidCookingIngredient(GameObject obj)
		{
			try
			{
				if (obj.IsTemporary && !obj.HasPropertyOrTag("CanCookTemporary"))
				{
					return false;
				}
				if (obj.IsImportant())
				{
					return false;
				}
				if (obj.HasPart<PreparedCookingIngredient>())
				{
					return true;
				}
				LiquidVolume liquidVolume = obj.LiquidVolume;
				if (liquidVolume != null && !liquidVolume.EffectivelySealed() && !liquidVolume.GetPreparedCookingIngredient().IsNullOrEmpty() && obj.GetEpistemicStatus() != 0)
				{
					return true;
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("Campfire::IsValidCookingIngredient", ex, "game_exception");
			}
			return false;
		}

		// Token: 0x0600B889 RID: 47241 RVA: 0x004328BC File Offset: 0x00430ABC
		public static List<GameObject> GetValidCookingIngredients(GameObject who)
		{
			return who.GetInventoryDirectAndEquipmentAndAdjacentCells(new Predicate<GameObject>(Campfire.IsValidCookingIngredient));
		}

		// Token: 0x0600B88A RID: 47242 RVA: 0x004328D0 File Offset: 0x00430AD0
		public static List<GameObject> GetValidCookingIngredients()
		{
			return Campfire.GetValidCookingIngredients(The.Player);
		}

		// Token: 0x0600B88B RID: 47243 RVA: 0x004328DC File Offset: 0x00430ADC
		public static bool IsHungry(GameObject Object)
		{
			Stomach stomach;
			return Object.TryGetPart<Stomach>(out stomach) && (stomach.HungerLevel > 0 || stomach.CookCount < 3);
		}

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x0600B88C RID: 47244 RVA: 0x00432909 File Offset: 0x00430B09
		public static Stomach pStomach
		{
			get
			{
				return The.Player.GetPart<Stomach>();
			}
		}

		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x0600B88D RID: 47245 RVA: 0x00432915 File Offset: 0x00430B15
		public static bool hasSkill
		{
			get
			{
				return The.Player.HasSkill("CookingAndGathering");
			}
		}

		// Token: 0x0600B88E RID: 47246 RVA: 0x00432928 File Offset: 0x00430B28
		public bool Preserve()
		{
			if (!Campfire.hasSkill)
			{
				Popup.Show("You don't have the Cooking and Gathering skill.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				return false;
			}
			List<GameObject> inventoryDirect = The.Player.GetInventoryDirect((GameObject go) => go.HasPart<PreservableItem>() && !go.HasTag("ChooseToPreserve") && !go.IsTemporary && !go.IsImportant());
			if (inventoryDirect.Count == 0)
			{
				Popup.Show("You don't have anything to preserve.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				return false;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("You preserved:\n\n");
			int num = 0;
			IComponent<GameObject>.PlayUISound("Sounds/Interact/sfx_interact_preserve_foods", 1f, false, SoundRequest.SoundEffectType.None);
			int i = 0;
			int count = inventoryDirect.Count;
			while (i < count)
			{
				Campfire.PerformPreserve(inventoryDirect[i], stringBuilder, The.Player, true, false);
				stringBuilder.Append('.');
				if (i < count - 1)
				{
					stringBuilder.Append('\n');
				}
				num++;
				i++;
			}
			Popup.Show(stringBuilder.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
			return true;
		}

		// Token: 0x0600B88F RID: 47247 RVA: 0x00432A20 File Offset: 0x00430C20
		public bool PreserveExotic()
		{
			if (!Campfire.hasSkill)
			{
				Popup.Show("You don't have the Cooking and Gathering skill.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				return false;
			}
			List<GameObject> list = The.Player.GetInventoryDirect((GameObject go) => go.HasPart<PreservableItem>() && go.HasTag("ChooseToPreserve") && !go.IsTemporary && go.Understood());
			if (list.Count == 0)
			{
				Popup.Show("You don't have anything to preserve.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				return false;
			}
			while (list.Count != 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				int num = 0;
				bool[] array = new bool[list.Count];
				string[] array2 = new string[list.Count];
				IRenderable[] array3 = new IRenderable[list.Count];
				int num2 = 0;
				foreach (GameObject gameObject in list)
				{
					array[num2] = false;
					array2[num2] = gameObject.DisplayName;
					array3[num2] = gameObject.RenderForUI(null, false);
					num2++;
				}
				int num3 = Popup.PickOption("Choose exotic foods to preserve.", null, "", "Sounds/UI/ui_notification", array2, null, array3, null, null, null, null, 0, 60, num, -1, true, false, false, true, false, null, null);
				if (num3 < 0)
				{
					return true;
				}
				GameObject gameObject2 = list[num3];
				if (gameObject2.ConfirmUseImportant(null, "preserve", null, -1))
				{
					int num4 = 1;
					if (gameObject2.Count > 1)
					{
						int? num5 = Popup.AskNumber(gameObject2.DisplayNameOnlyDirect + ": how many do you want to preserve? (max = " + gameObject2.Count.ToString() + ")", "Sounds/UI/ui_notification", "", gameObject2.Count, 0, gameObject2.Count);
						if (num5 == null)
						{
							goto IL_023F;
						}
						int? num6 = num5;
						int num7 = 0;
						if ((num6.GetValueOrDefault() == num7) & (num6 != null))
						{
							goto IL_023F;
						}
						try
						{
							num4 = Convert.ToInt32(num5);
							if (num4 > gameObject2.Count)
							{
								num4 = gameObject2.Count;
							}
						}
						catch
						{
							goto IL_023F;
						}
						if (num4 <= 0)
						{
							goto IL_023F;
						}
					}
					stringBuilder.Append("You preserved:\n\n");
					IComponent<GameObject>.PlayUISound("Sounds/Interact/sfx_interact_preserve_exotic", 1f, false, SoundRequest.SoundEffectType.None);
					Campfire.PerformPreserve(gameObject2.Split(num4, true), stringBuilder, The.Player, true, false);
					stringBuilder.Append('.');
					Popup.Show(stringBuilder.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				IL_023F:
				list = The.Player.GetInventoryDirect((GameObject go) => go.HasPart<PreservableItem>() && go.HasTag("ChooseToPreserve") && !go.IsTemporary && go.Understood());
			}
			return true;
		}

		// Token: 0x0600B890 RID: 47248 RVA: 0x00432CC0 File Offset: 0x00430EC0
		public void AfterCooked()
		{
			Event @event = Event.New("CookedAt", "Actor", The.Player, "Object", this.ParentObject);
			The.Player.FireEvent(@event);
			this.ParentObject.FireEvent(@event);
		}

		// Token: 0x0600B891 RID: 47249 RVA: 0x00432D08 File Offset: 0x00430F08
		public bool CookPresetMeal(int index)
		{
			if (!Campfire.IsHungry(The.Player))
			{
				Popup.Show("You aren't hungry. Instead, you relax by the warmth of the fire.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				return false;
			}
			IComponent<GameObject>.PlayUISound("Human_Eating", 1f, false, SoundRequest.SoundEffectType.None);
			Popup.Show(HistoricStringExpander.ExpandString("<spice.cooking.ate.!random>", null), null, "Sounds/UI/ui_notification", true, true, true, true, null);
			The.Player.FireEvent("ClearFoodEffects");
			The.Player.CleanEffects();
			CookingRecipe cookingRecipe = this.presetMeals[index];
			if (The.Player.HasPart(typeof(Carnivorous)) && (cookingRecipe.HasPlants() || cookingRecipe.HasFungi()))
			{
				if (50.in100())
				{
					Popup.Show("Ugh, you feel sick.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					The.Player.ApplyEffect(new Ill(100, 1, null, false, false), null);
				}
			}
			else
			{
				cookingRecipe.ApplyEffectsTo(The.Player, true);
				Campfire.ClearHunger();
			}
			Campfire.pStomach.CookCount++;
			this.AfterCooked();
			return true;
		}

		// Token: 0x0600B892 RID: 47250 RVA: 0x00432E14 File Offset: 0x00431014
		public static int IngredientSort(GameObject a, GameObject b)
		{
			LiquidVolume liquidVolume = a.LiquidVolume;
			LiquidVolume liquidVolume2 = b.LiquidVolume;
			string text = ((liquidVolume != null) ? liquidVolume.GetPreparedCookingIngredient() : null);
			string text2 = ((liquidVolume2 != null) ? liquidVolume2.GetPreparedCookingIngredient() : null);
			if (!text.IsNullOrEmpty() && !text2.IsNullOrEmpty())
			{
				int num = ProducesLiquidEvent.Check(a, text).CompareTo(ProducesLiquidEvent.Check(b, text2));
				if (num != 0)
				{
					return -num;
				}
				int num2 = WantsLiquidCollectionEvent.Check(a, The.Player, text).CompareTo(WantsLiquidCollectionEvent.Check(b, The.Player, text2));
				if (num2 != 0)
				{
					return num2;
				}
				int num3 = liquidVolume.Volume.CompareTo(liquidVolume2.Volume);
				if (num3 != 0)
				{
					return -num3;
				}
			}
			int num4 = a.HasTagOrProperty("WaterContainer").CompareTo(b.HasTagOrProperty("WaterContainer"));
			if (num4 != 0)
			{
				return -num4;
			}
			int num5 = a.ValueEach.CompareTo(b.ValueEach);
			if (num5 != 0)
			{
				return num5;
			}
			return a.Blueprint.CompareTo(b.Blueprint);
		}

		// Token: 0x0600B893 RID: 47251 RVA: 0x00432F18 File Offset: 0x00431118
		public bool CookFromIngredients(bool random)
		{
			if (!Campfire.IsHungry(The.Player))
			{
				Popup.Show("You aren't hungry. Instead, you relax by the warmth of the fire.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				return false;
			}
			bool flag = Campfire.pStomach.HungerLevel > 0;
			int num = 0;
			List<GameObject> validCookingIngredients = Campfire.GetValidCookingIngredients();
			validCookingIngredients.Sort(new Comparison<GameObject>(Campfire.IngredientSort));
			List<ValueTuple<int, GameObject, string>> list = new List<ValueTuple<int, GameObject, string>>(validCookingIngredients.Count);
			StringBuilder stringBuilder = new StringBuilder();
			foreach (GameObject gameObject in validCookingIngredients)
			{
				Campfire.<>c__DisplayClass52_0 CS$<>8__locals1 = new Campfire.<>c__DisplayClass52_0();
				if (!The.Player.HasPart<Carnivorous>() || (!gameObject.HasTag("Plant") && !gameObject.HasTag("Fungus")))
				{
					Campfire.<>c__DisplayClass52_0 CS$<>8__locals2 = CS$<>8__locals1;
					LiquidVolume liquidVolume = gameObject.LiquidVolume;
					CS$<>8__locals2.liquidIngredient1 = ((liquidVolume != null) ? liquidVolume.GetPreparedCookingIngredient() : null);
					LiquidVolume liquidVolume2 = gameObject.LiquidVolume;
					int num2 = ((liquidVolume2 != null) ? liquidVolume2.Volume : gameObject.Count);
					if (gameObject.LiquidVolume == null)
					{
						gameObject.Count = 1;
						CS$<>8__locals1.name = gameObject.GetDisplayName(1120, null, null, false, false, false, false, false, false, true, false, false, false, false, false, false, null, false, false, false, false, null, false, null, false, true);
						gameObject.Count = num2;
					}
					else
					{
						stringBuilder.Clear();
						gameObject.LiquidVolume.AppendLiquidName(stringBuilder, false);
						CS$<>8__locals1.name = "a dram of " + stringBuilder.ToString();
					}
					int num3 = list.FindIndex(delegate([TupleElementNames(new string[] { "Count", "go", "name" })] ValueTuple<int, GameObject, string> o)
					{
						LiquidVolume liquidVolume4 = o.Item2.LiquidVolume;
						string text4 = ((liquidVolume4 != null) ? liquidVolume4.GetPreparedCookingIngredient() : null);
						if (!CS$<>8__locals1.liquidIngredient1.IsNullOrEmpty() && !text4.IsNullOrEmpty())
						{
							return CS$<>8__locals1.liquidIngredient1 == text4;
						}
						return o.Item3 == CS$<>8__locals1.name;
					});
					if (num3 != -1)
					{
						ValueTuple<int, GameObject, string> valueTuple = list[num3];
						list.Remove(valueTuple);
						valueTuple.Item1 += num2;
						list.Add(valueTuple);
					}
					else
					{
						list.Add(new ValueTuple<int, GameObject, string>(num2, gameObject, CS$<>8__locals1.name));
					}
				}
			}
			foreach (GameObject gameObject2 in validCookingIngredients)
			{
				gameObject2.ResetNameCache();
			}
			int num4 = 0;
			int num5 = 3;
			List<string> list2 = new List<string>();
			List<GameObject> list3 = Event.NewGameObjectList();
			List<GameObject> list4 = Event.NewGameObjectList();
			if (!random && !The.Player.HasSkill("CookingAndGathering"))
			{
				Popup.Show("You don't have the Cooking and Gathering skill.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				return false;
			}
			if (random)
			{
				if (The.Player.HasSkill("CookingAndGathering"))
				{
					goto IL_06DD;
				}
				List<string> list5 = new List<string>();
				list.ShuffleInPlace<ValueTuple<int, GameObject, string>>(null);
				using (List<ValueTuple<int, GameObject, string>>.Enumerator enumerator2 = list.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						ValueTuple<int, GameObject, string> valueTuple2 = enumerator2.Current;
						GameObject item = valueTuple2.Item2;
						using (ScopeDisposedList<string> fromPool = ScopeDisposedList<string>.GetFromPool())
						{
							PreparedCookingIngredient preparedCookingIngredient;
							if (item.TryGetPart<PreparedCookingIngredient>(out preparedCookingIngredient))
							{
								preparedCookingIngredient.GetTypeOptions(fromPool);
							}
							else
							{
								fromPool.AddRange(item.LiquidVolume.GetPreparedCookingIngredient().CachedCommaExpansion());
							}
							if (fromPool.Count > 0)
							{
								fromPool.ShuffleInPlace<string>(null);
								if (!list5.Contains(fromPool[0]) && num4.in100() && !list2.Contains(fromPool[0]))
								{
									list2.Add(fromPool[0]);
									list3.Add(item);
									if (list2.Count >= num5)
									{
										break;
									}
								}
								list5.Add(fromPool[0]);
							}
						}
					}
					goto IL_06DD;
				}
			}
			int num6 = 2;
			if (The.Player.HasSkill("CookingAndGathering_Spicer"))
			{
				num6++;
			}
			int num7 = 0;
			List<bool> list6 = new List<bool>();
			int num8 = 0;
			list.Sort(([TupleElementNames(new string[] { "Count", "go", "name" })] ValueTuple<int, GameObject, string> a, [TupleElementNames(new string[] { "Count", "go", "name" })] ValueTuple<int, GameObject, string> b) => ConsoleLib.Console.ColorUtility.CompareExceptFormattingAndCase(a.Item3, b.Item3));
			foreach (ValueTuple<int, GameObject, string> valueTuple3 in list)
			{
				list6.Add(false);
			}
			for (;;)
			{
				List<string> list7 = new List<string>();
				List<IRenderable> list8 = new List<IRenderable>();
				string text = "C";
				if (num8 > num6)
				{
					text = "R";
				}
				string text2 = "";
				if (num8 < num6)
				{
					text2 = "\n{{y|[up to " + (num6 - num8).ToString() + " remaining]}}";
				}
				else if (num8 == num6)
				{
					text2 = "\n{{y|[0 remaining]}}";
				}
				list7.Add(string.Concat(new string[]
				{
					"{{W|Cook with the {{",
					text,
					"|",
					num8.ToString(),
					"}} selected ingredients.}}",
					text2
				}));
				list8.Add(null);
				int num9 = 0;
				foreach (ValueTuple<int, GameObject, string> valueTuple4 in list)
				{
					int item2 = valueTuple4.Item1;
					GameObject item3 = valueTuple4.Item2;
					string item4 = valueTuple4.Item3;
					string text3 = string.Format(" {{{{K|x{0}}}}}", item2);
					if (list6[num9])
					{
						list7.Add("{{y|[{{G|X}}]}}   " + item4 + text3);
					}
					else
					{
						list7.Add("[ ]   " + item4 + text3);
					}
					list8.Add(item3.RenderForUI(null, false));
					num9++;
				}
				int num10 = Popup.PickOption("Choose ingredients to cook with.", null, "", "Sounds/UI/ui_notification", list7.ToArray(), null, list8.ToArray(), null, null, null, null, 0, 60, num7, 6, true, false, false, true, false, null, null);
				if (num10 < 0)
				{
					break;
				}
				if (num10 > 0)
				{
					num7 = num10;
					list6[num10 - 1] = !list6[num10 - 1];
					if (list6[num10 - 1])
					{
						num8++;
					}
					else
					{
						num8--;
					}
				}
				else if (num10 != 0 || num8 <= num6)
				{
					goto IL_05BD;
				}
			}
			return false;
			IL_05BD:
			for (int i = 0; i < list6.Count; i++)
			{
				if (list6[i])
				{
					GameObject gameObject3 = null;
					PreparedCookingIngredient preparedCookingIngredient2;
					string type;
					if (list[i].Item2.TryGetPart<PreparedCookingIngredient>(out preparedCookingIngredient2))
					{
						type = preparedCookingIngredient2.GetTypeInstanceAvoidingList(list2);
						if (preparedCookingIngredient2.type == "random" || preparedCookingIngredient2.type == "randomHighTier")
						{
							gameObject3 = EncountersAPI.GetAnObjectNoExclusions((GameObjectBlueprint ob) => EncountersAPI.IsEligibleForDynamicEncounters(ob) && (ob.GetPartParameter<string>("PreparedCookingIngredient", "type", "").Contains(type) || (ob.HasTag("LiquidCookingIngredient", false) && ob.createSample().LiquidVolume.GetPreparedCookingIngredient().Contains(type))));
						}
					}
					else
					{
						type = list[i].Item2.LiquidVolume.GetPreparedCookingIngredient().Split(',', StringSplitOptions.None).GetRandomElement<string>(null);
					}
					if (type != null && !list2.Contains(type))
					{
						list2.Add(type);
					}
					list3.Add(list[i].Item2);
					if (gameObject3 != null)
					{
						list4.Add(gameObject3);
					}
					else
					{
						list4.Add(list[i].Item2);
					}
				}
			}
			IL_06DD:
			if (list2.Count > 0)
			{
				The.Player.FireEvent("ClearFoodEffects");
				The.Player.CleanEffects();
				ProceduralCookingEffect proceduralCookingEffect2;
				if (!random && The.Player.HasEffect<Inspired>())
				{
					List<ProceduralCookingEffect> list9 = this.GenerateEffectsFromTypeList(list2, 3);
					List<string> list10 = new List<string>();
					foreach (ProceduralCookingEffect proceduralCookingEffect in list9)
					{
						list10.Add(Campfire.ProcessEffectDescription(proceduralCookingEffect.GetTemplatedProceduralEffectDescription(), The.Player));
					}
					Popup.Show(Campfire.DescribeMeal(list3), null, "Sounds/UI/ui_notification", true, true, true, true, null);
					if (flag)
					{
						if (!Campfire.RollTasty(num, The.Player.HasPart<Carnivorous>(), Campfire.ForceTastyBasedOnIngredients(list2)))
						{
							Popup.Show(HistoricStringExpander.ExpandString("<spice.cooking.ate.!random>", null), null, "Sounds/UI/ui_notification", true, true, true, true, null);
						}
					}
					else
					{
						IComponent<GameObject>.PlayUISound("Human_Eating", 1f, false, SoundRequest.SoundEffectType.None);
						Popup.Show(HistoricStringExpander.ExpandString("<spice.cooking.ate.!random>", null), null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					IComponent<GameObject>.PlayUISound("HarmonicaRiff_GoodFood_3", 1f, false, SoundRequest.SoundEffectType.None);
					int num11 = Popup.PickOption("You let inspiration guide you toward a mouthwatering dish.", null, "", "Sounds/UI/ui_notification", list10.ToArray(), null, null, null, null, null, null, 1, 60, 0, -1, false, false, false, true, false, null, null);
					proceduralCookingEffect2 = list9[num11];
					CookingRecipe cookingRecipe = CookingRecipe.FromIngredients(list4, proceduralCookingEffect2, The.Player.BaseDisplayName, "generic");
					CookingGameState.LearnRecipe(cookingRecipe, The.Player);
					Popup.Show("You create a new recipe for {{|" + cookingRecipe.GetDisplayName() + "}}!", null, "sfx_cookingRecipe_create", true, true, true, true, null);
					JournalAPI.AddAccomplishment("You invented a mouthwatering dish called {{|" + cookingRecipe.GetDisplayName() + "}}.", "In a moment of divine inspiration, the Carbide Chef =name= invented the mouthwatering dish called {{|" + cookingRecipe.GetDisplayName() + "}}.", string.Concat(new string[]
					{
						"At a remote tavern near ",
						JournalAPI.GetLandmarkNearestPlayer().Text,
						", =name= met with a group of kippers and, divinely inspired by <spice.elements.",
						The.Player.GetMythicDomain(5, null),
						".practices.!random>, invented the dish called ",
						cookingRecipe.GetDisplayName(),
						"."
					}), null, "general", MuralCategory.CreatesSomething, MuralWeight.Low, null, -1L, true);
					Achievement.RECIPES_100.Progress.Increment(1);
					The.Player.RemoveEffect<Inspired>();
					this.AfterCooked();
				}
				else
				{
					proceduralCookingEffect2 = this.GenerateEffectFromTypeList(list2);
					Popup.Show(Campfire.DescribeMeal(list3), null, "Sounds/UI/ui_notification", true, true, true, true, null);
					if (flag)
					{
						if (!Campfire.RollTasty(num, The.Player.HasPart<Carnivorous>(), Campfire.ForceTastyBasedOnIngredients(list2)))
						{
							Popup.Show(HistoricStringExpander.ExpandString("<spice.cooking.ate.!random>", null), null, "Sounds/UI/ui_notification", true, true, true, true, null);
						}
					}
					else
					{
						IComponent<GameObject>.PlayUISound("Human_Eating", 1f, false, SoundRequest.SoundEffectType.None);
						Popup.Show(HistoricStringExpander.ExpandString("<spice.cooking.ate.!random>", null), null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					this.AfterCooked();
				}
				Campfire.pStomach.CookCount++;
				Campfire.ClearHunger();
				if (proceduralCookingEffect2 != null)
				{
					proceduralCookingEffect2.Init(The.Player);
					Popup.Show("You start to metabolize the meal, gaining the following effect for the rest of the day:\n\n{{W|" + Campfire.ProcessEffectDescription(proceduralCookingEffect2.GetProceduralEffectDescription(), The.Player) + "}}", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					proceduralCookingEffect2.Duration = 1;
					The.Player.ApplyEffect(proceduralCookingEffect2, null);
				}
			}
			else
			{
				Campfire.pStomach.CookCount++;
				Campfire.ClearHunger();
				Popup.Show(Campfire.DescribeMeal(list3), null, "Sounds/UI/ui_notification", true, true, true, true, null);
				TutorialManager.OnTrigger("AteRandom");
				if (flag)
				{
					if (!Campfire.RollTasty(num, The.Player.HasPart<Carnivorous>(), false))
					{
						IComponent<GameObject>.PlayUISound("Human_Eating", 1f, false, SoundRequest.SoundEffectType.None);
						Popup.Show(HistoricStringExpander.ExpandString("<spice.cooking.ate.!random>", null), null, "Sounds/UI/ui_notification", true, true, true, true, null);
						this.AfterCooked();
					}
				}
				else
				{
					IComponent<GameObject>.PlayUISound("Human_Eating", 1f, false, SoundRequest.SoundEffectType.None);
					Popup.Show(HistoricStringExpander.ExpandString("<spice.cooking.ate.!random>", null), null, "Sounds/UI/ui_notification", true, true, true, true, null);
					this.AfterCooked();
				}
			}
			Event @event = Event.New("UsedAsIngredient", "Actor", The.Player);
			foreach (GameObject gameObject4 in list3)
			{
				gameObject4.FireEvent(@event);
				if (gameObject4.HasPart<PreparedCookingIngredient>())
				{
					PreparedCookingIngredient part = gameObject4.GetPart<PreparedCookingIngredient>();
					if (part.HasTag("AlwaysStack"))
					{
						gameObject4.Destroy(null, false, false, null);
					}
					else
					{
						gameObject4.SplitFromStack();
						part.charges--;
						if (part.charges <= 0)
						{
							gameObject4.Destroy(null, false, false, null);
						}
						else
						{
							gameObject4.CheckStack();
						}
					}
				}
				else
				{
					LiquidVolume liquidVolume3 = gameObject4.LiquidVolume;
					if (liquidVolume3 != null)
					{
						liquidVolume3.UseDram();
					}
				}
			}
			return true;
		}

		// Token: 0x0600B894 RID: 47252 RVA: 0x00433BAC File Offset: 0x00431DAC
		public bool CookFromRecipe()
		{
			if (!Campfire.hasSkill)
			{
				Popup.Show("You don't have the Cooking and Gathering skill.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				return false;
			}
			if (!Campfire.IsHungry(The.Player))
			{
				Popup.Show("You aren't hungry. Instead, you relax by the warmth of the fire.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				return false;
			}
			bool flag = Campfire.pStomach.HungerLevel > 0;
			int num = 0;
			CookingGameState.ResetInventorySnapshot();
			bool flag2 = false;
			int num2 = 0;
			List<Tuple<string, CookingRecipe>> list;
			int num4;
			for (;;)
			{
				IL_005A:
				int num3 = 0;
				list = new List<Tuple<string, CookingRecipe>>();
				foreach (CookingRecipe cookingRecipe in CookingGameState.instance.knownRecipies)
				{
					if (!cookingRecipe.Hidden && (!The.Player.HasPart<Carnivorous>() || (!cookingRecipe.HasPlants() && !cookingRecipe.HasFungi())))
					{
						if (!flag2 && !cookingRecipe.CheckIngredients(false))
						{
							num3++;
						}
						else
						{
							list.Add(new Tuple<string, CookingRecipe>(cookingRecipe.GetCampfireDescription() + "\n\n", cookingRecipe));
						}
					}
				}
				if (list.Count <= 0)
				{
					if (num3 <= 0)
					{
						break;
					}
					flag2 = true;
				}
				else
				{
					list.Sort((Tuple<string, CookingRecipe> a, Tuple<string, CookingRecipe> b) => ConsoleLib.Console.ColorUtility.CompareExceptFormattingAndCase(a.Item1, b.Item1));
					string text = "";
					if (num3 > 0)
					{
						text = text + "&K< " + num3.ToString() + " hidden for missing ingredients >";
						list.Add(new Tuple<string, CookingRecipe>("Show " + num3.ToString() + " hidden recipes missing ingredients", null));
					}
					num4 = Popup.PickOption("Choose a recipe", text, Popup.SPACING_DARK_LINE.Replace('=', '÷'), "Sounds/UI/ui_notification", TupleUtilities<string, CookingRecipe>.GetFirstArray(list), null, null, null, null, null, null, 1, 72, num2, -1, true, true, false, true, false, null, null);
					if (num4 >= 0 && num4 < list.Count && list[num4].Item2 == null)
					{
						flag2 = true;
					}
					else
					{
						if (num4 < 0)
						{
							return false;
						}
						num2 = num4;
						int num5;
						for (;;)
						{
							string text2 = "Add to favorite recipes";
							if (list[num4].Item2.Favorite)
							{
								text2 = "Remove from favorite recipes";
							}
							num5 = Popup.PickOption("", list[num4].Item2.GetCampfireDescription(), "", "Sounds/UI/ui_notification", new string[] { "Cook", text2, "Forget", "Back" }, null, null, null, null, null, null, 0, 72, 0, -1, true, true, false, true, false, null, null);
							if (num5 < 0 || num5 == 3)
							{
								goto IL_005A;
							}
							if (num5 != 1)
							{
								break;
							}
							list[num4].Item2.Favorite = !list[num4].Item2.Favorite;
						}
						if (num5 == 2)
						{
							if (Popup.ShowYesNo("Are you sure you want to forget this recipe?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes)
							{
								list[num4].Item2.Hidden = true;
							}
						}
						else if (list[num4].Item2.CheckIngredients(true))
						{
							List<GameObject> list2 = new List<GameObject>();
							if (list[num4].Item2.UseIngredients(list2))
							{
								goto Block_19;
							}
						}
					}
				}
			}
			Popup.Show("You don't know any recipes.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			return false;
			Block_19:
			The.Player.FireEvent("ClearFoodEffects");
			The.Player.CleanEffects();
			if (flag)
			{
				List<GameObject> list2;
				if (!Campfire.RollTasty(num, The.Player.HasPart<Carnivorous>(), Campfire.ForceTastyBasedOnIngredients(list2)))
				{
					IComponent<GameObject>.PlayUISound("Human_Eating", 1f, false, SoundRequest.SoundEffectType.None);
					Popup.Show(HistoricStringExpander.ExpandString("<spice.cooking.ate.!random>", null), null, "Sounds/UI/ui_notification", true, true, true, true, null);
					this.AfterCooked();
				}
			}
			else
			{
				IComponent<GameObject>.PlayUISound("Human_Eating", 1f, false, SoundRequest.SoundEffectType.None);
				Popup.Show(HistoricStringExpander.ExpandString("<spice.cooking.ate.!random>", null), null, "Sounds/UI/ui_notification", true, true, true, true, null);
				this.AfterCooked();
			}
			if (list[num4].Item2.ApplyEffectsTo(The.Player, true))
			{
				Campfire.pStomach.CookCount++;
				Campfire.ClearHunger();
			}
			return true;
		}

		// Token: 0x0600B895 RID: 47253 RVA: 0x00433FCC File Offset: 0x004321CC
		public bool Cook()
		{
			if (!The.Player.CheckFrozen(false, false, false, null))
			{
				return false;
			}
			if (The.Player.AreHostilesNearby())
			{
				Popup.Show("You can't cook with hostile creatures nearby.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				return false;
			}
			ActivePartStatus activePartStatus = base.GetActivePartStatus(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null);
			if (activePartStatus != ActivePartStatus.Operational)
			{
				if (activePartStatus == ActivePartStatus.SwitchedOff)
				{
					Popup.Show(this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " turned off.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				else if (activePartStatus == ActivePartStatus.Unpowered)
				{
					Popup.Show(this.ParentObject.Does("do", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " not have enough charge to operate.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				else if (activePartStatus == ActivePartStatus.NotHanging)
				{
					Popup.Show(this.ParentObject.Does("need", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " to be hung up first.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				else
				{
					Popup.Show(this.ParentObject.Does("do", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " not seem to be working.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				return false;
			}
			if (!The.Player.CanChangeMovementMode("Cooking", true, false, true, false))
			{
				return false;
			}
			GameManager.Instance.uiQueue.queueTask(delegate
			{
				GameObject.Find("CampfireSounds").GetComponent<CampfireSounds>().Open();
			}, 0);
			try
			{
				for (;;)
				{
					Dictionary<string, InventoryAction> dictionary = new Dictionary<string, InventoryAction>(16);
					GetCookingActionsEvent.SendToActorAndObject(The.Player, this.ParentObject, dictionary);
					InventoryAction inventoryAction = EquipmentAPI.ShowInventoryActionMenu(dictionary, The.Player, this.ParentObject, false, false, "{{W|The fire breathes its warmth on your bones.}}", new InventoryAction.Comparer
					{
						priorityFirst = true
					}, false);
					if (inventoryAction == null)
					{
						break;
					}
					if (inventoryAction.Process(this.ParentObject, The.Player, false).InterfaceExitRequested())
					{
						goto Block_12;
					}
				}
				return true;
				Block_12:
				return true;
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("Campfire", ex, "game_exception");
			}
			finally
			{
				GameManager.Instance.uiQueue.queueTask(delegate
				{
					GameObject.Find("CampfireSounds").GetComponent<CampfireSounds>().Close();
				}, 0);
			}
			return true;
		}

		// Token: 0x0600B896 RID: 47254 RVA: 0x00434298 File Offset: 0x00432498
		private static bool CanExtinguish(GameObject obj)
		{
			LiquidVolume liquidVolume = obj.LiquidVolume;
			return liquidVolume.Volume >= 10 && liquidVolume.IsOpenVolume();
		}

		// Token: 0x0600B897 RID: 47255 RVA: 0x004342C0 File Offset: 0x004324C0
		public static GameObject FindExtinguishingPool(Cell C)
		{
			GameObject gameObject = ((C != null) ? C.GetFirstObjectWithPart("LiquidVolume", new Predicate<GameObject>(Campfire.CanExtinguish)) : null);
			if (gameObject == null)
			{
				return null;
			}
			if (C.HasBridge())
			{
				return null;
			}
			return gameObject;
		}

		// Token: 0x0600B898 RID: 47256 RVA: 0x004342FB File Offset: 0x004324FB
		public static GameObject FindExtinguishingPool(GameObject obj)
		{
			return Campfire.FindExtinguishingPool((obj != null) ? obj.CurrentCell : null);
		}

		// Token: 0x0600B899 RID: 47257 RVA: 0x0043430E File Offset: 0x0043250E
		public GameObject FindExtinguishingPool()
		{
			return Campfire.FindExtinguishingPool(this.ParentObject);
		}

		// Token: 0x0600B89A RID: 47258 RVA: 0x0043431B File Offset: 0x0043251B
		public static void ClearHunger()
		{
			Stomach part = The.Player.GetPart<Stomach>();
			if (part == null)
			{
				return;
			}
			part.ClearHunger();
		}

		// Token: 0x0600B89B RID: 47259 RVA: 0x00434331 File Offset: 0x00432531
		public static bool ForceTastyBasedOnIngredients(List<string> ingredients)
		{
			return ingredients.Contains("tastyMinor");
		}

		// Token: 0x0600B89C RID: 47260 RVA: 0x00434340 File Offset: 0x00432540
		public static bool ForceTastyBasedOnIngredients(List<GameObject> ingredientObjects)
		{
			foreach (GameObject gameObject in ingredientObjects)
			{
				PreparedCookingIngredient part = gameObject.GetPart<PreparedCookingIngredient>();
				if (part != null && part.HasTypeOption("tastyMinor"))
				{
					return true;
				}
				LiquidVolume liquidVolume = gameObject.LiquidVolume;
				if (liquidVolume != null && liquidVolume.GetPreparedCookingIngredient().Contains("tastyMinor"))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600B89D RID: 47261 RVA: 0x004343C8 File Offset: 0x004325C8
		private static Effect RandomTastyEffect(string tastyMessage)
		{
			switch (Stat.Random(0, 6))
			{
			case 0:
				return new BasicCookingEffect_Hitpoints(tastyMessage);
			case 1:
				return new BasicCookingEffect_MA(tastyMessage);
			case 2:
				return new BasicCookingEffect_MS(tastyMessage);
			case 3:
				return new BasicCookingEffect_Quickness(tastyMessage);
			case 4:
				return new BasicCookingEffect_RandomStat(tastyMessage);
			case 5:
				return new BasicCookingEffect_Regeneration(tastyMessage);
			case 6:
				return new BasicCookingEffect_ToHit(tastyMessage);
			default:
				return null;
			}
		}

		// Token: 0x0600B89E RID: 47262 RVA: 0x00434434 File Offset: 0x00432634
		public static bool RollTasty(int Bonus = 0, bool bCarnivore = false, bool bAlwaysSucceed = false)
		{
			if (The.Player == null)
			{
				return false;
			}
			if (bAlwaysSucceed || (10 + Bonus).in100())
			{
				string text = ((!bCarnivore) ? "You eat the meal. It's tastier than usual." : "You gorge on the succulent meat. It's tastier than usual.");
				IComponent<GameObject>.PlayUISound("Human_Eating_WithGulp", 1f, false, SoundRequest.SoundEffectType.None);
				The.Player.ApplyEffect(Campfire.RandomTastyEffect(text), null);
				return true;
			}
			return false;
		}

		// Token: 0x0600B89F RID: 47263 RVA: 0x00434490 File Offset: 0x00432690
		public static string[] RollIngredients(int Amount, IReadOnlyList<GameObject> Objects = null, global::System.Random Random = null)
		{
			Dictionary<string, string> variableCache = HistoricStringExpander.GetVariableCache();
			string objectTypeForZone = ZoneManager.GetObjectTypeForZone(The.Player.Physics.CurrentCell.ParentZone.ZoneID);
			string tag = GameObjectFactory.Factory.Blueprints[objectTypeForZone].GetTag("LairOwnerTable", "GenericLairOwner");
			int num = 0;
			GameObject gameObject = null;
			GameObject gameObject2 = null;
			GameObject gameObject3 = null;
			GameObject gameObject4 = null;
			try
			{
				do
				{
					if (gameObject != null)
					{
						gameObject.Destroy(null, false, false, null);
					}
					num++;
					gameObject = GameObject.Create(PopulationManager.RollOneFrom((num < 20) ? tag : "LairOwners_Jungle", null, null).Blueprint);
				}
				while (!Axe_Dismember.HasAnyDismemberableBodyPart(gameObject, The.Player, null, true, false));
				variableCache["$terrain"] = GameObjectFactory.Factory.Blueprints[objectTypeForZone].GetTag("Terrain", "");
				variableCache["$creaturePossessive"] = Grammar.MakePossessive(gameObject.DisplayNameOnlyStripped);
				variableCache["$creatureBodyPart"] = Axe_Dismember.GetDismemberableBodyPart(gameObject, The.Player, null, false, false).GetOrdinalName();
				variableCache["$bookName"] = "{{|" + (gameObject2 = EncountersAPI.CreateARandomDescendantOf("Book")).DisplayNameOnlyStripped + "}}";
				variableCache["$gasName"] = "{{|" + (gameObject3 = EncountersAPI.CreateARandomDescendantOf("Gas")).DisplayNameOnlyStripped + "}}";
				variableCache["$tonicName"] = "{{|" + (gameObject4 = EncountersAPI.CreateARandomDescendantOf("Tonic")).DisplayNameOnlyDirect + "}}";
			}
			catch (Exception ex)
			{
				MetricsManager.LogException(string.Concat(new string[]
				{
					"HistoricVariables [Terrain: ",
					objectTypeForZone,
					", Creature: ",
					(gameObject != null) ? gameObject.DebugName : null,
					", Book: ",
					(gameObject2 != null) ? gameObject2.DebugName : null,
					", Gas: ",
					(gameObject3 != null) ? gameObject3.DebugName : null,
					", Tonic: ",
					(gameObject4 != null) ? gameObject4.DebugName : null,
					"]"
				}), ex, "game_exception");
				return new string[] { "a pinch of salt" };
			}
			finally
			{
				if (gameObject != null)
				{
					gameObject.Destroy(null, false, false, null);
				}
				if (gameObject2 != null)
				{
					gameObject2.Destroy(null, false, false, null);
				}
				if (gameObject3 != null)
				{
					gameObject3.Destroy(null, false, false, null);
				}
				if (gameObject4 != null)
				{
					gameObject4.Destroy(null, false, false, null);
				}
			}
			string[] array = new string[Amount];
			for (int i = 0; i < Amount; i++)
			{
				try
				{
					if (Objects != null && i < Objects.Count)
					{
						LiquidVolume liquidVolume = Objects[i].LiquidVolume;
						if (liquidVolume != null && liquidVolume.HasPreparedCookingIngredient())
						{
							array[i] = "a dram of " + liquidVolume.GetLiquidName();
						}
						else
						{
							array[i] = Objects[i].an(int.MaxValue, null, "CookingIngredient", false, true, true, false, false, false, true, false, false, true, false, null, false, null, false);
						}
					}
					else
					{
						array[i] = HistoricStringExpander.ExpandString("<spice.cooking.ingredient.!random>", null, null, variableCache, Random);
					}
				}
				catch (Exception ex2)
				{
					GameObject gameObject5 = Objects.ElementAtOrDefault(i, null);
					string text = ((gameObject5 != null) ? gameObject5.DebugName : null) ?? "unknown";
					MetricsManager.LogException("MealIngredient [" + text + "]", ex2, "game_exception");
					array[i] = "{invalid ingredient: " + text + "}";
				}
			}
			return array;
		}

		// Token: 0x0600B8A0 RID: 47264 RVA: 0x0043485C File Offset: 0x00432A5C
		public static string DescribeMeal(IReadOnlyList<GameObject> mealObjects)
		{
			string text = "unknown";
			string text2;
			try
			{
				text = Grammar.MakeAndList(Campfire.RollIngredients(Stat.Random(3, 4), mealObjects, null), true);
				text2 = Grammar.ConvertAtoAn(HistoricStringExpander.ExpandString("<spice.cooking.cookTemplate.!random>", null).Replace("*ingredients*", text));
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("Error generating meal description", ex, "game_exception");
				text2 = "{invalid meal: " + text + "}";
			}
			return text2;
		}

		// Token: 0x0600B8A1 RID: 47265 RVA: 0x004348D8 File Offset: 0x00432AD8
		public static string ProcessEffectDescription(string description, GameObject go)
		{
			description = description.TrimEnd(new char[] { '\r', '\n' });
			if (go == null || go.IsPlayer())
			{
				return Grammar.CapAfterNewlines(Grammar.InitCap(description.Replace("@thisCreature", "you").Replace("@s", "").Replace("@es", "")
					.Replace("@is", "are")
					.Replace("@they", "you")
					.Replace("@their", "your")
					.Replace("@them", "you")));
			}
			return Grammar.CapAfterNewlines(Grammar.InitCap(description.Replace("@thisCreature", go.IsPlural ? "these creatures" : "this creature").Replace("@s", go.IsPlural ? "" : "s").Replace("@es", go.IsPlural ? "" : "es")
				.Replace("@is", go.Is)
				.Replace("@they", go.it)
				.Replace("@their", go.its)
				.Replace("@them", go.them)));
		}

		// Token: 0x0600B8A2 RID: 47266 RVA: 0x00434A22 File Offset: 0x00432C22
		private List<GameObject> GetPlayerAndAdjacentCompanions()
		{
			List<GameObject> list = Event.NewGameObjectList();
			list.Add(The.Player);
			list.AddRange(The.Player.GetCompanionsReadonly(1, null));
			return list;
		}

		// Token: 0x0600B8A3 RID: 47267 RVA: 0x00434A46 File Offset: 0x00432C46
		private bool AnyPlayerOrAdjacentCompanion(Predicate<GameObject> Filter)
		{
			return Filter == null || Filter(The.Player) || The.Player.AnyCompanion(1, Filter);
		}

		// Token: 0x0600B8A4 RID: 47268 RVA: 0x00434A68 File Offset: 0x00432C68
		private List<GameObject> GetMedicinalIngredients()
		{
			List<GameObject> list = Event.NewGameObjectList();
			foreach (GameObject gameObject in The.Player.GetInventory())
			{
				if (gameObject.HasTagOrProperty("Medicinal"))
				{
					list.Add(gameObject);
				}
				else
				{
					PreparedCookingIngredient part = gameObject.GetPart<PreparedCookingIngredient>();
					if (((part != null) ? part.type : null) == "regenLowtier")
					{
						list.Add(gameObject);
					}
				}
			}
			if (list.Count > 1)
			{
				list.Sort((GameObject a, GameObject b) => a.Value.CompareTo(b.Value));
			}
			return list;
		}

		// Token: 0x0600B8A5 RID: 47269 RVA: 0x00434B28 File Offset: 0x00432D28
		public bool NostrumsCanStopBleeding()
		{
			return this.AnyPlayerOrAdjacentCompanion((GameObject who) => who.HasEffect<Bleeding>((Bleeding fx) => !fx.Internal));
		}

		// Token: 0x0600B8A6 RID: 47270 RVA: 0x00434B50 File Offset: 0x00432D50
		public void NostrumsStopBleeding()
		{
			List<GameObject> playerAndAdjacentCompanions = this.GetPlayerAndAdjacentCompanions();
			int num = 0;
			foreach (GameObject gameObject in playerAndAdjacentCompanions)
			{
				bool flag = false;
				bool flag2 = false;
				if (gameObject._Effects != null)
				{
					int i = 0;
					int count = gameObject.Effects.Count;
					while (i < count)
					{
						Bleeding bleeding = gameObject.Effects[i] as Bleeding;
						if (bleeding != null)
						{
							if (bleeding.Internal)
							{
								flag2 = true;
							}
							else
							{
								flag = true;
							}
						}
						i++;
					}
				}
				if (flag || flag2)
				{
					if (!gameObject.IsPlayer() && !The.Player.PhaseMatches(gameObject))
					{
						Popup.ShowFail(string.Concat(new string[]
						{
							"You try to staunch the wounds of ",
							gameObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
							", but your limbs pass through ",
							gameObject.them,
							"."
						}), true, true, true);
					}
					else if (!gameObject.IsPlayer() && gameObject.IsInStasis())
					{
						Popup.ShowFail(string.Concat(new string[]
						{
							"You try to staunch the wounds of ",
							gameObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
							", but cannot affect ",
							gameObject.them,
							"."
						}), true, true, true);
					}
					else if (flag)
					{
						IComponent<GameObject>.PlayUISound("Sounds/Interact/sfx_interact_campfire_treat_bleeding", 1f, false, SoundRequest.SoundEffectType.None);
						if (flag2)
						{
							if (gameObject.IsPlayer())
							{
								Popup.Show("You staunch your wounds, though some are too deep to treat.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
							}
							else
							{
								Popup.Show("You staunch the wounds of " + gameObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ", though some are too deep to treat.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
							}
						}
						else if (gameObject.IsPlayer())
						{
							Popup.Show("You staunch your wounds.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						}
						else
						{
							Popup.Show("You staunch the wounds of " + gameObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						}
						int j = 0;
						int num2 = gameObject.Effects.Count;
						int num3 = 0;
						while (j < num2)
						{
							if (num3 >= 1000)
							{
								break;
							}
							Bleeding bleeding2 = gameObject.Effects[j] as Bleeding;
							if (bleeding2 != null && !bleeding2.Internal)
							{
								bleeding2.StopMessageUsePopup = false;
								gameObject.RemoveEffect(bleeding2, true);
								j = -1;
								num2 = gameObject.Effects.Count;
							}
							j++;
							num3++;
						}
					}
					else
					{
						Popup.Show(gameObject.Poss("wounds", true) + " are too deep to treat.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					num++;
				}
			}
			if (num == 0)
			{
				if (playerAndAdjacentCompanions.Count == 1)
				{
					Popup.ShowFail("You are not bleeding.", true, true, true);
					return;
				}
				if (playerAndAdjacentCompanions.Count == 2)
				{
					Popup.ShowFail("Neither you nor " + playerAndAdjacentCompanions[1].t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " are bleeding.", true, true, true);
					return;
				}
				Popup.ShowFail("Neither you nor any of your nearby companions are bleeding.", true, true, true);
			}
		}

		// Token: 0x0600B8A7 RID: 47271 RVA: 0x00434EF8 File Offset: 0x004330F8
		public bool NostrumsCanTreatPoison()
		{
			return this.AnyPlayerOrAdjacentCompanion((GameObject who) => who.HasEffectOfType(67174400, null));
		}

		// Token: 0x0600B8A8 RID: 47272 RVA: 0x00434F20 File Offset: 0x00433120
		public void NostrumsTreatPoison()
		{
			List<GameObject> playerAndAdjacentCompanions = this.GetPlayerAndAdjacentCompanions();
			List<GameObject> list = Event.NewGameObjectList(playerAndAdjacentCompanions, (GameObject who) => who.HasEffectOfType(67174400, null));
			int count = list.Count;
			while (list.Count > 0)
			{
				GameObject gameObject;
				if (list.Count > 1)
				{
					gameObject = Popup.PickGameObject("Treat whom " + ((list.Count == count) ? "first" : "next") + "?", list, true, false, true, null, 0, false);
					if (gameObject == null)
					{
						break;
					}
				}
				else
				{
					gameObject = list[0];
				}
				list.Remove(gameObject);
				List<GameObject> medicinalIngredients = this.GetMedicinalIngredients();
				if (medicinalIngredients.Count == 0)
				{
					Popup.ShowFail("You have no medicinal ingredients with which to treat the poison coursing through " + gameObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", true, true, true);
					break;
				}
				GameObject gameObject2 = Popup.PickGameObject("Select an ingredient to use.", medicinalIngredients, true, false, true, null, 0, false);
				if (gameObject2 == null)
				{
					break;
				}
				if (!The.Player.PhaseMatches(gameObject))
				{
					Popup.ShowFail(string.Concat(new string[]
					{
						"You try to cure the poison coursing through ",
						gameObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
						", but your limbs pass through ",
						gameObject.them,
						"."
					}), true, true, true);
				}
				else if (gameObject.IsInStasis())
				{
					Popup.ShowFail(string.Concat(new string[]
					{
						"You try to cure the poison coursing through ",
						gameObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
						", but cannot affect ",
						gameObject.them,
						"."
					}), true, true, true);
				}
				else
				{
					int num = gameObject.RemoveEffectsOfType(67174400, null, true);
					if (num > 0)
					{
						IComponent<GameObject>.PlayUISound("Sounds/Interact/sfx_interact_campfire_treat_poison", 1f, false, SoundRequest.SoundEffectType.None);
						Popup.Show(string.Concat(new string[]
						{
							"You cure the ",
							(num == 1) ? "poison" : "poisons",
							" coursing through ",
							gameObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
							" with a balm made from ",
							gameObject2.an(int.MaxValue, null, null, false, true, false, false, false, false, true, false, false, true, false, null, false, null, false),
							"."
						}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
						gameObject2.Destroy(null, false, false, null);
					}
					else
					{
						Popup.ShowFail("You try to cure the poison coursing through " + gameObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ", but your cures are ineffective.", true, true, true);
					}
				}
			}
			if (count == 0)
			{
				if (playerAndAdjacentCompanions.Any<GameObject>((GameObject who) => who.HasEffectOfType(65536, null)))
				{
					List<GameObject> list2 = Event.NewGameObjectList(playerAndAdjacentCompanions, (GameObject who) => who.HasEffectOfType(65536, null));
					if (!list2.Contains(The.Player))
					{
						Popup.ShowFail("The poison affecting " + Grammar.MakeAndList(list2, false, true, false, true, false, null) + " is too strong to be cured by your nostrums.", true, true, true);
						return;
					}
					if (list2.Count == 1)
					{
						Popup.ShowFail("The poison affecting you is too strong to be cured by your nostrums.", true, true, true);
						return;
					}
					if (list2.Count == 2)
					{
						Popup.ShowFail("The poison affecting you and " + list2[1].t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " is too strong to be cured by your nostrums.", true, true, true);
						return;
					}
					Popup.ShowFail("The poison affecting you and your companions is too strong to be cured by your nostrums.", true, true, true);
					return;
				}
				else
				{
					if (playerAndAdjacentCompanions.Count == 1)
					{
						Popup.ShowFail("You are not poisoned.", true, true, true);
						return;
					}
					if (playerAndAdjacentCompanions.Count == 2)
					{
						Popup.ShowFail("Neither you nor " + playerAndAdjacentCompanions[1].t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " are poisoned.", true, true, true);
						return;
					}
					Popup.ShowFail("Neither you nor any of your nearby companions are poisoned.", true, true, true);
				}
			}
		}

		// Token: 0x0600B8A9 RID: 47273 RVA: 0x0043538F File Offset: 0x0043358F
		public bool NostrumsCanTreatIllness()
		{
			return this.AnyPlayerOrAdjacentCompanion((GameObject who) => who.HasEffect<Ill>());
		}

		// Token: 0x0600B8AA RID: 47274 RVA: 0x004353B8 File Offset: 0x004335B8
		public void NostrumsTreatIllness()
		{
			List<GameObject> playerAndAdjacentCompanions = this.GetPlayerAndAdjacentCompanions();
			List<GameObject> list = Event.NewGameObjectList(playerAndAdjacentCompanions, (GameObject who) => who.HasEffect<Ill>());
			int count = list.Count;
			while (list.Count > 0)
			{
				GameObject gameObject;
				if (list.Count > 1)
				{
					gameObject = Popup.PickGameObject("Treat whom " + ((list.Count == count) ? "first" : "next") + "?", list, true, false, true, null, 0, false);
					if (gameObject == null)
					{
						break;
					}
				}
				else
				{
					gameObject = list[0];
				}
				list.Remove(gameObject);
				List<GameObject> medicinalIngredients = this.GetMedicinalIngredients();
				if (medicinalIngredients.Count == 0)
				{
					Popup.ShowFail("You have no medicinal ingredients with which to treat " + gameObject.poss("illness", true) + ".", true, true, true);
					break;
				}
				GameObject gameObject2 = Popup.PickGameObject("Select an ingredient to use.", medicinalIngredients, true, false, true, null, 0, false);
				if (gameObject2 == null)
				{
					break;
				}
				if (!The.Player.PhaseMatches(gameObject))
				{
					Popup.ShowFail(string.Concat(new string[]
					{
						"You try to cure ",
						gameObject.poss("illness", true),
						", but your limbs pass through ",
						gameObject.them,
						"."
					}), true, true, true);
				}
				else if (gameObject.IsInStasis())
				{
					Popup.ShowFail(string.Concat(new string[]
					{
						"You try to ",
						gameObject.poss("illness", true),
						", but cannot affect ",
						gameObject.them,
						"."
					}), true, true, true);
				}
				else
				{
					IComponent<GameObject>.PlayUISound("Sounds/Interact/sfx_interact_campfire_treat_illness", 1f, false, SoundRequest.SoundEffectType.None);
					gameObject.RemoveAllEffects<Ill>(true);
					Popup.Show(string.Concat(new string[]
					{
						"You cure ",
						gameObject.poss("illness", true),
						" with a balm made from ",
						gameObject2.an(int.MaxValue, null, null, false, true, false, false, false, false, true, false, false, true, false, null, false, null, false),
						"."
					}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
					gameObject2.Destroy(null, false, false, null);
				}
			}
			if (count == 0)
			{
				if (playerAndAdjacentCompanions.Count == 1)
				{
					Popup.ShowFail("You are not ill.", true, true, true);
					return;
				}
				if (playerAndAdjacentCompanions.Count == 2)
				{
					Popup.ShowFail("Neither you nor " + playerAndAdjacentCompanions[1].t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " are ill.", true, true, true);
					return;
				}
				Popup.ShowFail("Neither you nor any of your nearby companions are ill.", true, true, true);
			}
		}

		// Token: 0x0600B8AB RID: 47275 RVA: 0x00435659 File Offset: 0x00433859
		public bool NostrumsCanTreatDiseaseOnset()
		{
			return this.AnyPlayerOrAdjacentCompanion((GameObject who) => GetDiseaseOnsetEvent.GetFor(who) != null);
		}

		// Token: 0x0600B8AC RID: 47276 RVA: 0x00435680 File Offset: 0x00433880
		public void NostrumsTreatDiseaseOnset()
		{
			List<GameObject> playerAndAdjacentCompanions = this.GetPlayerAndAdjacentCompanions();
			List<GameObject> list = Event.NewGameObjectList();
			List<Effect> list2 = new List<Effect>();
			int i = 0;
			int count = playerAndAdjacentCompanions.Count;
			while (i < count)
			{
				GameObject gameObject = playerAndAdjacentCompanions[i];
				Effect effect = GetDiseaseOnsetEvent.GetFor(gameObject);
				if (effect != null)
				{
					list.Add(gameObject);
					list2.Add(effect);
				}
				i++;
			}
			int count2 = list.Count;
			int num = 20;
			while (list.Count > 0)
			{
				GameObject gameObject;
				if (list.Count > 1)
				{
					gameObject = Popup.PickGameObject("Treat whom " + ((list.Count == count2) ? "first" : "next") + "?", list, true, false, true, null, 0, false);
					if (gameObject == null)
					{
						break;
					}
				}
				else
				{
					gameObject = list[0];
				}
				int num2 = list.IndexOf(gameObject);
				list.RemoveAt(num2);
				Effect effect = list2[num2];
				list2.RemoveAt(num2);
				if (gameObject.HasEffect<BoostedImmunity>())
				{
					Popup.ShowFail(gameObject.Does("have", int.MaxValue, null, null, "already", false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " boosted immunity from a nostrum.", true, true, true);
				}
				else
				{
					List<GameObject> medicinalIngredients = this.GetMedicinalIngredients();
					if (medicinalIngredients.Count == 0)
					{
						Popup.ShowFail("You have no medicinal ingredients with which to treat " + gameObject.poss(effect.GetDescription(), true) + ".", true, true, true);
						break;
					}
					GameObject gameObject2 = Popup.PickGameObject("Select an ingredient to use.", medicinalIngredients, true, false, true, null, 0, false);
					if (gameObject2 == null)
					{
						break;
					}
					if (!The.Player.PhaseMatches(gameObject))
					{
						Popup.ShowFail(string.Concat(new string[]
						{
							"You try to cure ",
							gameObject.poss("diease onset", true),
							", but your limbs pass through ",
							gameObject.them,
							"."
						}), true, true, true);
					}
					else if (gameObject.IsInStasis())
					{
						Popup.ShowFail(string.Concat(new string[]
						{
							"You try to ",
							gameObject.poss("disease onset", true),
							", but cannot affect ",
							gameObject.them,
							"."
						}), true, true, true);
					}
					else if (num.in100())
					{
						IComponent<GameObject>.PlayUISound("Sounds/Interact/sfx_interact_campfire_treat_disease", 1f, false, SoundRequest.SoundEffectType.None);
						Popup.Show(string.Concat(new string[]
						{
							"You cure ",
							gameObject.poss(effect.GetDescription(), true),
							" with a balm made from ",
							gameObject2.an(int.MaxValue, null, null, false, true, false, false, false, false, true, false, false, true, false, null, false, null, false),
							"."
						}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
						gameObject.RemoveEffect(effect, true);
						gameObject2.Destroy(null, false, false, null);
					}
					else if (gameObject.ApplyEffect(new BoostedImmunity(), null))
					{
						IComponent<GameObject>.PlayUISound("Sounds/Interact/sfx_interact_campfire_treat_disease", 1f, false, SoundRequest.SoundEffectType.None);
						Popup.Show(string.Concat(new string[]
						{
							"You boost ",
							gameObject.poss("immunity", true),
							" with a balm made from ",
							gameObject2.an(int.MaxValue, null, null, false, true, false, false, false, false, true, false, false, true, false, null, false, null, false),
							"."
						}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
						gameObject2.Destroy(null, false, false, null);
					}
					else
					{
						Popup.Show(string.Concat(new string[]
						{
							"You try to boost ",
							gameObject.poss("immunity", true),
							" with a balm made from ",
							gameObject2.an(int.MaxValue, null, null, false, true, false, false, false, false, true, false, false, true, false, null, false, null, false),
							", but it is ineffective."
						}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
						gameObject2.Destroy(null, false, false, null);
					}
				}
			}
			if (count2 == 0)
			{
				if (playerAndAdjacentCompanions.Count == 1)
				{
					Popup.ShowFail("You are not suffering from the onset of a disease.", true, true, true);
					return;
				}
				if (playerAndAdjacentCompanions.Count == 2)
				{
					Popup.ShowFail("Neither you nor " + playerAndAdjacentCompanions[1].t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " are suffering from the onset of a disease.", true, true, true);
					return;
				}
				Popup.ShowFail("Neither you nor any of your nearby companions are suffering from the onset of a disease.", true, true, true);
			}
		}

		// Token: 0x0600B8AD RID: 47277 RVA: 0x00435AE0 File Offset: 0x00433CE0
		public void Extinguish(GameObject Actor = null, GameObject Object = null)
		{
			if (Actor != null)
			{
				IComponent<GameObject>.XDidYToZ(Actor, "extinguish", this.ParentObject, null, null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
			}
			else if (Object != null)
			{
				base.EmitMessage(this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " extinguished by " + Object.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", ' ', false, false, false, null, null);
			}
			base.PlayWorldSound("Sounds/Interact/sfx_interact_campfire_extinguish", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
			bool flag = this.ParentObject.HasProperty("PlayerCampfire");
			GameObject gameObject = this.ParentObject.ReplaceWith(this.ExtinguishBlueprint);
			if (gameObject != null && flag)
			{
				gameObject.SetIntProperty("PlayerCampfire", 1, false);
				gameObject.SetStringProperty("PointOfInterestKey", "PlayerCampfire", false);
			}
		}

		// Token: 0x04004A4F RID: 19023
		public const string COOKING_PREFIX = "ProceduralCookingIngredient_";

		// Token: 0x04004A50 RID: 19024
		public string ExtinguishBlueprint;

		// Token: 0x04004A51 RID: 19025
		public string PresetMeals = "";

		// Token: 0x04004A52 RID: 19026
		public List<CookingRecipe> specificProcgenMeals;

		// Token: 0x04004A53 RID: 19027
		[NonSerialized]
		protected List<CookingRecipe> _presetMeals;

		// Token: 0x04004A54 RID: 19028
		public static readonly string COOK_COMMAND_RECIPE = "CookFromRecipe";

		// Token: 0x04004A55 RID: 19029
		public static readonly string COOK_COMMAND_WHIP_UP = "CookWhipUp";

		// Token: 0x04004A56 RID: 19030
		public static readonly string COOK_COMMAND_CHOOSE = "CookChooseIngredients";

		// Token: 0x04004A57 RID: 19031
		public static readonly string COOK_COMMAND_PRESERVE = "CookPreserve";

		// Token: 0x04004A58 RID: 19032
		public static readonly string COOK_COMMAND_PRESERVE_EXOTIC = "CookPreserveExotic";

		// Token: 0x04004A59 RID: 19033
		public static readonly string COOK_COMMAND_PRESET_MEAL = "CookPresetMeal:";

		// Token: 0x04004A5A RID: 19034
		public static readonly string COOK_COMMAND_STOP_BLEEDING = "CookStopBleeding";

		// Token: 0x04004A5B RID: 19035
		public static readonly string COOK_COMMAND_TREAT_POISON = "CookTreatPoison";

		// Token: 0x04004A5C RID: 19036
		public static readonly string COOK_COMMAND_TREAT_ILLNESS = "CookTreatIllness";

		// Token: 0x04004A5D RID: 19037
		public static readonly string COOK_COMMAND_TREAT_DISEASE_ONSET = "CookTreatDiseaseOnset";
	}
}
