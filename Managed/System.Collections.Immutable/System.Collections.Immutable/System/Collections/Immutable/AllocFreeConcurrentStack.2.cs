using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000017 RID: 23
	internal static class AllocFreeConcurrentStack
	{
		// Token: 0x0400000F RID: 15
		[Nullable(new byte[] { 2, 1, 1 })]
		[ThreadStatic]
		internal static Dictionary<Type, object> t_stacks;
	}
}
