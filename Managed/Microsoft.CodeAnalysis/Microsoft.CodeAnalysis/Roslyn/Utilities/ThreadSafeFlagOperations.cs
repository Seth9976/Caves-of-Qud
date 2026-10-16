using System;
using System.Threading;

namespace Roslyn.Utilities
{
	// Token: 0x02000068 RID: 104
	internal static class ThreadSafeFlagOperations
	{
		// Token: 0x06000275 RID: 629 RVA: 0x00008CC4 File Offset: 0x00006EC4
		public static bool Set(ref int flags, int toSet)
		{
			for (;;)
			{
				int num = flags;
				int num2 = num | toSet;
				if (num2 == num)
				{
					break;
				}
				if (Interlocked.CompareExchange(ref flags, num2, num) == num)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00008CEC File Offset: 0x00006EEC
		public static bool Clear(ref int flags, int toClear)
		{
			for (;;)
			{
				int num = flags;
				int num2 = num & ~toClear;
				if (num2 == num)
				{
					break;
				}
				if (Interlocked.CompareExchange(ref flags, num2, num) == num)
				{
					return true;
				}
			}
			return false;
		}
	}
}
