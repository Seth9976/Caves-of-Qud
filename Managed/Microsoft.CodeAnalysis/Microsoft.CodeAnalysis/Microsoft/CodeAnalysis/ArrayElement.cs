using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000111 RID: 273
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{Value,nq}")]
	internal struct ArrayElement<[Nullable(2)] T>
	{
		// Token: 0x06000985 RID: 2437 RVA: 0x0001B364 File Offset: 0x00019564
		public static implicit operator T([Nullable(new byte[] { 0, 1 })] ArrayElement<T> element)
		{
			return element.Value;
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0001B36C File Offset: 0x0001956C
		[return: Nullable(new byte[] { 2, 0, 1 })]
		[return: NotNullIfNotNull("items")]
		public static ArrayElement<T>[] MakeElementArray([Nullable(new byte[] { 2, 1 })] T[] items)
		{
			if (items == null)
			{
				return null;
			}
			ArrayElement<T>[] array = new ArrayElement<T>[items.Length];
			for (int i = 0; i < items.Length; i++)
			{
				array[i].Value = items[i];
			}
			return array;
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0001B3AC File Offset: 0x000195AC
		[return: Nullable(new byte[] { 2, 1 })]
		[return: NotNullIfNotNull("items")]
		public static T[] MakeArray([Nullable(new byte[] { 2, 0, 1 })] ArrayElement<T>[] items)
		{
			if (items == null)
			{
				return null;
			}
			T[] array = new T[items.Length];
			for (int i = 0; i < items.Length; i++)
			{
				array[i] = items[i].Value;
			}
			return array;
		}

		// Token: 0x040002A5 RID: 677
		internal T Value;
	}
}
