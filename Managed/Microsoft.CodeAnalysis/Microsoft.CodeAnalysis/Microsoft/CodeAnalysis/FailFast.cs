using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000192 RID: 402
	[NullableContext(1)]
	[Nullable(0)]
	internal static class FailFast
	{
		// Token: 0x0600104F RID: 4175 RVA: 0x0002FD08 File Offset: 0x0002DF08
		[DebuggerHidden]
		[DoesNotReturn]
		internal static void OnFatalException(Exception exception)
		{
			if (Debugger.IsAttached)
			{
				Debugger.Break();
			}
			AggregateException ex = exception as AggregateException;
			if (ex != null && ex.InnerExceptions.Count == 1)
			{
				exception = ex.InnerExceptions[0];
			}
			Environment.FailFast(exception.ToString(), exception);
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x0002FD58 File Offset: 0x0002DF58
		[DebuggerHidden]
		[DoesNotReturn]
		internal static void Fail(string message)
		{
			Environment.FailFast(message);
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x0002FD68 File Offset: 0x0002DF68
		[NullableContext(2)]
		[Conditional("DEBUG")]
		internal static void DumpStackTrace(Exception exception = null, string message = null)
		{
			Console.WriteLine("Dumping info before call to failfast");
			if (message != null)
			{
				Console.WriteLine(message);
			}
			if (exception != null)
			{
				Console.WriteLine("Exception info");
				for (Exception ex = exception; ex != null; ex = ex.InnerException)
				{
					Console.WriteLine(ex.Message);
					Console.WriteLine(ex.StackTrace);
				}
			}
			Console.WriteLine("Stack trace of handler");
			Console.WriteLine(new StackTrace().ToString());
			Console.Out.Flush();
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x0002FDDC File Offset: 0x0002DFDC
		[NullableContext(2)]
		[Conditional("DEBUG")]
		[DebuggerHidden]
		internal static void Assert([DoesNotReturnIf(false)] bool condition, string message = null)
		{
			if (condition)
			{
				return;
			}
			if (Debugger.IsAttached)
			{
				Debugger.Break();
			}
			Environment.FailFast("ASSERT FAILED" + Environment.NewLine + message);
		}
	}
}
