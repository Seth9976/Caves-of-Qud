using System;

namespace XRL
{
	// Token: 0x020004C7 RID: 1223
	[AttributeUsage(AttributeTargets.Class)]
	public class RequiresMod : Attribute
	{
		// Token: 0x04001AC6 RID: 6854
		public string ID;

		// Token: 0x04001AC7 RID: 6855
		public ulong WorkshopID;
	}
}
