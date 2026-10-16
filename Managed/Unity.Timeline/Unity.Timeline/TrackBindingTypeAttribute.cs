using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000044 RID: 68
	[AttributeUsage(AttributeTargets.Class)]
	public class TrackBindingTypeAttribute : Attribute
	{
		// Token: 0x060002CE RID: 718 RVA: 0x00009E23 File Offset: 0x00008023
		public TrackBindingTypeAttribute(Type type)
		{
			this.type = type;
			this.flags = TrackBindingFlags.AllowCreateComponent;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00009E39 File Offset: 0x00008039
		public TrackBindingTypeAttribute(Type type, TrackBindingFlags flags)
		{
			this.type = type;
			this.flags = flags;
		}

		// Token: 0x040000F6 RID: 246
		public readonly Type type;

		// Token: 0x040000F7 RID: 247
		public readonly TrackBindingFlags flags;
	}
}
