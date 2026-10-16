using System;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x0200068A RID: 1674
	internal readonly struct LayoutConfig
	{
		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x060030B7 RID: 12471 RVA: 0x000B9458 File Offset: 0x000B7658
		public static LayoutConfig Undefined
		{
			get
			{
				return new LayoutConfig(default(LayoutDataAccess), LayoutHandle.Undefined);
			}
		}

		// Token: 0x060030B8 RID: 12472 RVA: 0x000B9478 File Offset: 0x000B7678
		internal LayoutConfig(LayoutDataAccess access, LayoutHandle handle)
		{
			this.m_Access = access;
			this.m_Handle = handle;
		}

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x060030B9 RID: 12473 RVA: 0x000B9489 File Offset: 0x000B7689
		public bool IsUndefined
		{
			get
			{
				return this.m_Handle.Equals(LayoutHandle.Undefined);
			}
		}

		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x060030BA RID: 12474 RVA: 0x000B949B File Offset: 0x000B769B
		public LayoutHandle Handle
		{
			get
			{
				return this.m_Handle;
			}
		}

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x060030BB RID: 12475 RVA: 0x000B94A3 File Offset: 0x000B76A3
		public ref float PointScaleFactor
		{
			get
			{
				return ref this.m_Access.GetConfigData(this.m_Handle).PointScaleFactor;
			}
		}

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x060030BC RID: 12476 RVA: 0x000B94BB File Offset: 0x000B76BB
		public ref bool ShouldLog
		{
			get
			{
				return ref this.m_Access.GetConfigData(this.m_Handle).ShouldLog;
			}
		}

		// Token: 0x0400180E RID: 6158
		private readonly LayoutDataAccess m_Access;

		// Token: 0x0400180F RID: 6159
		private readonly LayoutHandle m_Handle;
	}
}
