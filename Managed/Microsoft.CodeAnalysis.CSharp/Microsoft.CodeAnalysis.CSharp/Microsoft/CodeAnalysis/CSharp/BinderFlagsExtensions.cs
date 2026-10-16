using System;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000011 RID: 17
	internal static class BinderFlagsExtensions
	{
		// Token: 0x060004D3 RID: 1235 RVA: 0x00032F44 File Offset: 0x00031144
		public static bool Includes(this BinderFlags self, BinderFlags other)
		{
			return (self & other) == other;
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00032F4C File Offset: 0x0003114C
		public static bool IncludesAny(this BinderFlags self, BinderFlags other)
		{
			return (self & other) > BinderFlags.None;
		}
	}
}
