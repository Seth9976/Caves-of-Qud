using System;

namespace UnityEngine.Timeline
{
	// Token: 0x0200004A RID: 74
	[AttributeUsage(AttributeTargets.Class)]
	public class CustomStyleAttribute : Attribute
	{
		// Token: 0x060002D6 RID: 726 RVA: 0x00009E94 File Offset: 0x00008094
		public CustomStyleAttribute(string ussStyle)
		{
			this.ussStyle = ussStyle;
		}

		// Token: 0x040000FB RID: 251
		public readonly string ussStyle;
	}
}
