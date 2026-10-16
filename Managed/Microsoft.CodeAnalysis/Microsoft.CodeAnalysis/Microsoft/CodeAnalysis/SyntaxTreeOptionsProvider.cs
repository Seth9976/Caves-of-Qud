using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000157 RID: 343
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class SyntaxTreeOptionsProvider
	{
		// Token: 0x06000DCB RID: 3531
		public abstract GeneratedKind IsGenerated(SyntaxTree tree, CancellationToken cancellationToken);

		// Token: 0x06000DCC RID: 3532
		public abstract bool TryGetDiagnosticValue(SyntaxTree tree, string diagnosticId, CancellationToken cancellationToken, out ReportDiagnostic severity);

		// Token: 0x06000DCD RID: 3533
		public abstract bool TryGetGlobalDiagnosticValue(string diagnosticId, CancellationToken cancellationToken, out ReportDiagnostic severity);
	}
}
