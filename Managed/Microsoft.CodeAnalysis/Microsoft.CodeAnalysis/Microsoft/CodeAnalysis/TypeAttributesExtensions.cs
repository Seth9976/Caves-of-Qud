using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001AD RID: 429
	internal static class TypeAttributesExtensions
	{
		// Token: 0x060011EC RID: 4588 RVA: 0x00037271 File Offset: 0x00035471
		public static bool IsInterface(this TypeAttributes flags)
		{
			return (flags & 32) > 0;
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x0003727A File Offset: 0x0003547A
		public static bool IsWindowsRuntime(this TypeAttributes flags)
		{
			return (flags & 16384) > 0;
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x00037286 File Offset: 0x00035486
		public static bool IsPublic(this TypeAttributes flags)
		{
			return (flags & 1) > 0;
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x0003728E File Offset: 0x0003548E
		public static bool IsSpecialName(this TypeAttributes flags)
		{
			return (flags & 1024) > 0;
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x0003729C File Offset: 0x0003549C
		internal static CharSet ToCharSet(this TypeAttributes flags)
		{
			TypeAttributes typeAttributes = flags & 196608;
			if (typeAttributes == null)
			{
				return 2;
			}
			if (typeAttributes == 65536)
			{
				return 3;
			}
			if (typeAttributes == 131072)
			{
				return 4;
			}
			return 0;
		}
	}
}
