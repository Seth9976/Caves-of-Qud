using System;
using XRL.UI;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x02000913 RID: 2323
	[Serializable]
	public class TinkeringSifrahTokenCharge : SifrahPrioritizableToken
	{
		// Token: 0x0600589B RID: 22683 RVA: 0x001ACFF8 File Offset: 0x001AB1F8
		public TinkeringSifrahTokenCharge()
		{
			this.Description = "use {{C|1}} charge from an energy cell";
			this.Tile = "Items/sw_electricalgeneration.bmp";
			this.RenderString = "\b";
			this.ColorString = "&W";
			this.DetailColor = 'B';
		}

		// Token: 0x0600589C RID: 22684 RVA: 0x001AD046 File Offset: 0x001AB246
		public TinkeringSifrahTokenCharge(int Amount)
			: this()
		{
			this.Amount = Amount;
			this.Description = "use {{C|" + Amount.ToString() + "}} charge from an energy cell";
		}

		// Token: 0x0600589D RID: 22685 RVA: 0x001AD074 File Offset: 0x001AB274
		public override int GetPriority()
		{
			ElectricalGeneration part = The.Player.GetPart<ElectricalGeneration>();
			if (part != null && part.GetCharge() >= this.Amount)
			{
				return int.MaxValue;
			}
			int num = 0;
			checked
			{
				foreach (GameObject gameObject in The.Player.GetInventoryAndEquipment())
				{
					if (gameObject.HasPartDescendedFrom<IEnergyCell>() && gameObject.Understood())
					{
						try
						{
							num += gameObject.QueryCharge(false, 0L, true, true);
						}
						catch (OverflowException)
						{
							return 2147483646;
						}
					}
				}
				return num;
			}
		}

		// Token: 0x0600589E RID: 22686 RVA: 0x001AD124 File Offset: 0x001AB324
		public override int GetTiebreakerPriority()
		{
			return 2147483646;
		}

		// Token: 0x0600589F RID: 22687 RVA: 0x001AD12C File Offset: 0x001AB32C
		public override string GetDescription(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			string text = this.Description;
			ElectricalGeneration part = The.Player.GetPart<ElectricalGeneration>();
			if (part != null && part.GetCharge() >= this.Amount)
			{
				text = text.Replace("from an energy cell", "via Electrical Generation");
			}
			return text;
		}

		// Token: 0x060058A0 RID: 22688 RVA: 0x001AD170 File Offset: 0x001AB370
		public bool IsAvailable()
		{
			ElectricalGeneration part = The.Player.GetPart<ElectricalGeneration>();
			if (part != null && part.GetCharge() >= this.Amount)
			{
				return true;
			}
			foreach (GameObject gameObject in The.Player.GetInventoryAndEquipment())
			{
				if (gameObject.HasPartDescendedFrom<IEnergyCell>() && gameObject.Understood() && gameObject.TestCharge(this.Amount, false, 0L, true, true))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060058A1 RID: 22689 RVA: 0x001AD208 File Offset: 0x001AB408
		public override bool GetDisabled(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			return !this.IsAvailable() || base.GetDisabled(Game, Slot, ContextObject);
		}

		// Token: 0x060058A2 RID: 22690 RVA: 0x001AD220 File Offset: 0x001AB420
		public override bool CheckTokenUse(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			if (!this.IsAvailable())
			{
				if (The.Player.GetPart<ElectricalGeneration>() != null)
				{
					Popup.ShowFail("You do not have any energy cells with " + this.Amount.ToString() + " charge available, and your electrical generation capacity is unable to meet the demand.", true, true, true);
				}
				else
				{
					Popup.ShowFail("You do not have any energy cells with " + this.Amount.ToString() + " charge available.", true, true, true);
				}
				return false;
			}
			return base.CheckTokenUse(Game, Slot, ContextObject);
		}

		// Token: 0x060058A3 RID: 22691 RVA: 0x001AD294 File Offset: 0x001AB494
		public override void UseToken(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			bool flag = false;
			ElectricalGeneration part = The.Player.GetPart<ElectricalGeneration>();
			if (part != null && part.GetCharge() >= this.Amount)
			{
				part.UseCharge(this.Amount);
				flag = true;
			}
			if (!flag)
			{
				foreach (GameObject gameObject in The.Player.GetInventoryAndEquipment())
				{
					if (gameObject.HasPartDescendedFrom<IEnergyCell>() && gameObject.Understood() && gameObject.TestCharge(this.Amount, false, 0L, true, true))
					{
						gameObject.SplitFromStack();
						try
						{
							if (gameObject.UseCharge(this.Amount, false, 0L, true, true, null))
							{
								break;
							}
						}
						finally
						{
							if (GameObject.Validate(gameObject))
							{
								gameObject.CheckStack();
							}
						}
					}
				}
			}
			base.UseToken(Game, Slot, ContextObject);
		}

		// Token: 0x04002C22 RID: 11298
		public int Amount = 1;
	}
}
