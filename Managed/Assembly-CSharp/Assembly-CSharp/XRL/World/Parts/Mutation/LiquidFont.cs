using System;
using System.Collections.Generic;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x0200141F RID: 5151
	[Serializable]
	public class LiquidFont : BaseMutation
	{
		// Token: 0x0600C883 RID: 51331 RVA: 0x00490014 File Offset: 0x0048E214
		public override string GetLevelText(int Level)
		{
			return "You ooze fluids with the best of them.\n";
		}

		// Token: 0x0600C884 RID: 51332 RVA: 0x0049001B File Offset: 0x0048E21B
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == EnteredCellEvent.ID && this.Prefill);
		}

		// Token: 0x0600C885 RID: 51333 RVA: 0x00490039 File Offset: 0x0048E239
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (this.Prefill)
			{
				this.Prefill = false;
				this.Bubble(80);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C886 RID: 51334 RVA: 0x00490059 File Offset: 0x0048E259
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600C887 RID: 51335 RVA: 0x0049005C File Offset: 0x0048E25C
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.BubbleTick(Amount);
		}

		// Token: 0x0600C888 RID: 51336 RVA: 0x00490068 File Offset: 0x0048E268
		public void BubbleTick(int ticks = 1)
		{
			this.CooldownLeft -= ticks;
			if (this.ParentObject.OnWorldMap())
			{
				return;
			}
			int num = 0;
			while (this.CooldownLeft < 0)
			{
				this.CooldownLeft += this.Cooldown.RollCached();
				if (this.Chance.in100())
				{
					num++;
				}
			}
			if (num > 0)
			{
				this.Bubble(num);
				this.ParentObject.ParticleBlip("&" + LiquidVolume.GetLiquid(this.Liquid).GetColor() + "~", 10, 0L, false);
			}
		}

		// Token: 0x0600C889 RID: 51337 RVA: 0x00490108 File Offset: 0x0048E308
		public void Bubble(int n = 1)
		{
			int num = 1;
			while (num < this.MaxRadius && n > 0)
			{
				List<Cell> list = this.ParentObject.GetCurrentCell().GetLocalAdjacentCells(num, false).ShuffleInPlace<Cell>(null);
				for (int i = 0; i < list.Count; i++)
				{
					GameObject gameObject = list[i].GetOpenLiquidVolume();
					if (gameObject == null)
					{
						Cell cell = list[i];
						gameObject = GameObject.Create("AcidPool");
						gameObject.LiquidVolume.Fill(this.Liquid, this.Amount.RollCached());
						cell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						base.UseEnergy(this.EnergyCost);
						n--;
						if (n <= 0)
						{
							break;
						}
					}
					else
					{
						LiquidVolume liquidVolume = gameObject.LiquidVolume;
						if (liquidVolume.Amount(this.Liquid) < this.MaxAmount)
						{
							liquidVolume.MixWith(new LiquidVolume(this.Liquid, this.Amount.RollCached()), null, null, null, false);
							base.UseEnergy(this.EnergyCost);
							n--;
							if (n <= 0)
							{
								break;
							}
						}
					}
				}
				num++;
			}
		}

		// Token: 0x04004E86 RID: 20102
		public bool Prefill = true;

		// Token: 0x04004E87 RID: 20103
		public int Chance = 100;

		// Token: 0x04004E88 RID: 20104
		public int EnergyCost = 1000;

		// Token: 0x04004E89 RID: 20105
		public string Amount = "500";

		// Token: 0x04004E8A RID: 20106
		public int MaxAmount = 800;

		// Token: 0x04004E8B RID: 20107
		public int MaxRadius = 3;

		// Token: 0x04004E8C RID: 20108
		public string Cooldown = "1d20";

		// Token: 0x04004E8D RID: 20109
		public string Liquid = "lava";

		// Token: 0x04004E8E RID: 20110
		public int CooldownLeft;
	}
}
