using System;
using System.Collections.Generic;
using XRL.Rules;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AC8 RID: 2760
	public class CentralitySorter : Comparer<GameObject>
	{
		// Token: 0x06006F98 RID: 28568 RVA: 0x0027257D File Offset: 0x0027077D
		public CentralitySorter(int _CenterX, int _CenterY)
		{
			this.CenterX = _CenterX;
			this.CenterY = _CenterY;
		}

		// Token: 0x06006F99 RID: 28569 RVA: 0x002725A3 File Offset: 0x002707A3
		public CentralitySorter(Zone Z)
			: this(Z.Width / 2, Z.Height / 2)
		{
		}

		// Token: 0x06006F9A RID: 28570 RVA: 0x002725BC File Offset: 0x002707BC
		public override int Compare(GameObject a, GameObject b)
		{
			int num = XRL.Rules.Geometry.Distance(this.CenterX, this.CenterY, a);
			int num2 = XRL.Rules.Geometry.Distance(this.CenterX, this.CenterY, b);
			if (num > num2)
			{
				return 1;
			}
			if (num2 > num)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x04003338 RID: 13112
		public int CenterX = 40;

		// Token: 0x04003339 RID: 13113
		public int CenterY = 12;
	}
}
