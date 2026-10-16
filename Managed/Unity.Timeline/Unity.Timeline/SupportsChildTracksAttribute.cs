using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000045 RID: 69
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	internal class SupportsChildTracksAttribute : Attribute
	{
		// Token: 0x060002D0 RID: 720 RVA: 0x00009E4F File Offset: 0x0000804F
		public SupportsChildTracksAttribute(Type childType = null, int levels = 2147483647)
		{
			this.childType = childType;
			this.levels = levels;
		}

		// Token: 0x040000F8 RID: 248
		public readonly Type childType;

		// Token: 0x040000F9 RID: 249
		public readonly int levels;
	}
}
