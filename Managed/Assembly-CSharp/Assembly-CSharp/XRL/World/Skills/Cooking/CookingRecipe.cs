using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.Console;
using Cysharp.Text;
using HistoryKit;
using Occult.Engine.CodeGeneration;
using Qud.API;
using UnityEngine;
using Wintellect.PowerCollections;
using XRL.Language;
using XRL.Rules;
using XRL.UI;
using XRL.World.Effects;
using XRL.World.Parts;

namespace XRL.World.Skills.Cooking
{
	// Token: 0x02000BAA RID: 2986
	[GenerateSerializationPartial]
	[Serializable]
	public class CookingRecipe : IComposite
	{
		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x060076E5 RID: 30437 RVA: 0x002CC73A File Offset: 0x002CA93A
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public virtual bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060076E6 RID: 30438 RVA: 0x002CC740 File Offset: 0x002CA940
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public virtual void Write(SerializationWriter Writer)
		{
			Writer.Write(this.Hidden);
			Writer.Write(this.Favorite);
			Writer.WriteOptimized(this.DisplayName);
			Writer.WriteOptimized(this.ChefName);
			Writer.WriteComposite<ICookingRecipeComponent>(this.Components);
			Writer.WriteComposite<ICookingRecipeResult>(this.Effects);
			Writer.Write(this.Tile);
		}

		// Token: 0x060076E7 RID: 30439 RVA: 0x002CC7A4 File Offset: 0x002CA9A4
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public virtual void Read(SerializationReader Reader)
		{
			this.Hidden = Reader.ReadBoolean();
			this.Favorite = Reader.ReadBoolean();
			this.DisplayName = Reader.ReadOptimizedString();
			this.ChefName = Reader.ReadOptimizedString();
			this.Components = Reader.ReadCompositeList<ICookingRecipeComponent>();
			this.Effects = Reader.ReadCompositeList<ICookingRecipeResult>();
			this.Tile = (Renderable)Reader.ReadComposite();
		}

		// Token: 0x060076E8 RID: 30440 RVA: 0x002CC80A File Offset: 0x002CAA0A
		public virtual string GetApplyMessage()
		{
			return "";
		}

		// Token: 0x060076E9 RID: 30441 RVA: 0x002CC814 File Offset: 0x002CAA14
		public virtual string GetDisplayName()
		{
			if (this.CachedDisplayName == null)
			{
				using (Utf16ValueStringBuilder utf16ValueStringBuilder = ZString.CreateStringBuilder())
				{
					utf16ValueStringBuilder.Append("{{W|");
					if (this.ChefName != null)
					{
						utf16ValueStringBuilder.Append(Grammar.MakePossessive(this.ChefName));
						utf16ValueStringBuilder.Append(' ');
					}
					utf16ValueStringBuilder.Append(this.DisplayName ?? base.GetType().Name);
					utf16ValueStringBuilder.Append("}}");
					this.CachedDisplayName = utf16ValueStringBuilder.ToString();
				}
			}
			return this.CachedDisplayName;
		}

		// Token: 0x060076EA RID: 30442 RVA: 0x002CC8C4 File Offset: 0x002CAAC4
		public virtual Renderable GetRenderable()
		{
			if (this.Tile == null)
			{
				this.Tile = CookingRecipe.GenerateRecipeTile(this);
			}
			return this.Tile;
		}

		// Token: 0x060076EB RID: 30443 RVA: 0x002CC8E0 File Offset: 0x002CAAE0
		public virtual CookingRecipe DeepCopy()
		{
			CookingRecipe cookingRecipe = (CookingRecipe)Activator.CreateInstance(base.GetType());
			cookingRecipe.Hidden = this.Hidden;
			cookingRecipe.Favorite = this.Favorite;
			cookingRecipe.DisplayName = this.DisplayName;
			cookingRecipe.ChefName = this.ChefName;
			cookingRecipe.Components = new List<ICookingRecipeComponent>(this.Components);
			cookingRecipe.Effects = new List<ICookingRecipeResult>(this.Effects);
			cookingRecipe.Tile = new Renderable(this.Tile);
			return cookingRecipe;
		}

