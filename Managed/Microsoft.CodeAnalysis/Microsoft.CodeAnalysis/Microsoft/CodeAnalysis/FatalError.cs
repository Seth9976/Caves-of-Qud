using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000193 RID: 403
	[NullableContext(1)]
	[Nullable(0)]
	internal static class FatalError
	{
		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06001053 RID: 4179 RVA: 0x0002FE03 File Offset: 0x0002E003
		// (set) Token: 0x06001054 RID: 4180 RVA: 0x0002FE0A File Offset: 0x0002E00A
		[Nullable(new byte[] { 2, 1 })]
		public static Action<Exception> Handler
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get
			{
				return FatalError.s_fatalHandler;
			}
			[param: Nullable(new byte[] { 2, 1 })]
			[param: DisallowNull]
			set
			{
				if (FatalError.s_fatalHandler != value)
				{
					FatalError.s_fatalHandler = value;
				}
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06001055 RID: 4181 RVA: 0x0002FE1F File Offset: 0x0002E01F
		// (set) Token: 0x06001056 RID: 4182 RVA: 0x0002FE26 File Offset: 0x0002E026
		[Nullable(new byte[] { 2, 1 })]
		public static Action<Exception> NonFatalHandler
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get
			{
				return FatalError.s_nonFatalHandler;
			}
			[param: Nullable(new byte[] { 2, 1 })]
			[param: DisallowNull]
			set
			{
				if (FatalError.s_nonFatalHandler != value)
				{
					FatalError.s_nonFatalHandler = value;
				}
			}
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x0002FE3B File Offset: 0x0002E03B
		public static void OverwriteHandler([Nullable(new byte[] { 2, 1 })] Action<Exception> value)
		{
			FatalError.s_fatalHandler = value;
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x0002FE43 File Offset: 0x0002E043
		private static bool IsCurrentOperationBeingCancelled(Exception exception, CancellationToken cancellationToken)
		{
			return exception is OperationCanceledException && cancellationToken.IsCancellationRequested;
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x0002FE56 File Offset: 0x0002E056
		[DebuggerHidden]
		public static bool ReportAndPropagateUnlessCanceled(Exception exception)
		{
			return !(exception is OperationCanceledException) && FatalError.ReportAndPropagate(exception);
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x0002FE68 File Offset: 0x0002E068
		[DebuggerHidden]
		public static bool ReportAndPropagateUnlessCanceled(Exception exception, CancellationToken contextCancellationToken)
		{
			return !FatalError.IsCurrentOperationBeingCancelled(exception, contextCancellationToken) && FatalError.ReportAndPropagate(exception);
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x0002FE7B File Offset: 0x0002E07B
		[DebuggerHidden]
		public static bool ReportAndCatchUnlessCanceled(Exception exception)
		{
			return !(exception is OperationCanceledException) && FatalError.ReportAndCatch(exception);
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x0002FE8D File Offset: 0x0002E08D
		[DebuggerHidden]
		public static bool ReportAndCatchUnlessCanceled(Exception exception, CancellationToken contextCancellationToken)
		{
			return !FatalError.IsCurrentOperationBeingCancelled(exception, contextCancellationToken) && FatalError.ReportAndCatch(exception);
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x0002FEA0 File Offset: 0x0002E0A0
		[DebuggerHidden]
		public static bool ReportAndPropagate(Exception exception)
		{
			FatalError.Report(exception, FatalError.s_fatalHandler);
			return false;
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x0002FEAE File Offset: 0x0002E0AE
		[DebuggerHidden]
		public static bool ReportAndCatch(Exception exception)
		{
			FatalError.Report(exception, FatalError.s_nonFatalHandler);
			return true;
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x0002FEBC File Offset: 0x0002E0BC
		private static void Report(Exception exception, [Nullable(new byte[] { 2, 1 })] Action<Exception> handler)
		{
			FatalError.s_reportedException = exception;
			FatalError.s_reportedExceptionMessage = exception.ToString();
			if (handler == null)
			{
				return;
			}
			if (exception.Data[FatalError.s_reportedMarker] != null)
			{
				return;
			}
			AggregateException ex = exception as AggregateException;
			if (ex != null && ex.InnerExceptions.Count == 1 && ex.InnerExceptions[0].Data[FatalError.s_reportedMarker] != null)
			{
				return;
			}
			if (!exception.Data.IsReadOnly)
			{
				exception.Data[FatalError.s_reportedMarker] = FatalError.s_reportedMarker;
			}
			handler.Invoke(exception);
		}

		// Token: 0x04000501 RID: 1281
		[Nullable(new byte[] { 2, 1 })]
		private static Action<Exception> s_fatalHandler;

		// Token: 0x04000502 RID: 1282
		[Nullable(new byte[] { 2, 1 })]
		private static Action<Exception> s_nonFatalHandler;

		// Token: 0x04000503 RID: 1283
		[Nullable(2)]
		private static Exception s_reportedException;

		// Token: 0x04000504 RID: 1284
		[Nullable(2)]
		private static string s_reportedExceptionMessage;

		// Token: 0x04000505 RID: 1285
		private static readonly object s_reportedMarker = new object();
	}
}
