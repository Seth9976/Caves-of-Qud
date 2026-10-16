using System;

namespace XRL.World.Parts
{
	// Token: 0x020011C8 RID: 4552
	[Serializable]
	public class FreePower : IPoweredPart
	{
		// Token: 0x0600B4FC RID: 46332 RVA: 0x00420EC3 File Offset: 0x0041F0C3
		public FreePower()
		{
			this.ChargeUse = 0;
			this.IsBootSensitive = false;
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B4FD RID: 46333 RVA: 0x00420EEF File Offset: 0x0041F0EF
		public bool HasCharge(int Amount)
		{
			return this.ChargeFulfilled >= Amount;
		}

		// Token: 0x0600B4FE RID: 46334 RVA: 0x00420EFD File Offset: 0x0041F0FD
		public int GetCharge()
		{
			return this.ChargeFulfilled;
		}

		// Token: 0x0600B4FF RID: 46335 RVA: 0x00420F08 File Offset: 0x0041F108
		public override bool SameAs(IPart p)
		{
			FreePower freePower = p as FreePower;
			return freePower.ChargeFulfilled == this.ChargeFulfilled && freePower.ConsiderLive == this.ConsiderLive && base.SameAs(p);
		}

		// Token: 0x0600B500 RID: 46336 RVA: 0x00420F44 File Offset: 0x0041F144
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<GetDebugInternalsEvent>.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || (ID == PooledEvent<GetElectricalConductivityEvent>.ID && this.ConsiderLive) || ID == QueryChargeEvent.ID || ID == TestChargeEvent.ID || ID == UseChargeEvent.ID;
		}

		// Token: 0x0600B501 RID: 46337 RVA: 0x00420F95 File Offset: 0x0041F195
		public override bool HandleEvent(GetDebugInternalsEvent E)
		{
			E.AddEntry(this, "ChargeFulfilled", this.ChargeFulfilled);
			E.AddEntry(this, "ConsiderLive", this.ConsiderLive);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B502 RID: 46338 RVA: 0x00420FC2 File Offset: 0x0041F1C2
		public override bool HandleEvent(GetElectricalConductivityEvent E)
		{
			if (E.Pass == 1 && E.Object == this.ParentObject && this.ConsiderLive)
			{
				E.MinValue(95);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B503 RID: 46339 RVA: 0x00420FF4 File Offset: 0x0041F1F4
		public override bool HandleEvent(QueryChargeEvent E)
		{
			if ((!E.LiveOnly || this.ConsiderLive) && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				int num = this.ChargeUse * E.Multiple;
				int num2 = this.ChargeFulfilled - num;
				if (num2 > 0)
				{
					E.Amount += num2;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B504 RID: 46340 RVA: 0x0042106C File Offset: 0x0041F26C
		public override bool HandleEvent(TestChargeEvent E)
		{
			if ((!E.LiveOnly || this.ConsiderLive) && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				int num = this.ChargeUse * E.Multiple;
				int num2 = Math.Min(E.Amount, this.ChargeFulfilled - num);
				if (num2 > 0)
				{
					E.Amount -= num2;
					if (E.Amount <= 0)
					{
						return false;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B505 RID: 46341 RVA: 0x004210F8 File Offset: 0x0041F2F8
		public override bool HandleEvent(UseChargeEvent E)
		{
			if (E.Pass == 1 && (!E.LiveOnly || this.ConsiderLive) && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				int num = this.ChargeUse * E.Multiple;
				int num2 = Math.Min(E.Amount, this.ChargeFulfilled - num);
				if (num2 > 0)
				{
					E.Amount -= num2;
					if (E.Amount <= 0)
					{
						return false;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x040048FB RID: 18683
		public int ChargeFulfilled = 10;

		// Token: 0x040048FC RID: 18684
		public bool ConsiderLive = true;
	}
}
