using System;
using System.Collections.Generic;
using XRL.Language;
using XRL.World.Parts;

namespace XRL.World.Skills.Cooking
{
	// Token: 0x02000BB7 RID: 2999
	[Serializable]
	public class PreparedCookingRecipieComponentBlueprint : ICookingRecipeComponent, IComposite
	{
		// Token: 0x0600773B RID: 30523 RVA: 0x002CE678 File Offset: 0x002CC878
		public PreparedCookingRecipieComponentBlueprint()
		{
		}

		// Token: 0x0600773C RID: 30524 RVA: 0x002CE680 File Offset: 0x002CC880
		public PreparedCookingRecipieComponentBlueprint(string ingredientType, string displayName = null, int amount = 1)
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

		// Token: 0x0600773D RID: 30525 RVA: 0x002CE6E9 File Offset: 0x002CC8E9
		public string getIngredientId()
		{
			return "blueprint-" + this.ingredientBlueprint;
		}

		// Token: 0x0600773E RID: 30526 RVA: 0x002CE6FC File Offset: 0x002CC8FC
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

		// Token: 0x0600773F RID: 30527 RVA: 0x002CE74C File Offset: 0x002CC94C
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

		// Token: 0x06007740 RID: 30528 RVA: 0x002CE79C File Offset: 0x002CC99C
		public string getDisplayName()
		{
			string text = ((this.amount > 1) ? "servings" : "serving");
			if (this.doesPlayerHaveEnough())
			{
				return string.Concat(new string[]
				{
					"{{C|",
					this.amount.ToString(),
					"}} ",
					text,
					" of ",
					this.ingredientDisplayName
				});
			}
			return string.Concat(new string[]
			{
				"{{r|",
				this.amount.ToString(),
				"}} ",
				text,
				" of ",
				this.ingredientDisplayName
			});
		}

		// Token: 0x06007741 RID: 30529 RVA: 0x002CE844 File Offset: 0x002CCA44
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

		// Token: 0x06007742 RID: 30530 RVA: 0x002CE8B8 File Offset: 0x002CCAB8
		public bool doesPlayerHaveEnough()
		{
			return this.amount <= CookingGameState.GetIngredientQuantity(this);
		}

		// Token: 0x06007743 RID: 30531 RVA: 0x002CE8CB File Offset: 0x002CCACB
		public string createPlayerDoesNotHaveEnoughMessage()
		{
			return "You don't have enough servings of " + this.ingredientDisplayName + ".";
		}

		// Token: 0x06007744 RID: 30532 RVA: 0x002CE8E4 File Offset: 0x002CCAE4
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
						PreparedCookingIngredient part = gameObject.GetPart<PreparedCookingIngredient>();
						if (num > part.charges)
						{
							num -= part.charges;
							part.ParentObject.SplitFromStack();
							part.ParentObject.Destroy(null, false, false, null);
							goto IL_0007;
						}
						part.ParentObject.SplitFromStack();
						part.charges -= num;
						if (part.charges == 0)
						{
							part.ParentObject.Destroy(null, false, false, null);
						}
						else
						{
							part.ParentObject.CheckStack();
						}
						num = 0;
						break;
					}
				}
				break;
			}
			CookingGameState.ResetInventorySnapshot();
		}

		// Token: 0x040036CC RID: 14028
		public string ingredientBlueprint;

		// Token: 0x040036CD RID: 14029
		public string ingredientDisplayName;

		// Token: 0x040036CE RID: 14030
		public int amount;
	}
}
