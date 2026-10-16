using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000119 RID: 281
	[NullableContext(1)]
	[Nullable(0)]
	internal static class ConsListExtensions
	{
		// Token: 0x060009C8 RID: 2504 RVA: 0x0001BFC2 File Offset: 0x0001A1C2
		public static ConsList<T> Prepend<[Nullable(2)] T>([Nullable(new byte[] { 2, 1 })] this ConsList<T> list, T head)
		{
			return new ConsList<T>(head, list ?? ConsList<T>.Empty);
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0001BFD4 File Offset: 0x0001A1D4
		public static bool ContainsReference<[Nullable(2)] T>(this ConsList<T> list, T element)
		{
			while (list != ConsList<T>.Empty)
			{
				if (list.Head == element)
				{
					return true;
				}
				list = list.Tail;
			}
			return false;
		}
	}
}
