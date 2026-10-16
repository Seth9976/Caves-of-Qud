using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Versioning
{
	// Token: 0x02000020 RID: 32
	[AttributeUsage(1, AllowMultiple = false, Inherited = false)]
	internal sealed class TargetPlatformAttribute : OSPlatformAttribute
	{
		// Token: 0x0600006C RID: 108 RVA: 0x00002EE5 File Offset: 0x000010E5
		[NullableContext(1)]
		public TargetPlatformAttribute(string platformName)
			: base(platformName)
		{
		}
	}
}
