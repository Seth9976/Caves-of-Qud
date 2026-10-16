using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Collections
{
	// Token: 0x0200049E RID: 1182
	[NullableContext(1)]
	[Nullable(0)]
	internal class KeyedStack<T, [Nullable(2)] R>
	{
		// Token: 0x06002EAB RID: 11947 RVA: 0x00083924 File Offset: 0x00081B24
		public void Push(T key, R value)
		{
			Stack<R> stack;
			if (!this._dict.TryGetValue(key, ref stack))
			{
				stack = new Stack<R>();
				this._dict.Add(key, stack);
			}
			stack.Push(value);
		}

		// Token: 0x06002EAC RID: 11948 RVA: 0x0008395C File Offset: 0x00081B5C
		public bool TryPop(T key, [MaybeNullWhen(false)] out R value)
		{
			Stack<R> stack;
			if (this._dict.TryGetValue(key, ref stack) && stack.Count > 0)
			{
				value = stack.Pop();
				return true;
			}
			value = default(R);
			return false;
		}

		// Token: 0x0400127B RID: 4731
		private readonly Dictionary<T, Stack<R>> _dict = new Dictionary<T, Stack<R>>();
	}
}
