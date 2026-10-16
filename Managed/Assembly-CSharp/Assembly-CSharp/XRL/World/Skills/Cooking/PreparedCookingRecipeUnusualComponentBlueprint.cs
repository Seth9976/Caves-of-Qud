using System;
using System.Collections.Generic;
using XRL.Language;
using XRL.World.Parts;

namespace XRL.World.Skills.Cooking
{
	// Token: 0x02000BB8 RID: 3000
	[Serializable]
	public class PreparedCookingRecipeUnusualComponentBlueprint : ICookingRecipeComponent, IComposite
	{
		// Token: 0x06007745 RID: 30533 RVA: 0x002CE9F0 File Offset: 0x002CCBF0
		public PreparedCookingRecipeUnusualComponentBlueprint()
		{
		}

		// Token: 0x06007746 RID: 30534 RVA: 0x002CE9F8 File Offset: 0x002CCBF8
		public PreparedCookingRecipeUnusualComponentBlueprint(string ingredientType, string displayName = null, int amount = 1)
		{
			this.ingredientBlueprint = ingredientType;
			if (displayName == null)
			{
				if (ingredientType.Contains('|'))
				{
					this.ingredientDisplayName = Grammar.MakeOrList(ingredientType.Split('|', StringSplitOptions.None), true);
				}
				else
				{
					this.ingredientDisplayName = GameObjectFactory.Factory.CreateObject(this.ingredientBlueprint).DisplayNameOnlyDirect;
				}
			}
			else
			{
				this.ingredientDisplayName = displayName;
			}
			this.amount = amount;
		}

		// Token: 0x06007747 RID: 30535 RVA: 0x002CEA61 File Offset: 0x002CCC61
		public string getIngredientId()
		{
			return "blueprint-" + this.ingredientBlueprint;
		}

		// Token: 0x06007748 RID: 30536 RVA: 0x002CEA74 File Offset: 0x002CCC74
		public bool HasPlants()
		{
			foreach (string text in this.ingredientBlueprint.Split('|', StringSplitOptions.None))
			{
				if (GameObjectFactory.Factory.Blueprints[text].HasTag("Plant", false))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06007749 RID: 30537 RVA: 0x002CEAC4 File Offset: 0x002CCCC4
		public bool HasFungi()
		{
			foreach (string text in this.ingredientBlueprint.Split('|', StringSplitOptions.None))
			{
				if (GameObjectFactory.Factory.Blueprints[text].HasTag("Fungus", false))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600774A RID: 30538 RVA: 0x002CEB14 File Offset: 0x002CCD14
		public string getDisplayName()
		{
			GameObject gameObject = GameObjectFactory.Factory.Blueprints[this.ingredientBlueprint].createSample();
			if (this.doesPlayerHaveEnough())
			{
				if (this.amount > 1)
				{
					return "{{C|" + this.amount.ToString() + "}} " + gameObject.GetPluralName(false, false, false, false);
				}
				return "{{C|" + this.amount.ToString() + "}} " + this.ingredientDisplayName;
			}
			else
			{
				if (this.amount > 1)
				{
					return "{{r|" + this.amount.ToString() + "}} " + gameObject.GetPluralName(false, false, false, false);
				}
				return "{{r|" + this.amount.ToString() + "}} " + this.ingredientDisplayName;
			}
		}

		// Token: 0x0600774B RID: 30539 RVA: 0x002CEBE4 File Offset: 0x002CCDE4
		public int PlayerHolding()
		{
			int num = 0;
			string[] array = this.ingredientBlueprint.Split('|', StringSplitOptions.None);
			foreach (GameObject gameObject in CookingGameState.GetInventorySnapshot())
			{
				if (array.Contains(gameObject.Blueprint))
				{
					num += gameObject.Count;
				}
			}
			return num;
		}

		// Token: 0x0600774C RID: 30540 RVA: 0x002CEC58 File Offset: 0x002CCE58
		public bool doesPlayerHaveEnough()
		{
			return this.amount <= CookingGameState.GetIngredientQuantity(this);
		}

		// Token: 0x0600774D RID: 30541 RVA: 0x002CEC6B File Offset: 0x002CCE6B
		public string createPlayerDoesNotHaveEnoughMessage()
		{
			return "You don't have enough servings of " + this.ingredientDisplayName + ".";
		}

		// Token: 0x0600774E RID: 30542 RVA: 0x002CEC84 File Offset: 0x002CCE84
		public void use(List<GameObject> used)
		{
			int num = this.amount;
			for (;;)
			{
				IL_0007:
				string[] array = this.ingredientBlueprint.Split('|', StringSplitOptions.None);
				foreach (GameObject gameObject in CookingGameState.GetInventorySnapshot())
				{
					if (array.Contains(gameObject.Blueprint))
					{
						used.Add(gameObject);
						gameObject.GetPart<PreparedCookingIngredient>();
						if (num > 0)
						{
							num--;
							gameObject.SplitFromStack();
							gameObject.Destroy(null, false, false, null);
							goto IL_0007;
						}
						break;
					}
				}
				break;
			}
			CookingGameState.ResetInventorySnapshot();
		}

		// Token: 0x040036CF RID: 14031
		public string ingredientBlueprint;

		// Token: 0x040036D0 RID: 14032
		public string ingredientDisplayName;

		// Token: 0x040036D1 RID: 14033
		public int amount;
	}
}