		// Token: 0x060076EC RID: 30444 RVA: 0x002CC960 File Offset: 0x002CAB60
		public int CompareTo(CookingRecipe recipe)
		{
			if (recipe == this)
			{
				return 0;
			}
			bool flag = this.CheckIngredients(false);
			bool flag2 = recipe.CheckIngredients(false);
			if (flag == flag2 && this.Favorite == recipe.Favorite)
			{
				return this.GetDisplayName().CompareTo(recipe.GetDisplayName());
			}
			if (this.Favorite != recipe.Favorite)
			{
				if (this.Favorite)
				{
					return -1;
				}
				return 1;
			}
			else
			{
				if (flag)
				{
					return -1;
				}
				return 1;
			}
		}

		// Token: 0x060076ED RID: 30445 RVA: 0x002CC9C7 File Offset: 0x002CABC7
		public virtual bool HasPlants()
		{
			return this.Components.Any<ICookingRecipeComponent>((ICookingRecipeComponent r) => r.HasPlants());
		}

		// Token: 0x060076EE RID: 30446 RVA: 0x002CC9F8 File Offset: 0x002CABF8
		public virtual bool HasFungi()
		{
			return this.Components.Any<ICookingRecipeComponent>((ICookingRecipeComponent r) => r.HasFungi());
		}

		// Token: 0x060076EF RID: 30447 RVA: 0x002CCA29 File Offset: 0x002CAC29
		public virtual string GetCampfireDescription()
		{
			return this.GetAnnotatedDisplayName(false, true) + "\n\n" + this.GetDescription();
		}

		// Token: 0x060076F0 RID: 30448 RVA: 0x002CCA44 File Offset: 0x002CAC44
		public virtual string GetDescription()
		{
			string text = "";
			foreach (ICookingRecipeResult cookingRecipeResult in this.Effects)
			{
				if (text != "")
				{
					text += "  ";
				}
				text += cookingRecipeResult.GetCampfireDescription();
			}
			return text;
		}

