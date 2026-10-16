using System;
using System.Collections.Generic;
using XRL.World.Parts;

namespace XRL.World.Skills.Cooking
{
	// Token: 0x02000BB6 RID: 2998
	[Serializable]
	public class PreparedCookingRecipieComponentDomain : ICookingRecipeComponent, IComposite
	{
		// Token: 0x06007731 RID: 30513 RVA: 0x002CE3D4 File Offset: 0x002CC5D4
		public PreparedCookingRecipieComponentDomain()
		{
		}

		// Token: 0x06007732 RID: 30514 RVA: 0x002CE3DC File Offset: 0x002CC5DC
		public PreparedCookingRecipieComponentDomain(string ingredientType, int amount = 1)
		{
			this.ingredientType = ingredientType;
			this.amount = amount;
		}

		// Token: 0x06007733 RID: 30515 RVA: 0x002CE3F2 File Offset: 0x002CC5F2
		public string getIngredientId()
		{
			return "prepared-" + this.ingredientType;
		}

		// Token: 0x06007734 RID: 30516 RVA: 0x002CE404 File Offset: 0x002CC604
		public bool HasPlants()
		{
			return false;
		}

		// Token: 0x06007735 RID: 30517 RVA: 0x002CE407 File Offset: 0x002CC607
		public bool HasFungi()
		{
			return false;
		}

		// Token: 0x06007736 RID: 30518 RVA: 0x002CE40C File Offset: 0x002CC60C
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
					this.ingredientType
				});
			}
			return string.Concat(new string[]
			{
				"{{r|",
				this.amount.ToString(),
				"}} ",
				text,
				" of ",
				this.ingredientType
			});
		}

		// Token: 0x06007737 RID: 30519 RVA: 0x002CE4B4 File Offset: 0x002CC6B4
		public int PlayerHolding()
		{
			int num = 0;
			foreach (GameObject gameObject in CookingGameState.GetInventorySnapshot())
			{
				PreparedCookingIngredient part = gameObject.GetPart<PreparedCookingIngredient>();
				if (part != null && part.type == this.ingredientType)
				{
					num += part.charges;
				}
			}
			return num;
		}

		// Token: 0x06007738 RID: 30520 RVA: 0x002CE528 File Offset: 0x002CC728
		public bool doesPlayerHaveEnough()
		{
			return this.amount <= CookingGameState.GetIngredientQuantity(this);
		}

		// Token: 0x06007739 RID: 30521 RVA: 0x002CE53B File Offset: 0x002CC73B
		public string createPlayerDoesNotHaveEnoughMessage()
		{
			return "You don't have enough " + this.ingredientType + ".";
		}

		// Token: 0x0600773A RID: 30522 RVA: 0x002CE554 File Offset: 0x002CC754
		public void use(List<GameObject> used)
		{
			int num = this.amount;
			Event @event = Event.New("UsedAsIngredient", "Actor", The.Player);
			for (;;)
			{
				IL_001C:
				foreach (GameObject gameObject in CookingGameState.GetInventorySnapshot())
				{
					PreparedCookingIngredient part = gameObject.GetPart<PreparedCookingIngredient>();
					if (part != null && part.type == this.ingredientType)
					{
						used.Add(gameObject);
						gameObject.FireEvent(@event);
						if (num > part.charges)
						{
							num -= part.charges;
							part.ParentObject.SplitFromStack();
							part.ParentObject.Destroy(null, false, false, null);
							goto IL_001C;
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
						return;
					}
				}
				break;
			}
		}

		// Token: 0x040036CA RID: 14026
		public string ingredientType;

		// Token: 0x040036CB RID: 14027
		public int amount;
	}
}
