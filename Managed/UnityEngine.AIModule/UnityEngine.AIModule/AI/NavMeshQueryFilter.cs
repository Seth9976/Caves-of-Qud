using System;

namespace UnityEngine.AI
{
	// Token: 0x02000016 RID: 22
	public struct NavMeshQueryFilter
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00003E73 File Offset: 0x00002073
		// (set) Token: 0x06000182 RID: 386 RVA: 0x00003E7B File Offset: 0x0000207B
		internal float[] costs { readonly get; private set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00003E84 File Offset: 0x00002084
		// (set) Token: 0x06000184 RID: 388 RVA: 0x00003E8C File Offset: 0x0000208C
		public int areaMask { readonly get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00003E95 File Offset: 0x00002095
		// (set) Token: 0x06000186 RID: 390 RVA: 0x00003E9D File Offset: 0x0000209D
		public int agentTypeID { readonly get; set; }

		// Token: 0x06000187 RID: 391 RVA: 0x00003EA8 File Offset: 0x000020A8
		public float GetAreaCost(int areaIndex)
		{
			bool flag = this.costs == null;
			float num;
			if (flag)
			{
				bool flag2 = areaIndex < 0 || areaIndex >= 32;
				if (flag2)
				{
					string text = string.Format("The valid range is [0:{0}]", 31);
					throw new IndexOutOfRangeException(text);
				}
				num = 1f;
			}
			else
			{
				num = this.costs[areaIndex];
			}
			return num;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00003F08 File Offset: 0x00002108
		public void SetAreaCost(int areaIndex, float cost)
		{
			bool flag = this.costs == null;
			if (flag)
			{
				this.costs = new float[32];
				for (int i = 0; i < 32; i++)
				{
					this.costs[i] = 1f;
				}
			}
			this.costs[areaIndex] = cost;
		}

		// Token: 0x04000039 RID: 57
		private const int k_AreaCostElementCount = 32;
	}
}
