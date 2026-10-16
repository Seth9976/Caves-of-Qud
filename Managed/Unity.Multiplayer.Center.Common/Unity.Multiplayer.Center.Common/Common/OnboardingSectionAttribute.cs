using System;

namespace Unity.Multiplayer.Center.Common
{
	// Token: 0x02000006 RID: 6
	[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
	public sealed class OnboardingSectionAttribute : Attribute
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020F6 File Offset: 0x000002F6
		public OnboardingSectionCategory Category { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002106 File Offset: 0x00000306
		public DisplayCondition DisplayCondition { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000210F File Offset: 0x0000030F
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002117 File Offset: 0x00000317
		public SelectedSolutionsData.HostingModel HostingModelDependency { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002120 File Offset: 0x00000320
		// (set) Token: 0x0600000C RID: 12 RVA: 0x00002128 File Offset: 0x00000328
		public SelectedSolutionsData.NetcodeSolution NetcodeDependency { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002131 File Offset: 0x00000331
		// (set) Token: 0x0600000E RID: 14 RVA: 0x00002139 File Offset: 0x00000339
		public int Priority { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002142 File Offset: 0x00000342
		// (set) Token: 0x06000010 RID: 16 RVA: 0x0000214A File Offset: 0x0000034A
		public int Order { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002153 File Offset: 0x00000353
		// (set) Token: 0x06000012 RID: 18 RVA: 0x0000215B File Offset: 0x0000035B
		public string TargetPackageId { get; set; }

		// Token: 0x06000013 RID: 19 RVA: 0x00002164 File Offset: 0x00000364
		public OnboardingSectionAttribute(OnboardingSectionCategory category, string id)
		{
			this.Category = category;
			this.Id = id;
		}

		// Token: 0x0400000F RID: 15
		public readonly string Id;
	}
}
