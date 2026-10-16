using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200008A RID: 138
	internal static class EnumerableExtensions
	{
		// Token: 0x06000327 RID: 807 RVA: 0x0000A9C5 File Offset: 0x00008BC5
		public static IEnumerable<T> NonNull<T>(this IEnumerable<T> enumerable)
		{
			return enumerable.Where<T>((T obj) => obj != null);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000A9EC File Offset: 0x00008BEC
		public static IEnumerable<T> IgnoreExceptions<T, TException>(this IEnumerable<T> enumerable, Action<TException> onException = null) where TException : Exception
		{
			EnumerableExtensions.<IgnoreExceptions>d__1<T, TException> <IgnoreExceptions>d__ = new EnumerableExtensions.<IgnoreExceptions>d__1<T, TException>(-2);
			<IgnoreExceptions>d__.<>3__enumerable = enumerable;
			<IgnoreExceptions>d__.<>3__onException = onException;
			return <IgnoreExceptions>d__;
		}
	}
}
