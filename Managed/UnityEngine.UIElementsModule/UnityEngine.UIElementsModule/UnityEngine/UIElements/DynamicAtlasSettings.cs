using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002BC RID: 700
	[Serializable]
	public class DynamicAtlasSettings
	{
		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06001406 RID: 5126 RVA: 0x000505E1 File Offset: 0x0004E7E1
		// (set) Token: 0x06001407 RID: 5127 RVA: 0x000505E9 File Offset: 0x0004E7E9
		public int minAtlasSize
		{
			get
			{
				return this.m_MinAtlasSize;
			}
			set
			{
				this.m_MinAtlasSize = value;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06001408 RID: 5128 RVA: 0x000505F2 File Offset: 0x0004E7F2
		// (set) Token: 0x06001409 RID: 5129 RVA: 0x000505FA File Offset: 0x0004E7FA
		public int maxAtlasSize
		{
			get
			{
				return this.m_MaxAtlasSize;
			}
			set
			{
				this.m_MaxAtlasSize = value;
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x0600140A RID: 5130 RVA: 0x00050603 File Offset: 0x0004E803
		// (set) Token: 0x0600140B RID: 5131 RVA: 0x0005060B File Offset: 0x0004E80B
		public int maxSubTextureSize
		{
			get
			{
				return this.m_MaxSubTextureSize;
			}
			set
			{
				this.m_MaxSubTextureSize = value;
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x0600140C RID: 5132 RVA: 0x00050614 File Offset: 0x0004E814
		// (set) Token: 0x0600140D RID: 5133 RVA: 0x0005061C File Offset: 0x0004E81C
		public DynamicAtlasFilters activeFilters
		{
			get
			{
				return this.m_ActiveFilters;
			}
			set
			{
				this.m_ActiveFilters = value;
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x0600140E RID: 5134 RVA: 0x00050625 File Offset: 0x0004E825
		public static DynamicAtlasFilters defaultFilters
		{
			get
			{
				return DynamicAtlas.defaultFilters;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x0600140F RID: 5135 RVA: 0x0005062C File Offset: 0x0004E82C
		// (set) Token: 0x06001410 RID: 5136 RVA: 0x00050634 File Offset: 0x0004E834
		public DynamicAtlasCustomFilter customFilter
		{
			get
			{
				return this.m_CustomFilter;
			}
			set
			{
				this.m_CustomFilter = value;
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06001411 RID: 5137 RVA: 0x0005063D File Offset: 0x0004E83D
		public static DynamicAtlasSettings defaults
		{
			get
			{
				return new DynamicAtlasSettings
				{
					minAtlasSize = 64,
					maxAtlasSize = 4096,
					maxSubTextureSize = 64,
					activeFilters = DynamicAtlasSettings.defaultFilters,
					customFilter = null
				};
			}
		}

		// Token: 0x04000AD5 RID: 2773
		[SerializeField]
		[HideInInspector]
		private int m_MinAtlasSize;

		// Token: 0x04000AD6 RID: 2774
		[SerializeField]
		[HideInInspector]
		private int m_MaxAtlasSize;

		// Token: 0x04000AD7 RID: 2775
		[SerializeField]
		[HideInInspector]
		private int m_MaxSubTextureSize;

		// Token: 0x04000AD8 RID: 2776
		[SerializeField]
		[HideInInspector]
		private DynamicAtlasFilters m_ActiveFilters;

		// Token: 0x04000AD9 RID: 2777
		private DynamicAtlasCustomFilter m_CustomFilter;
	}
}
