using System;
using System.Text;

namespace XRL.World.Parts
{
	// Token: 0x0200100C RID: 4108
	[Serializable]
	public class CriticalThreshold : IPart
	{
		// Token: 0x0600A3A8 RID: 41896 RVA: 0x003BCE74 File Offset: 0x003BB074
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetCriticalThresholdEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600A3A9 RID: 41897 RVA: 0x003BCE92 File Offset: 0x003BB092
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(new Action<StringBuilder>(this.AppendEffect));
			return base.HandleEvent(E);
		}

		// Token: 0x0600A3AA RID: 41898 RVA: 0x003BCEB3 File Offset: 0x003BB0B3
		public void AppendEffect(StringBuilder SB)
		{
			SB.AppendSigned(this.Value * 5).Append("% chance to score critical hits");
		}

		// Token: 0x0600A3AB RID: 41899 RVA: 0x003BCECE File Offset: 0x003BB0CE
		public override bool HandleEvent(GetCriticalThresholdEvent E)
		{
			if (E.Weapon == this.ParentObject || E.Projectile == this.ParentObject)
			{
				E.Threshold -= this.Value;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x040043C9 RID: 17353
		public int Value;
	}
}
