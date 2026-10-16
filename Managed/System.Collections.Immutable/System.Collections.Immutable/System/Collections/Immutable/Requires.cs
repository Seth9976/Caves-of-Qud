using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000049 RID: 73
	[NullableContext(2)]
	[Nullable(0)]
	internal static class Requires
	{
		// Token: 0x0600039A RID: 922 RVA: 0x000099CF File Offset: 0x00007BCF
		[NullableContext(1)]
		[DebuggerStepThrough]
		public static void NotNull<T>([ValidatedNotNull] T value, [Nullable(2)] string parameterName) where T : class
		{
			if (value == null)
			{
				Requires.FailArgumentNullException(parameterName);
			}
		}

		// Token: 0x0600039B RID: 923 RVA: 0x000099DF File Offset: 0x00007BDF
		[NullableContext(1)]
		[DebuggerStepThrough]
		public static T NotNullPassthrough<T>([ValidatedNotNull] T value, [Nullable(2)] string parameterName) where T : class
		{
			Requires.NotNull<T>(value, parameterName);
			return value;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x000099E9 File Offset: 0x00007BE9
		[DebuggerStepThrough]
		public static void NotNullAllowStructs<T>([Nullable(1)] [ValidatedNotNull] T value, string parameterName)
		{
			if (value == null)
			{
				Requires.FailArgumentNullException(parameterName);
			}
		}

		// Token: 0x0600039D RID: 925 RVA: 0x000099F9 File Offset: 0x00007BF9
		[DebuggerStepThrough]
		private static void FailArgumentNullException(string parameterName)
		{
			throw new ArgumentNullException(parameterName);
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00009A01 File Offset: 0x00007C01
		[DebuggerStepThrough]
		public static void Range(bool condition, string parameterName, string message = null)
		{
			if (!condition)
			{
				Requires.FailRange(parameterName, message);
			}
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00009A0D File Offset: 0x00007C0D
		[DebuggerStepThrough]
		public static void FailRange(string parameterName, string message = null)
		{
			if (string.IsNullOrEmpty(message))
			{
				throw new ArgumentOutOfRangeException(parameterName);
			}
			throw new ArgumentOutOfRangeException(parameterName, message);
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00009A25 File Offset: 0x00007C25
		[DebuggerStepThrough]
		public static void Argument(bool condition, string parameterName, string message)
		{
			if (!condition)
			{
				throw new ArgumentException(message, parameterName);
			}
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00009A32 File Offset: 0x00007C32
		[DebuggerStepThrough]
		public static void Argument(bool condition)
		{
			if (!condition)
			{
				throw new ArgumentException();
			}
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00009A3D File Offset: 0x00007C3D
		[NullableContext(1)]
		[DebuggerStepThrough]
		[MethodImpl(8)]
		public static void FailObjectDisposed<[Nullable(2)] TDisposed>(TDisposed disposed)
		{
			throw new ObjectDisposedException(disposed.GetType().FullName);
		}
	}
}
