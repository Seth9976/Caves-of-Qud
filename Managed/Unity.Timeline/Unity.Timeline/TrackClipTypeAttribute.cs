using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000041 RID: 65
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class TrackClipTypeAttribute : Attribute
	{
		// Token: 0x060002CB RID: 715 RVA: 0x00009DF3 File Offset: 0x00007FF3
		public TrackClipTypeAttribute(Type clipClass)
		{
			this.inspectedType = clipClass;
			this.allowAutoCreate = true;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00009E09 File Offset: 0x00008009
		public TrackClipTypeAttribute(Type clipClass, bool allowAutoCreate)
		{
			this.inspectedType = clipClass;
		}

		// Token: 0x040000F0 RID: 240
		public readonly Type inspectedType;

		// Token: 0x040000F1 RID: 241
		public readonly bool allowAutoCreate;
	}
}
