using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000308 RID: 776
	[NullableContext(2)]
	[Nullable(0)]
	public sealed class AnalyzerLoadFailureEventArgs : EventArgs
	{
		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x0600221B RID: 8731 RVA: 0x0006C7FF File Offset: 0x0006A9FF
		public string TypeName { get; }

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x0600221C RID: 8732 RVA: 0x0006C807 File Offset: 0x0006AA07
		[Nullable(1)]
		public string Message
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x0600221D RID: 8733 RVA: 0x0006C80F File Offset: 0x0006AA0F
		public AnalyzerLoadFailureEventArgs.FailureErrorCode ErrorCode { get; }

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x0600221E RID: 8734 RVA: 0x0006C817 File Offset: 0x0006AA17
		public Exception Exception { get; }

		// Token: 0x0600221F RID: 8735 RVA: 0x0006C820 File Offset: 0x0006AA20
		public AnalyzerLoadFailureEventArgs(AnalyzerLoadFailureEventArgs.FailureErrorCode errorCode, [Nullable(1)] string message, Exception exceptionOpt = null, string typeNameOpt = null)
		{
			if (errorCode <= AnalyzerLoadFailureEventArgs.FailureErrorCode.None || errorCode > AnalyzerLoadFailureEventArgs.FailureErrorCode.ReferencesFramework)
			{
				throw new ArgumentOutOfRangeException("errorCode");
			}
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			this.ErrorCode = errorCode;
			this.Message = message;
			this.TypeName = typeNameOpt;
			this.Exception = exceptionOpt;
		}

		// Token: 0x020006D2 RID: 1746
		[NullableContext(0)]
		public enum FailureErrorCode
		{
			// Token: 0x04001AB1 RID: 6833
			None,
			// Token: 0x04001AB2 RID: 6834
			UnableToLoadAnalyzer,
			// Token: 0x04001AB3 RID: 6835
			UnableToCreateAnalyzer,
			// Token: 0x04001AB4 RID: 6836
			NoAnalyzers,
			// Token: 0x04001AB5 RID: 6837
			ReferencesFramework
		}
	}
}
