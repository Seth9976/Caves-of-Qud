using System;

namespace XRL
{
	// Token: 0x020004C6 RID: 1222
	[AttributeUsage(AttributeTargets.Class)]
	public class HasOptionFlagUpdate : Attribute
	{
		// Token: 0x04001AC4 RID: 6852
		public string Prefix;

		/// <summary>Treat all public static fields on this class as flag fields.</summary>
		// Token: 0x04001AC5 RID: 6853
		public bool FieldFlags;
	}
}
