using System;
using XRL.UI.Framework;
using XRL.World.Tinkering;

namespace Qud.UI
{
	// Token: 0x0200047B RID: 1147
	public class TinkeringBitsLineData : PooledFrameworkDataElement<TinkeringBitsLineData>
	{
		// Token: 0x06002BC9 RID: 11209 RVA: 0x000EA115 File Offset: 0x000E8315
		public TinkeringBitsLineData set(char cBit, string bit, int amount, BitCost activeCost)
		{
			this.cBit = cBit;
			this.bit = bit;
			this.amount = amount;
			this.activeCost = activeCost;
			return this;
		}

		// Token: 0x06002BCA RID: 11210 RVA: 0x000EA135 File Offset: 0x000E8335
		public override void free()
		{
			this.activeCost = null;
			this.bit = null;
			this.amount = 0;
			base.free();
		}

		// Token: 0x040017E5 RID: 6117
		public char cBit;

		// Token: 0x040017E6 RID: 6118
		public string bit;

		// Token: 0x040017E7 RID: 6119
		public int amount;

		// Token: 0x040017E8 RID: 6120
		public BitCost activeCost;
	}
}
