using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Versioning
{
	// Token: 0x02000022 RID: 34
	[AttributeUsage(1023, AllowMultiple = true, Inherited = false)]
	internal sealed class UnsupportedOSPlatformAttribute : OSPlatformAttribute
	{
		// Token: 0x0600006E RID: 110 RVA: 0x00002EF7 File Offset: 0x000010F7
		[NullableContext(1)]
		public UnsupportedOSPlatformAttribute(string platformName)
			: base(platformName)
		{
		}
	}
}
