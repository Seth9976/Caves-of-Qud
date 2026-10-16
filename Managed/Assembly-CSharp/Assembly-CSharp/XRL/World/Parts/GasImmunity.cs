using System;

namespace XRL.World.Parts
{
	// Token: 0x02000EBA RID: 3770
	[Serializable]
	public class GasImmunity : IPart
	{
		// Token: 0x06009732 RID: 38706 RVA: 0x0036DD80 File Offset: 0x0036BF80
		public override bool SameAs(IPart p)
		{
			GasImmunity gasImmunity = p as GasImmunity;
			return gasImmunity != null && gasImmunity.GasType == this.GasType;
		}

		// Token: 0x06009733 RID: 38707 RVA: 0x0036DDAA File Offset: 0x0036BFAA
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CheckGasCanAffectEvent>.ID;
		}

		// Token: 0x06009734 RID: 38708 RVA: 0x0036DDC0 File Offset: 0x0036BFC0
		public override bool HandleEvent(CheckGasCanAffectEvent E)
		{
			return !(E.Gas.GasType == this.GasType) && base.HandleEvent(E);
		}

		// Token: 0x06009735 RID: 38709 RVA: 0x0036DDE3 File Offset: 0x0036BFE3
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x04003EE4 RID: 16100
		public string GasType;
	}
}
