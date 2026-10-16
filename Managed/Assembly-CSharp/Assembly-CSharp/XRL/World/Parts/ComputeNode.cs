using System;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: if <see cref="!:IsPowerLoadSensitive" /> is true,
	/// which it is by default, compute power output is increased by a
	/// percentage equal to ((power load - 100) / 10), i.e. 30% for
	/// the standard overload power load of 400.
	/// </remarks>
	// Token: 0x0200100A RID: 4106
	[Serializable]
	public class ComputeNode : IPoweredPart
	{
		// Token: 0x0600A394 RID: 41876 RVA: 0x003BC6BE File Offset: 0x003BA8BE
		public ComputeNode()
		{
			this.WorksOnSelf = true;
			this.IsPowerLoadSensitive = true;
		}

		// Token: 0x0600A395 RID: 41877 RVA: 0x003BC6DC File Offset: 0x003BA8DC
		public override bool WantTurnTick()
		{
			return this.ChargeUse > 0;
		}

		// Token: 0x0600A396 RID: 41878 RVA: 0x003BC6E8 File Offset: 0x003BA8E8
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (!base.OnWorldMap)
			{
				base.ConsumeChargeIfOperational(false, false, false, false, false, false, false, false, false, Amount, null, false, 0, false, null);
			}
		}

		// Token: 0x0600A397 RID: 41879 RVA: 0x003BC723 File Offset: 0x003BA923
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<GetAvailableComputePowerEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600A398 RID: 41880 RVA: 0x003BC744 File Offset: 0x003BA944
		public override bool HandleEvent(GetAvailableComputePowerEvent E)
		{
			if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				E.Amount += this.GetEffectivePower();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A399 RID: 41881 RVA: 0x003BC794 File Offset: 0x003BA994
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.Power != 0 && (this.WorksOnEquipper || this.WorksOnWearer || this.WorksOnHolder || this.WorksOnImplantee))
			{
				E.Postfix.Append("\n{{rules|When ");
				if (this.WorksOnEquipper || this.WorksOnWearer || this.WorksOnHolder)
				{
					if (this.WorksOnImplantee)
					{
						E.Postfix.Append("equipped or implanted");
					}
					else
					{
						E.Postfix.Append("equipped");
					}
				}
				else if (this.WorksOnImplantee)
				{
					E.Postfix.Append("implanted");
				}
				if (this.ChargeUse > 0)
				{
					E.Postfix.Append(" and powered");
				}
				int effectivePower = this.GetEffectivePower();
				E.Postfix.Append(", provides ").Append(effectivePower).Append(' ')
					.Append((effectivePower == 1) ? "unit" : "units")
					.Append(" of compute power to the local lattice.");
				base.AddStatusSummary(E.Postfix);
				E.Postfix.Append("}}");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A39A RID: 41882 RVA: 0x003BC8C4 File Offset: 0x003BAAC4
		public int GetEffectivePower()
		{
			int num = this.Power;
			int num2 = this.MyPowerLoadBonus(int.MinValue, 100, 10);
			if (num2 != 0)
			{
				num = num * (100 + num2) / 100;
			}
			return num;
		}

		// Token: 0x040043C4 RID: 17348
		public int Power = 20;
	}
}
