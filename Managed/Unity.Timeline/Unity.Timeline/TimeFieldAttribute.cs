using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000047 RID: 71
	internal class TimeFieldAttribute : PropertyAttribute
	{
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x00009E6D File Offset: 0x0000806D
		public TimeFieldAttribute.UseEditMode useEditMode { get; }

		// Token: 0x060002D3 RID: 723 RVA: 0x00009E75 File Offset: 0x00008075
		public TimeFieldAttribute(TimeFieldAttribute.UseEditMode useEditMode = TimeFieldAttribute.UseEditMode.ApplyEditMode)
		{
			this.useEditMode = useEditMode;
		}

		// Token: 0x0200007E RID: 126
		public enum UseEditMode
		{
			// Token: 0x04000192 RID: 402
			None,
			// Token: 0x04000193 RID: 403
			ApplyEditMode
		}
	}
}
