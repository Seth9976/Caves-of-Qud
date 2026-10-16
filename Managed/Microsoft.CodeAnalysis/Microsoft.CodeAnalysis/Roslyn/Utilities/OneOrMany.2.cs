using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Roslyn.Utilities
{
	// Token: 0x02000055 RID: 85
	[NullableContext(1)]
	[Nullable(0)]
	internal static class OneOrMany
	{
		// Token: 0x060001CE RID: 462 RVA: 0x00006B83 File Offset: 0x00004D83
		[return: Nullable(new byte[] { 0, 1 })]
		public static OneOrMany<T> Create<T>(T one)
		{
			return new OneOrMany<T>(one);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00006B8B File Offset: 0x00004D8B
		[return: Nullable(new byte[] { 0, 1 })]
		public static OneOrMany<T> Create<T>([Nullable(new byte[] { 0, 1 })] ImmutableArray<T> many)
		{
			return new OneOrMany<T>(many);
		}
	}
}
