using System;

namespace System.Reflection
{
	// Token: 0x0200001D RID: 29
	internal static class TypeAttributesExtensions
	{
		// Token: 0x060000DD RID: 221 RVA: 0x000031F5 File Offset: 0x000013F5
		public static bool IsForwarder(this TypeAttributes flags)
		{
			return (flags & 2097152) > 0;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003201 File Offset: 0x00001401
		public static bool IsNested(this TypeAttributes flags)
		{
			return (flags & 6) > 0;
		}

		// Token: 0x04000047 RID: 71
		private const TypeAttributes Forwarder = 2097152;

		// Token: 0x04000048 RID: 72
		private const TypeAttributes NestedMask = 6;
	}
}
