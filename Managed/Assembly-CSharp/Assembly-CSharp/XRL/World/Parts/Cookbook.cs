using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using HistoryKit;
using Occult.Engine.CodeGeneration;
using Qud.API;
using XRL.Core;
using XRL.Language;
using XRL.Rules;
using XRL.UI;
using XRL.World.Skills.Cooking;

namespace XRL.World.Parts
{
	// Token: 0x02000DCB RID: 3531
	[GenerateSerializationPartial]
	[Serializable]
	public class Cookbook : IPart
	{
		// Token: 0x06008F8E RID: 36750 RVA: 0x0033C130 File Offset: 0x0033A330
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.Tier);
			Writer.WriteOptimized(this.NumberOfIngredients);
			Writer.Write(this.id);
			Writer.WriteOptimized(this.Style);
			Writer.WriteOptimized(this.ChefName);
			Writer.WriteComposite<CookingRecipe>(this.recipes);
			Writer.Write<bool>(this.readPage);
		}

		// Token: 0x06008F8F RID: 36751 RVA: 0x0033C194 File Offset: 0x0033A394
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			this.Tier = Reader.ReadOptimizedInt32();
			this.NumberOfIngredients = Reader.ReadOptimizedString();
			this.id = Reader.ReadGuid();
			this.Style = Reader.ReadOptimizedString();
			this.ChefName = Reader.ReadOptimizedString();
			this.recipes = Reader.ReadCompositeList<CookingRecipe>();
			this.readPage = Reader.ReadList<bool>();
		}

		// Token: 0x06008F90 RID: 36752 RVA: 0x0033C1F5 File Offset: 0x0033A3F5
		public Cookbook()
		{
		}

		// Token: 0x06008F91 RID: 36753 RVA: 0x0033C230 File Offset: 0x0033A430
		public Cookbook(string Style, string NumberOfIngredients, int Tier, string ChefName)
			: this()
		{
			this.Style = Style;
			this.NumberOfIngredients = NumberOfIngredients;
			this.Tier = Tier;
			this.ChefName = ChefName;
		}

		// Token: 0x06008F92 RID: 36754 RVA: 0x0033C255 File Offset: 0x0033A455
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06008F93 RID: 36755 RVA: 0x0033C258 File Offset: 0x0033A458
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID || ID == ObjectCreatedEvent.ID;
		}

		// Token: 0x06008F94 RID: 36756 RVA: 0x0033C280 File Offset: 0x0033A480
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			E.AddAction("Read", "read", "Read", null, 'r', false, this.GetHasBeenRead() ? 0 : 100, 0, false, false, false, false, true, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x06008F95 RID: 36757 RVA: 0x0033C2C4 File Offset: 0x0033A4C4
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "Read")
			{
				if (!E.Actor.IsPlayer())
				{
					return false;
				}
				if (this.bookText == null)
				{
					this.bookText = new List<string>();
					foreach (CookingRecipe cookingRecipe in this.recipes)
					{
						this.bookText.Add(string.Concat(new string[]
						{
							cookingRecipe.GetDisplayName(),
							"\n\n",
							cookingRecipe.GetIngredients(),
							"\n\n",
							cookingRecipe.GetDescription()
						}));
					}
				}
				List<Action> afterClosed = new List<Action>();
				BookUI.ShowBook(this.bookText, this.ParentObject.DisplayName, "Sounds/Interact/sfx_interact_book_read", delegate(int p)
				{
					if (!this.readPage[p])
					{
						this.readPage[p] = true;
						afterClosed.Add(delegate
						{
							CookingGameState.LearnRecipe(this.recipes[p], null);
						});
					}
				}, null);
				foreach (Action action in afterClosed)
				{
					action();
				}
				AfterReadBookEvent.Send(E.Actor, this.ParentObject, this, E);
				if (!this.GetHasBeenRead())
				{
					this.SetHasBeenRead(true);
					string referenceDisplayName = this.ParentObject.GetReferenceDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, false, false, null, false);
					JournalAPI.AddAccomplishment("You read " + referenceDisplayName + ".", string.Concat(new string[]
					{
						"In the month of ",
						Calendar.GetMonth(),
						" of ",
						Calendar.GetYear().ToString(),
						", =name= penned the influential cookbook, ",
						referenceDisplayName,
						"."
					}), string.Concat(new string[]
					{
						"At a remote library near ",
						JournalAPI.GetLandmarkNearestPlayer().Text,
						", =name= met with a group of tongueless kippers and together they penned the beloved cookbook ",
						referenceDisplayName,
						"."
					}), null, "general", MuralCategory.CreatesSomething, MuralWeight.VeryLow, null, -1L, true);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008F96 RID: 36758 RVA: 0x0033C4F4 File Offset: 0x0033A6F4
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			this.GenerateCookbook();
			return base.HandleEvent(E);
		}

		// Token: 0x06008F97 RID: 36759 RVA: 0x0033C504 File Offset: 0x0033A704
		public void GenerateCookbook()
		{
			if (this.Tier == -1)
			{
				this.Tier = 0;
				this.Tier = ZoneManager.zoneGenerationContextTier;
			}
			this.id = Guid.NewGuid();
			if (this.Style == "Generic")
			{
				this.ParentObject.Render.DisplayName = HistoricStringExpander.ExpandString("<spice.cooking.cookbooks.genericName.!random>", null);
			}
			if (this.Style == "Generic_LegendaryChef")
			{
				this.ParentObject.Render.DisplayName = "Chef " + Grammar.MakePossessive(this.ChefName.Split(',', StringSplitOptions.None)[0]) + " " + HistoricStringExpander.ExpandString("<spice.cooking.cookbooks.genericName.!random>", null);
			}
			if (this.Style == "Focal")
			{
				this.ParentObject.Render.DisplayName = HistoricStringExpander.ExpandString("<spice.cooking.cookbooks.focalName.!random>", null);
			}
			int num = Stat.Roll(this.NumberOfIngredients, null);
			GameObject gameObject = null;
			if (this.Style == "Focal")
			{
				string text = "";
				gameObject = GameObjectFactory.Factory.CreateObject(CookingRecipe.RollOvenSafeIngredient("Ingredients" + this.Tier.ToString()));
				if (gameObject.HasPart<LiquidVolume>())
				{
					if (gameObject.LiquidVolume.GetPrimaryLiquid() != null)
					{
						text = gameObject.LiquidVolume.GetPreparedCookingIngredientLiquidDomainPairs().Split(',', StringSplitOptions.None).GetRandomElement<string>(null)
							.Split(':', StringSplitOptions.None)[0];
					}
				}
				else
				{
					text = gameObject.DisplayNameOnlyStripped;
				}
				if (!gameObject.HasTag("Preservable") && !gameObject.HasPart<LiquidVolume>())
				{
					this.ParentObject.Render.DisplayName = Grammar.Pluralize(this.ParentObject.Render.DisplayName.Replace("$focus", text));
				}
				else
				{
					this.ParentObject.Render.DisplayName = this.ParentObject.Render.DisplayName.Replace("$focus", text);
				}
			}
			this.ParentObject.Render.DisplayName = "&g" + Grammar.MakeTitleCase(this.ParentObject.Render.DisplayName.Replace("$markovTitle", GameObjectFactory.Factory.CreateObject("StandaloneMarkovBook").DisplayNameOnlyDirect));
			for (int i = 0; i < num; i++)
			{
				List<GameObject> list = new List<GameObject>();
				int num2 = 25;
				int num3 = 50;
				if (this.Tier >= 0 && this.Tier <= 1)
				{
					num2 = 25;
					num3 = 85;
				}
				if (this.Tier >= 2 && this.Tier <= 3)
				{
					num2 = 15;
					num3 = 65;
				}
				if (this.Tier >= 4 && this.Tier <= 5)
				{
					num2 = 5;
					num3 = 52;
				}
				if (this.Tier >= 6 && this.Tier <= 7)
				{
					num2 = 0;
					num3 = 35;
				}
				if (this.Tier >= 8)
				{
					num2 = 0;
					num3 = 25;
				}
				int num4 = Stat.Random(1, 100);
				int num5;
				if (num4 <= num2)
				{
					num5 = 1;
				}
				else if (num4 <= num3)
				{
					num5 = 2;
				}
				else
				{
					num5 = 3;
				}
				Commerce part = this.ParentObject.GetPart<Commerce>();
				if (part != null)
				{
					part.Value = (double)(50 + 10 * this.Tier);
				}
				if (this.Style == "Focal")
				{
					list.Add(gameObject);
					num5--;
				}
				int num6 = 0;
				for (int j = 0; j < num5; j++)
				{
					string ingredientBlueprint = CookingRecipe.RollOvenSafeIngredient("Ingredients" + this.Tier.ToString());
					if (num6 < 50 && list.Any<GameObject>((GameObject o) => o.Blueprint == ingredientBlueprint))
					{
						j--;
					}
					else
					{
						list.Add(GameObject.Create(ingredientBlueprint));
					}
					num6++;
				}
				CookingRecipe cookingRecipe;
				if (this.ChefName == null)
				{
					cookingRecipe = CookingRecipe.FromIngredients(list, null, null, "generic");
				}
				else
				{
					cookingRecipe = CookingRecipe.FromIngredients(list, null, "Chef " + this.ChefName.Split(',', StringSplitOptions.None)[0], "generic");
				}
				this.recipes.Add(cookingRecipe);
				this.readPage.Add(false);
			}
		}

		// Token: 0x06008F98 RID: 36760 RVA: 0x0033C90C File Offset: 0x0033AB0C
		public string GetBookKey()
		{
			string text = "AlreadyRead_";
			Guid guid = this.id;
			return text + guid.ToString();
		}

		// Token: 0x06008F99 RID: 36761 RVA: 0x0033C937 File Offset: 0x0033AB37
		public bool GetHasBeenRead()
		{
			return XRLCore.Core.Game.HasStringGameState(this.GetBookKey());
		}

		// Token: 0x06008F9A RID: 36762 RVA: 0x0033C94E File Offset: 0x0033AB4E
		public void SetHasBeenRead(bool flag)
		{
			if (flag)
			{
				XRLCore.Core.Game.SetStringGameState(this.GetBookKey(), "Yes");
				return;
			}
			XRLCore.Core.Game.SetStringGameState(this.GetBookKey(), "");
		}

		// Token: 0x04003BE9 RID: 15337
		public int Tier = 1;

		// Token: 0x04003BEA RID: 15338
		public string NumberOfIngredients = "2-4";

		// Token: 0x04003BEB RID: 15339
		public Guid id;

		// Token: 0x04003BEC RID: 15340
		public string Style = "Generic";

		// Token: 0x04003BED RID: 15341
		public string ChefName;

		// Token: 0x04003BEE RID: 15342
		public List<CookingRecipe> recipes = new List<CookingRecipe>();

		// Token: 0x04003BEF RID: 15343
		public List<bool> readPage = new List<bool>();

		// Token: 0x04003BF0 RID: 15344
		[NonSerialized]
		public List<string> bookText;
	}
}
