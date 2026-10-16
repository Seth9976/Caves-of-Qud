using System;
using System.Runtime.CompilerServices;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x0200010C RID: 268
	internal static class Error
	{
		// Token: 0x0600061C RID: 1564 RVA: 0x0000E040 File Offset: 0x0000C240
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ThrowArgumentNullException<T>(T value, string paramName) where T : class
		{
			if (value == null)
			{
				Error.ThrowArgumentNullExceptionCore(paramName);
			}
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0000E050 File Offset: 0x0000C250
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowArgumentNullExceptionCore(string paramName)
		{
			throw new ArgumentNullException(paramName);
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0000E058 File Offset: 0x0000C258
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Exception ArgumentOutOfRange(string paramName)
		{
			return new ArgumentOutOfRangeException(paramName);
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0000E060 File Offset: 0x0000C260
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Exception NoElements()
		{
			return new InvalidOperationException("Source sequence doesn't contain any elements.");
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0000E06C File Offset: 0x0000C26C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Exception MoreThanOneElement()
		{
			return new InvalidOperationException("Source sequence contains more than one element.");
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0000E078 File Offset: 0x0000C278
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void ThrowArgumentException(string message)
		{
			throw new ArgumentException(message);
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0000E080 File Offset: 0x0000C280
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void ThrowNotYetCompleted()
		{
			throw new InvalidOperationException("Not yet completed.");
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0000E08C File Offset: 0x0000C28C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static T ThrowNotYetCompleted<T>()
		{
			throw new InvalidOperationException("Not yet completed.");
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0000E098 File Offset: 0x0000C298
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ThrowWhenContinuationIsAlreadyRegistered<T>(T continuationField) where T : class
		{
			if (continuationField != null)
			{
				Error.ThrowInvalidOperationExceptionCore("continuation is already registered.");
			}
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0000E0AC File Offset: 0x0000C2AC
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowInvalidOperationExceptionCore(string message)
		{
			throw new InvalidOperationException(message);
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0000E0B4 File Offset: 0x0000C2B4
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void ThrowOperationCanceledException()
		{
			throw new OperationCanceledException();
		}
	}
}
