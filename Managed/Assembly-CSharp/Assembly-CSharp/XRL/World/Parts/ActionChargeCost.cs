using System;
using UnityEngine;

namespace XRL.World.Parts
{
	// Token: 0x020012AB RID: 4779
	[Serializable]
	public class ActionChargeCost : IPart
	{
		// Token: 0x0600BCC3 RID: 48323 RVA: 0x00451920 File Offset: 0x0044FB20
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<UseEnergyEvent>.ID;
		}

		// Token: 0x0600BCC4 RID: 48324 RVA: 0x00451938 File Offset: 0x0044FB38
		public override bool HandleEvent(UseEnergyEvent E)
		{
			if (!E.Passive)
			{
				int num = Mathf.RoundToInt((float)E.Amount * ((float)this.Percentage / 100f));
				this.ParentObject.UseCharge(num, false, 0L, true, true, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04004BAF RID: 19375
		public int Percentage = 100;
	}
}
