using System;
using System.Collections.Generic;
using XRL.World.Parts;

namespace XRL.World.Skills.Cooking
{
	// Token: 0x02000BB9 RID: 3001
	[Serializable]
	public class PreparedCookingRecipieComponentLiquid : ICookingRecipeComponent, IComposite
	{
		// Token: 0x0600774F RID: 30543 RVA: 0x002CED24 File Offset: 0x002CCF24
		public PreparedCookingRecipieComponentLiquid()
		{
		}

		// Token: 0x06007750 RID: 30544 RVA: 0x002CED2C File Offset: 0x002CCF2C
		public PreparedCookingRecipieComponentLiquid(string liquid, int amount = 1)
		{
			this.liquid = liquid;
			this.amount = amount;
		}

		// Token: 0x06007751 RID: 30545 RVA: 0x002CED42 File Offset: 0x002CCF42
		public string getIngredientId()
		{
			return "liquid-" + this.liquid;
		}

		// Token: 0x06007752 RID: 30546 RVA: 0x002CED54 File Offset: 0x002CCF54
		public bool HasPlants()
		{
			return this.liquid == "sap" || this.liquid == "cider";
		}

		// Token: 0x06007753 RID: 30547 RVA: 0x002CED7F File Offset: 0x002CCF7F
		public bool HasFungi()
		{
			return false;
		}

		// Token: 0x06007754 RID: 30548 RVA: 0x002CED84 File Offset: 0x002CCF84
		public string getDisplayName()
		{
			string text = ((this.amount > 1) ? "drams" : "dram");
			if (this.doesPlayerHaveEnough())
			{
				return string.Concat(new string[]
				{
					this.amount.ToString(),
					" ",
					text,
					" of ",
					LiquidVolume.GetLiquid(this.liquid).GetName()
				});
			}
			return string.Concat(new string[]
			{
				"{{r|",
				this.amount.ToString(),
				"}} ",
				text,
				" of ",
				LiquidVolume.GetLiquid(this.liquid).GetName()
			});
		}

		// Token: 0x06007755 RID: 30549 RVA: 0x002CEE44 File Offset: 0x002CD044
		public int PlayerHolding()
		{
			int num = 0;
			foreach (GameObject gameObject in CookingGameState.GetInventorySnapshot())
			{
				LiquidVolume part = gameObject.GetPart<LiquidVolume>();
				if (part != null && part.IsPureLiquid(this.liquid, false))
				{
					num += part.Volume;
				}
			}
			return num;
		}

		// Token: 0x06007756 RID: 30550 RVA: 0x002CEEB4 File Offset: 0x002CD0B4
		public bool doesPlayerHaveEnough()
		{
			return this.amount <= CookingGameState.GetIngredientQuantity(this);
		}

		// Token: 0x06007757 RID: 30551 RVA: 0x002CEEC7 File Offset: 0x002CD0C7
		public string createPlayerDoesNotHaveEnoughMessage()
		{
			return "You don't have enough " + LiquidVolume.GetLiquid(this.liquid).GetName(null) + "&Y.";
		}

		// Token: 0x06007758 RID: 30552 RVA: 0x002CEEF0 File Offset: 0x002CD0F0
		public void use(List<GameObject> used)
		{
			int num = this.amount;
			for (;;)
			{
				IL_0007:
				foreach (GameObject gameObject in CookingGameState.GetInventorySnapshot())
				{
					LiquidVolume part = gameObject.GetPart<LiquidVolume>();
					if (part != null && part.IsPureLiquid(this.liquid, false))
					{
						used.Add(gameObject);
						if (num > part.Volume)
						{
							num -= part.Volume;
							part.Volume = 0;
							part.Empty(false, false);
							goto IL_0007;
						}
						part.UseDrams(num);
						break;
					}
				}
				break;
			}
			CookingGameState.ResetInventorySnapshot();
		}

		// Token: 0x040036D2 RID: 14034
		public string liquid;

		// Token: 0x040036D3 RID: 14035
		public int amount;
	}
}
