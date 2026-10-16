using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: if <see cref="!:IsPowerLoadSensitive" /> is true,
	/// which it is by default, charge rate is increased by a percentage
	/// equal to power load, i.e. 400% for the standard overload power load
	/// of 400.
	/// </remarks>
	// Token: 0x020011E1 RID: 4577
	[Serializable]
	public class ZeroPointEnergyCollector : IPoweredPart
	{
		// Token: 0x0600B66E RID: 46702 RVA: 0x004293EC File Offset: 0x004275EC
		public ZeroPointEnergyCollector()
		{
			this.ChargeUse = 0;
			this.IsBootSensitive = false;
			this.IsPowerLoadSensitive = true;
			this.IsRealityDistortionBased = true;
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B66F RID: 46703 RVA: 0x00429440 File Offset: 0x00427640
		public override bool SameAs(IPart p)
		{
			ZeroPointEnergyCollector zeroPointEnergyCollector = p as ZeroPointEnergyCollector;
			return zeroPointEnergyCollector.ChargeRate == this.ChargeRate && !(zeroPointEnergyCollector.World != this.World) && !(zeroPointEnergyCollector.Plane != this.Plane) && base.SameAs(p);
		}

		// Token: 0x0600B670 RID: 46704 RVA: 0x00429495 File Offset: 0x00427695
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == QueryChargeProductionEvent.ID || ID == PrimePowerSystemsEvent.ID;
		}

		// Token: 0x0600B671 RID: 46705 RVA: 0x004294B3 File Offset: 0x004276B3
		public override bool HandleEvent(PrimePowerSystemsEvent E)
		{
			if (this.ParentObject.HasPropertyOrTag("Furniture"))
			{
				this.ProduceCharge(1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B672 RID: 46706 RVA: 0x004294D8 File Offset: 0x004276D8
		public override bool HandleEvent(QueryChargeProductionEvent E)
		{
			if (this.ChargeRate > this.ChargeUse && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				E.Amount += this.GetEffectiveChargeRate(1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B673 RID: 46707 RVA: 0x00429538 File Offset: 0x00427738
		public override bool GetActivePartLocallyDefinedFailure()
		{
			Zone anyBasisZone = this.GetAnyBasisZone();
			if (anyBasisZone == null)
			{
				return true;
			}
			if (this.World.IsNullOrEmpty())
			{
				this.World = anyBasisZone.ResolveZoneWorld();
				this.Worlds = this.World.CachedCommaExpansion();
			}
			else if (this.World != "*")
			{
				if (this.Worlds == null)
				{
					this.Worlds = this.World.CachedCommaExpansion();
				}
				if (!this.Worlds.Contains(anyBasisZone.ResolveZoneWorld()))
				{
					return true;
				}
			}
			return this.Plane != "*" && anyBasisZone.ResolveWorldBlueprint().Plane != this.Plane;
		}

		// Token: 0x0600B674 RID: 46708 RVA: 0x004295EA File Offset: 0x004277EA
		public override string GetActivePartLocallyDefinedFailureDescription()
		{
			return "QuantumPhaseMismatch";
		}

		// Token: 0x0600B675 RID: 46709 RVA: 0x004295F1 File Offset: 0x004277F1
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600B676 RID: 46710 RVA: 0x004295F4 File Offset: 0x004277F4
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.ProduceCharge(Amount);
		}

		// Token: 0x0600B677 RID: 46711 RVA: 0x00429600 File Offset: 0x00427800
		public int GetEffectiveChargeRate(int Multiple = 1)
		{
			int num = this.ChargeRate - this.ChargeUse;
			if (Multiple != 1)
			{
				num *= Multiple;
			}
			int num2 = this.MyPowerLoadLevel();
			if (num2 != 100)
			{
				num = num * num2 / 100;
			}
			return num;
		}

		// Token: 0x0600B678 RID: 46712 RVA: 0x00429638 File Offset: 0x00427838
		public void ProduceCharge(int Turns = 1)
		{
			if (this.ChargeRate > this.ChargeUse && base.IsReady(false, false, false, false, false, false, false, false, false, false, Turns, null, false, 0L, null))
			{
				this.ParentObject.ChargeAvailable(this.GetEffectiveChargeRate(1), 0L, Turns, false, null);
			}
		}

		// Token: 0x0400499A RID: 18842
		public int ChargeRate = 10;

		// Token: 0x0400499B RID: 18843
		public string World = "JoppaWorld";

		// Token: 0x0400499C RID: 18844
		public string Plane = "*";

		// Token: 0x0400499D RID: 18845
		public List<string> Worlds;
	}
}
