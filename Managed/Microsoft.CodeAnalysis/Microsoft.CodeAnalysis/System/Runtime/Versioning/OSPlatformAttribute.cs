using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Versioning
{
	// Token: 0x0200001F RID: 31
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class OSPlatformAttribute : Attribute
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00002ECE File Offset: 0x000010CE
		private protected OSPlatformAttribute(string platformName)
		{
			this.PlatformName = platformName;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002EDD File Offset: 0x000010DD
		public string PlatformName { get; }
	}
}