		// Token: 0x060076F1 RID: 30449 RVA: 0x002CCAC0 File Offset: 0x002CACC0
		public virtual string GetIngredients()
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			int num = 0;
			foreach (ICookingRecipeComponent cookingRecipeComponent in this.Components)
			{
				if (num != 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(cookingRecipeComponent.getDisplayName());
				num++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060076F2 RID: 30450 RVA: 0x002CCB3C File Offset: 0x002CAD3C
		public virtual string GetAnnotatedDisplayName()
		{
			return this.GetAnnotatedDisplayName(true, false);
		}

		// Token: 0x060076F3 RID: 30451 RVA: 0x002CCB48 File Offset: 0x002CAD48
		public virtual string GetAnnotatedDisplayName(bool oneline = true, bool showQuantity = false)
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			if (this.Favorite)
			{
				stringBuilder.Append("{{R|").Append('\u0003').Append("}} {{W|");
			}
			if (!this.CheckIngredients(false))
			{
				stringBuilder.Append("{{K|").Append(ConsoleLib.Console.ColorUtility.StripFormatting(this.GetDisplayName())).Append("}}");
			}
			else
			{
				stringBuilder.Append(this.GetDisplayName());
			}
			if (this.Favorite)
			{
				stringBuilder.Append("}}");
			}
			if (oneline)
			{
				stringBuilder.Append(" [");
			}
			else
			{
				stringBuilder.Append('\n');
			}
			int num = 0;
			foreach (ICookingRecipeComponent cookingRecipeComponent in this.Components)
			{
				if (num != 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(cookingRecipeComponent.getDisplayName());
				if (showQuantity)
				{
					stringBuilder.Append("{{K|(");
					stringBuilder.Append(CookingGameState.GetIngredientQuantity(cookingRecipeComponent));
					stringBuilder.Append(")}}");
				}
				num++;
			}
			if (oneline)
			{
				stringBuilder.Append("]");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060076F4 RID: 30452 RVA: 0x002CCC8C File Offset: 0x002CAE8C
		public virtual bool CheckIngredients(bool displayMessage = false)
		{
			if (displayMessage)
			{
				bool flag = true;
				string text = "";
				foreach (ICookingRecipeComponent cookingRecipeComponent in this.Components)
				{
					if (!cookingRecipeComponent.doesPlayerHaveEnough())
					{
						if (!string.IsNullOrEmpty(text))
						{
							text += "\n";
						}
						text += cookingRecipeComponent.createPlayerDoesNotHaveEnoughMessage();
						Popup.Show(text, null, "Sounds/UI/ui_notification", true, true, true, true, null);
						return false;
					}
				}
				return flag;
			}
			using (List<ICookingRecipeComponent>.Enumerator enumerator = this.Components.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.doesPlayerHaveEnough())
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060076F5 RID: 30453 RVA: 0x002CCD70 File Offset: 0x002CAF70
		public virtual bool UseIngredients(List<GameObject> used)
		{
			foreach (ICookingRecipeComponent cookingRecipeComponent in this.Components)
			{
				cookingRecipeComponent.use(used);
			}
			return true;
		}

		// Token: 0x060076F6 RID: 30454 RVA: 0x002CCDC4 File Offset: 0x002CAFC4
		public virtual bool ApplyEffectsTo(GameObject target, bool showMessage = true)
		{
			string text = "";
			if (showMessage)
			{
				text = this.GetApplyMessage();
			}
			foreach (ICookingRecipeResult cookingRecipeResult in this.Effects)
			{
				text += cookingRecipeResult.apply(target);
				text += "\n";
			}
			if (showMessage)
			{
				Popup.Show("You start to metabolize the meal, gaining the following effect for the rest of the day:\n\n{{W|" + Campfire.ProcessEffectDescription(text, target) + "}}", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			return true;
		}

		// Token: 0x060076F7 RID: 30455 RVA: 0x002CCE64 File Offset: 0x002CB064
		public static CookingRecipe FromIngredients(List<string> ingredients, ProceduralCookingEffect effect = null, string chefName = null)
		{
			List<GameObject> list = new List<GameObject>(ingredients.Count);
			foreach (string text in ingredients)
			{
				list.Add(GameObjectFactory.Factory.CreateObject(text));
			}
			return CookingRecipe.FromIngredients(list, effect, chefName, "generic");
		}

		// Token: 0x060076F8 RID: 30456 RVA: 0x002CCED8 File Offset: 0x002CB0D8
		public static CookingRecipe FromIngredients(List<GameObject> ingredients, ProceduralCookingEffect effect = null, string chefName = null, string dishNames = "generic")
		{
			CookingRecipe cookingRecipe = new CookingRecipe();
			List<string> list = new List<string>(3);
			List<string> list2 = new List<string>(3);
			List<string> list3 = new List<string>(3);
			foreach (GameObject gameObject in ingredients)
			{
				if (gameObject == null)
				{
					Debug.LogError("Null in ingredient list");
				}
				else if (gameObject.HasPart<LiquidVolume>())
				{
					if (gameObject.LiquidVolume.GetPrimaryLiquid() != null)
					{
						string preparedCookingIngredientLiquidDomainPairs = gameObject.LiquidVolume.GetPreparedCookingIngredientLiquidDomainPairs();
						string[] array = preparedCookingIngredientLiquidDomainPairs.Split(',', StringSplitOptions.None).GetRandomElement<string>(null).Split(':', StringSplitOptions.None);
						if (array.Length == 2)
						{
							string text = array[1];
							string text2 = array[0];
							if (!list.Contains(text))
							{
								list.Add(text);
							}
							cookingRecipe.Components.Add(new PreparedCookingRecipieComponentLiquid(gameObject.LiquidVolume.GetPrimaryLiquid().ID, 1));
							list2.Add(text2);
							list3.Add(text2);
						}
						else
						{
							Debug.LogWarning("Weird return from liquid: " + preparedCookingIngredientLiquidDomainPairs);
						}
					}
				}
				else if (gameObject.HasPart<PreparedCookingIngredient>())
				{
					PreparedCookingIngredient part = gameObject.GetPart<PreparedCookingIngredient>();
					string domain = part.GetTypeInstance();
					if (!list.Contains(domain))
					{
						list.Add(domain);
					}
					if (part.type == "random" && effect == null)
					{
						GameObject anObjectNoExclusions = EncountersAPI.GetAnObjectNoExclusions((GameObjectBlueprint ob) => ob.GetPartParameter<string>("PreparedCookingIngredient", "type", "").Contains(domain) || (ob.HasTag("LiquidCookingIngredient", false) && ob.createSample().LiquidVolume.GetPreparedCookingIngredient().Contains(domain)));
						if (anObjectNoExclusions.HasTag("LiquidCookingIngredient"))
						{
							string id = anObjectNoExclusions.LiquidVolume.GetPrimaryLiquid().ID;
							cookingRecipe.Components.Add(new PreparedCookingRecipieComponentLiquid(id, 1));
							list2.Add(id);
							list3.Add(id);
						}
						cookingRecipe.Components.Add(new PreparedCookingRecipieComponentBlueprint(anObjectNoExclusions.Blueprint, anObjectNoExclusions.GetTag("ServingName", anObjectNoExclusions.DisplayNameOnlyDirect), 1));
						list2.Add(anObjectNoExclusions.Blueprint);
						list3.Add(null);
					}
					else
					{
						cookingRecipe.Components.Add(new PreparedCookingRecipieComponentBlueprint(gameObject.Blueprint, gameObject.GetTag("ServingName", gameObject.DisplayNameOnlyDirect), 1));
						list2.Add(gameObject.Blueprint);
						list3.Add(null);
					}
				}
				else
				{
					Debug.LogWarning("Ingredient " + gameObject.Blueprint + " has neither LiquidVolume nor PreparedCookingIngredient parts");
				}
			}
			ProceduralCookingEffect proceduralCookingEffect = null;
			if (effect == null)
			{
				IEnumerable<string[]> enumerable = Algorithms.GeneratePermutations<string>(list);
				List<List<string>> list4 = new List<List<string>>();
				foreach (string[] array2 in enumerable)
				{
					List<string> list5 = new List<string>(array2.Length);
					foreach (string text3 in array2)
					{
						list5.Add(text3);
					}
					list4.Add(list5);
				}
				list4.Add(null);
				list4.ShuffleInPlace<List<string>>(null);
				for (int j = 0; j < list4.Count; j++)
				{
					if (list4[j] != null && list4[j].Count == 0)
					{
						throw new InvalidOperationException("There weren't any types in the selected ingredient type permutations? (should be impossible)");
					}
					if (list4[j] == null || list4[j].Count == 1)
					{
						proceduralCookingEffect = ProceduralCookingEffect.CreateJustUnits(list);
						break;
					}
					if (list4[j].Count == 2)
					{
						if (Campfire.HasTriggers(list4[j][0]) && Campfire.HasActions(list4[j][1]))
						{
							proceduralCookingEffect = ProceduralCookingEffect.CreateTriggeredAction(list4[j][0], list4[j][1]);
							break;
						}
					}
					else if (Campfire.HasUnits(list4[j][0]) && Campfire.HasTriggers(list4[j][1]) && Campfire.HasActions(list4[j][2]))
					{
						proceduralCookingEffect = ProceduralCookingEffect.CreateBaseAndTriggeredAction(list4[j][0], list4[j][1], list4[j][2]);
						break;
					}
				}
			}
			else
			{
				proceduralCookingEffect = effect;
			}
			CookingRecipeResultProceduralEffect cookingRecipeResultProceduralEffect = new CookingRecipeResultProceduralEffect(proceduralCookingEffect);
			cookingRecipe.Effects.Add(cookingRecipeResultProceduralEffect);
			cookingRecipe.DisplayName = CookingRecipe.GenerateRecipeName(list2, list3, dishNames);
			cookingRecipe.ChefName = chefName;
			cookingRecipe.Tile = CookingRecipe.GenerateRecipeTile(cookingRecipe);
			return cookingRecipe;
		}

		// Token: 0x060076F9 RID: 30457 RVA: 0x002CD39C File Offset: 0x002CB59C
		public static string RollOvenSafeIngredient(string ingredientTable)
		{
			int num = 0;
			string blueprint;
			do
			{
				blueprint = PopulationManager.RollOneFrom(ingredientTable, null, null).Blueprint;
				num++;
			}
			while (blueprint == "Psychal Gland Paste" || blueprint == "FluxPhial" || blueprint == "CloningDraughtWaterskin_Ingredient" || blueprint == "Drop of Nectar" || (blueprint == "Wild Rice" && num <= 39));
			if (num == 40)
			{
				return "Starapple Preserves";
			}
			return blueprint;
		}

		// Token: 0x060076FA RID: 30458 RVA: 0x002CD40F File Offset: 0x002CB60F
		public static bool RecipeNameIsLike(string recipeName, string ingredient)
		{
			return recipeName.ToLower().Contains(ingredient);
		}

		// Token: 0x060076FB RID: 30459 RVA: 0x002CD420 File Offset: 0x002CB620
		public static Renderable GenerateRecipeTile(CookingRecipe recipie)
		{
			Renderable renderable = new Renderable();
			foreach (string text in CookingRecipe.ingredientTileTypes)
			{
				if (CookingRecipe.RecipeNameIsLike(recipie.GetDisplayName(), text))
				{
					renderable.Tile = "Items/sw_food_" + text + ".png";
				}
			}
			if (renderable.Tile == null)
			{
				List<string> list = new List<string>();
				list.AddRange(CookingRecipe.ingredientTileTypes.Select<string, string>((string s) => "Items/sw_food_" + s + ".png"));
				for (int i = 0; i < CookingRecipe.EXTRA_RECIPE_TILES; i++)
				{
					list.Add(string.Format("Items/sw_food_{0}.png", i));
				}
				renderable.Tile = list.GetRandomElement<string>(null);
			}
			List<string> list2 = new List<string>();
			list2.AddRange(Crayons.BrightColors);
			list2.AddRange(Crayons.DarkColors);
			renderable.DetailColor = list2.RemoveRandomElement<string>(null)[0];
			renderable.ColorString = "&" + list2.RemoveRandomElement<string>(null);
			return renderable;
		}

		// Token: 0x060076FC RID: 30460 RVA: 0x002CD558 File Offset: 0x002CB758
		public static string GenerateRecipeName(List<string> ingredients, List<string> liquids, string dishNames = "generic")
		{
			string text = "";
			int num = Stat.Random(0, 2147483646);
			ingredients.ShuffleInPlace<string>(new global::System.Random(num));
			liquids.ShuffleInPlace<string>(new global::System.Random(num));
			if (ingredients.Count == 1)
			{
				string text2 = ((liquids[0] == null && GameObjectFactory.Factory.CreateObject(ingredients[0]).HasTag("AggregatedIngredient")) ? GameObjectFactory.Factory.CreateObject(ingredients[0]).GetTag("AggregatedIngredient", null) : ingredients[0]);
				string text3 = ((liquids[0] == null) ? GameObjectFactory.Factory.CreateObject(ingredients[0]).DisplayNameOnlyStripped : liquids[0]);
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				dictionary.Set("$ingredient1", text2);
				dictionary.Set(("$" + text2 + "DisplayName").Replace(" ", ""), text3);
				dictionary.Set("*dishName*", "<spice.cooking.recipeNames.categorizedFoods." + dishNames.Split(',', StringSplitOptions.None).GetRandomElement<string>(null) + ".!random>");
				text = HistoricStringExpander.ExpandString("<spice.cooking.recipeNames.oneIngredientMeal.!random>", null, null, dictionary, null);
			}
			else if (ingredients.Count == 2)
			{
				string text4 = ((liquids[0] == null && GameObjectFactory.Factory.CreateObject(ingredients[0]).HasTag("AggregatedIngredient")) ? GameObjectFactory.Factory.CreateObject(ingredients[0]).GetTag("AggregatedIngredient", null) : ingredients[0]);
				string text5 = ((liquids[1] == null && GameObjectFactory.Factory.CreateObject(ingredients[1]).HasTag("AggregatedIngredient")) ? GameObjectFactory.Factory.CreateObject(ingredients[1]).GetTag("AggregatedIngredient", null) : ingredients[1]);
				string text6 = ((liquids[0] == null) ? GameObjectFactory.Factory.CreateObject(ingredients[0]).DisplayNameOnlyStripped : liquids[0]);
				string text7 = ((liquids[1] == null) ? GameObjectFactory.Factory.CreateObject(ingredients[1]).DisplayNameOnlyStripped : liquids[1]);
				Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
				dictionary2.Set("$ingredient1", text4);
				dictionary2.Set(("$" + text4 + "DisplayName").Replace(" ", ""), text6);
				dictionary2.Set("$ingredient2", text5);
				dictionary2.Set(("$" + text5 + "DisplayName").Replace(" ", ""), text7);
				dictionary2.Set("*dishName*", "<spice.cooking.recipeNames.categorizedFoods." + dishNames.Split(',', StringSplitOptions.None).GetRandomElement<string>(null) + ".!random>");
				text = HistoricStringExpander.ExpandString("<spice.cooking.recipeNames.twoIngredientMeal.!random>", null, null, dictionary2, null);
			}
			else if (ingredients.Count == 3)
			{
				string text8 = ((liquids[0] == null && GameObjectFactory.Factory.CreateObject(ingredients[0]).HasTag("AggregatedIngredient")) ? GameObjectFactory.Factory.CreateObject(ingredients[0]).GetTag("AggregatedIngredient", null) : ingredients[0]);
				string text9 = ((liquids[1] == null && GameObjectFactory.Factory.CreateObject(ingredients[1]).HasTag("AggregatedIngredient")) ? GameObjectFactory.Factory.CreateObject(ingredients[1]).GetTag("AggregatedIngredient", null) : ingredients[1]);
				string text10 = ((liquids[2] == null && GameObjectFactory.Factory.CreateObject(ingredients[2]).HasTag("AggregatedIngredient")) ? GameObjectFactory.Factory.CreateObject(ingredients[2]).GetTag("AggregatedIngredient", null) : ingredients[2]);
				string text11 = ((liquids[0] == null) ? GameObjectFactory.Factory.CreateObject(ingredients[0]).DisplayNameOnlyStripped : liquids[0]);
				string text12 = ((liquids[1] == null) ? GameObjectFactory.Factory.CreateObject(ingredients[1]).DisplayNameOnlyStripped : liquids[1]);
				string text13 = ((liquids[2] == null) ? GameObjectFactory.Factory.CreateObject(ingredients[2]).DisplayNameOnlyStripped : liquids[2]);
				Dictionary<string, string> dictionary3 = new Dictionary<string, string>();
				dictionary3.Set("$ingredient1", text8);
				dictionary3.Set(("$" + text8 + "DisplayName").Replace(" ", ""), text11);
				dictionary3.Set("$ingredient2", text9);
				dictionary3.Set(("$" + text9 + "DisplayName").Replace(" ", ""), text12);
				dictionary3.Set("$ingredient3", text10);
				dictionary3.Set(("$" + text10 + "DisplayName").Replace(" ", ""), text13);
				dictionary3.Set("*dishName*", "<spice.cooking.recipeNames.categorizedFoods." + dishNames.Split(',', StringSplitOptions.None).GetRandomElement<string>(null) + ".!random>");
				text = HistoricStringExpander.ExpandString("<spice.cooking.recipeNames.threeIngredientMeal.!random>", null, null, dictionary3, null);
			}
			text = HistoricStringExpander.ExpandString(text, null);
			return Grammar.MakeTitleCase(text.Replace("- ", "-"));
		}

		// Token: 0x040036C0 RID: 14016
		public bool Hidden;

		// Token: 0x040036C1 RID: 14017
		public bool Favorite;

		/// <summary>
		/// Don't read from this. Call GetDisplayName() instead.
		/// </summary>
		// Token: 0x040036C2 RID: 14018
		public string DisplayName;

		// Token: 0x040036C3 RID: 14019
		public string ChefName;

		// Token: 0x040036C4 RID: 14020
		public List<ICookingRecipeComponent> Components = new List<ICookingRecipeComponent>();

		// Token: 0x040036C5 RID: 14021
		public List<ICookingRecipeResult> Effects = new List<ICookingRecipeResult>();

		// Token: 0x040036C6 RID: 14022
		[NonSerialized]
		private string CachedDisplayName;

		// Token: 0x040036C7 RID: 14023
		public Renderable Tile;

		// Token: 0x040036C8 RID: 14024
		public static List<string> ingredientTileTypes = new List<string>
		{
			"cake", "bread", "loaf", "slaw", "stew", "soup", "brisket", "borscht", "dip", "baklava",
			"compote", "hash", "porridge", "matz", "cookies", "yogurt", "goulash", "rice", "hummus", "knish",
			"broth", "kugel", "latkes", "schnitzel", "pancake", "roast", "shawarma", "flatbread", "meatballs", "pastry",
			"casserole", "dumpling", "doughnut", "tajine", "couscous", "dolma", "kebab", "fillet"
		};

		// Token: 0x040036C9 RID: 14025
		public static int EXTRA_RECIPE_TILES = 12;
	}
}
