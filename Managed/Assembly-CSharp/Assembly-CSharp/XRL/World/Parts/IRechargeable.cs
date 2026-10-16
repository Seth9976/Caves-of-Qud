using System;

namespace XRL.World.Parts
{
	// Token: 0x020011CF RID: 4559
	[Serializable]
	public abstract class IRechargeable : IPoweredPart
	{
		// Token: 0x0600B58F RID: 46479 RVA: 0x004244B4 File Offset: 0x004226B4
		public IRechargeable()
		{
			this.ChargeUse = 0;
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B590 RID: 46480
		public abstract void AddCharge(int Amount);

		// Token: 0x0600B591 RID: 46481
		public abstract bool CanBeRecharged();

		// Token: 0x0600B592 RID: 46482
		public abstract int GetRechargeAmount();

		// Token: 0x0600B593 RID: 46483 RVA: 0x004244CA File Offset: 0x004226CA
		public virtual int GetRechargeValue()
		{
			return 3000;
		}

		// Token: 0x0600B594 RID: 46484 RVA: 0x004244D1 File Offset: 0x004226D1
		public virtual char GetRechargeBit()
		{
			return 'R';
		}
	}
}
