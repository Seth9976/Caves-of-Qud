using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x0200002F RID: 47
	[NullableContext(1)]
	[Nullable(0)]
	internal class ImmutableEnumerableDebuggerProxy<[Nullable(2)] T>
	{
		// Token: 0x060001E3 RID: 483 RVA: 0x000061F3 File Offset: 0x000043F3
		public ImmutableEnumerableDebuggerProxy(IEnumerable<T> enumerable)
		{
			Requires.NotNull<IEnumerable<T>>(enumerable, "enumerable");
			this._enumerable = enumerable;
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00006210 File Offset: 0x00004410
		[DebuggerBrowsable(3)]
		public T[] Contents
		{
			get
			{
				T[] array;
				if ((array = this._cachedContents) == null)
				{
					array = (this._cachedContents = Enumerable.ToArray<T>(this._enumerable));
				}
				return array;
			}
		}

		// Token: 0x04000024 RID: 36
		private readonly IEnumerable<T> _enumerable;

		// Token: 0x04000025 RID: 37
		private T[] _cachedContents;
	}
}
