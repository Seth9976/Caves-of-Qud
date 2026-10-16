using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000016 RID: 22
	[NullableContext(1)]
	[Nullable(0)]
	internal static class AllocFreeConcurrentStack<[Nullable(2)] T>
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00002C70 File Offset: 0x00000E70
		public static void TryAdd(T item)
		{
			Stack<RefAsValueType<T>> threadLocalStack = AllocFreeConcurrentStack<T>.ThreadLocalStack;
			if (threadLocalStack.Count < 35)
			{
				threadLocalStack.Push(new RefAsValueType<T>(item));
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002C9C File Offset: 0x00000E9C
		public static bool TryTake([MaybeNullWhen(false)] out T item)
		{
			Stack<RefAsValueType<T>> threadLocalStack = AllocFreeConcurrentStack<T>.ThreadLocalStack;
			if (threadLocalStack != null && threadLocalStack.Count > 0)
			{
				item = threadLocalStack.Pop().Value;
				return true;
			}
			item = default(T);
			return false;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002CD8 File Offset: 0x00000ED8
		[Nullable(new byte[] { 1, 0, 1 })]
		private static Stack<RefAsValueType<T>> ThreadLocalStack
		{
			get
			{
				Dictionary<Type, object> dictionary = AllocFreeConcurrentStack.t_stacks;
				if (dictionary == null)
				{
					dictionary = (AllocFreeConcurrentStack.t_stacks = new Dictionary<Type, object>());
				}
				object obj;
				if (!dictionary.TryGetValue(AllocFreeConcurrentStack<T>.s_typeOfT, ref obj))
				{
					obj = new Stack<RefAsValueType<T>>(35);
					dictionary.Add(AllocFreeConcurrentStack<T>.s_typeOfT, obj);
				}
				return (Stack<RefAsValueType<T>>)obj;
			}
		}

		// Token: 0x0400000D RID: 13
		private const int MaxSize = 35;

		// Token: 0x0400000E RID: 14
		private static readonly Type s_typeOfT = typeof(T);
	}
}
