using System;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x020006AA RID: 1706
	internal struct LayoutNodeData
	{
		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x060031EE RID: 12782 RVA: 0x000BC902 File Offset: 0x000BAB02
		// (set) Token: 0x060031EF RID: 12783 RVA: 0x000BC90F File Offset: 0x000BAB0F
		public bool HasNewLayout
		{
			get
			{
				return (this.Status & LayoutNodeData.FlexStatus.HasNewLayout) == LayoutNodeData.FlexStatus.HasNewLayout;
			}
			set
			{
				this.Status = (value ? (this.Status | LayoutNodeData.FlexStatus.HasNewLayout) : (this.Status & ~LayoutNodeData.FlexStatus.HasNewLayout));
			}
		}

		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x060031F0 RID: 12784 RVA: 0x000BC92D File Offset: 0x000BAB2D
		// (set) Token: 0x060031F1 RID: 12785 RVA: 0x000BC93A File Offset: 0x000BAB3A
		public bool IsDirty
		{
			get
			{
				return (this.Status & LayoutNodeData.FlexStatus.IsDirty) == LayoutNodeData.FlexStatus.IsDirty;
			}
			set
			{
				this.Status = (value ? (this.Status | LayoutNodeData.FlexStatus.IsDirty) : (this.Status & ~LayoutNodeData.FlexStatus.IsDirty));
			}
		}

		// Token: 0x04001870 RID: 6256
		public FixedBuffer2<LayoutValue> ResolvedDimensions;

		// Token: 0x04001871 RID: 6257
		private float TargetSize;

		// Token: 0x04001872 RID: 6258
		public int ManagedMeasureFunctionIndex;

		// Token: 0x04001873 RID: 6259
		public int ManagedBaselineFunctionIndex;

		// Token: 0x04001874 RID: 6260
		public int ManagedOwnerIndex;

		// Token: 0x04001875 RID: 6261
		public int LineIndex;

		// Token: 0x04001876 RID: 6262
		public LayoutHandle Config;

		// Token: 0x04001877 RID: 6263
		public LayoutHandle Parent;

		// Token: 0x04001878 RID: 6264
		public LayoutHandle NextChild;

		// Token: 0x04001879 RID: 6265
		public LayoutList<LayoutHandle> Children;

		// Token: 0x0400187A RID: 6266
		private LayoutNodeData.FlexStatus Status;

		// Token: 0x020006AB RID: 1707
		[Flags]
		internal enum FlexStatus
		{
			// Token: 0x0400187C RID: 6268
			IsDirty = 1,
			// Token: 0x0400187D RID: 6269
			HasNewLayout = 4,
			// Token: 0x0400187E RID: 6270
			DependsOnParentSize = 64,
			// Token: 0x0400187F RID: 6271
			Fixed = 8,
			// Token: 0x04001880 RID: 6272
			MinViolation = 16,
			// Token: 0x04001881 RID: 6273
			MaxViolation = 32
		}
	}
}
