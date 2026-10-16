using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Versioning
{
	// Token: 0x02000021 RID: 33
	[AttributeUsage(1023, AllowMultiple = true, Inherited = false)]
	internal sealed class SupportedOSPlatformAttribute : OSPlatformAttribute
	{
		// Token: 0x0600006D RID: 109 RVA: 0x00002EEE File Offset: 0x000010EE
		[NullableContext(1)]
		public SupportedOSPlatformAttribute(string platformName)
			: base(platformName)
		{
		}
	}
}
