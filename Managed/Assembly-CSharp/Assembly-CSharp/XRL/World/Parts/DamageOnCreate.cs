using System;
using UnityEngine;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02000E98 RID: 3736
	[Serializable]
	public class DamageOnCreate : IPart
	{
		// Token: 0x0600964D RID: 38477 RVA: 0x003678B4 File Offset: 0x00365AB4
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AfterObjectCreatedEvent.ID;
		}

		// Token: 0x0600964E RID: 38478 RVA: 0x003678CC File Offset: 0x00365ACC
		public override bool HandleEvent(AfterObjectCreatedEvent E)
		{
			if (this.Min != -1 && this.Max != -1)
			{
				int num = Stat.Random(this.Min, this.Max);
				this.ParentObject.hitpoints = num;
			}
			else if (this.MinPercent != -1 && this.MaxPercent != -1)
			{
				float num2 = (float)Stat.Random(this.MinPercent, this.MaxPercent) / 100f;
				int baseValue = this.ParentObject.GetStat("Hitpoints").BaseValue;
				this.ParentObject.hitpoints = Math.Max(Mathf.RoundToInt((float)baseValue * num2), 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04003E3F RID: 15935
		public int Min = -1;

		// Token: 0x04003E40 RID: 15936
		public int Max = -1;

		// Token: 0x04003E41 RID: 15937
		public int MinPercent = -1;

		// Token: 0x04003E42 RID: 15938
		public int MaxPercent = -1;
	}
}
