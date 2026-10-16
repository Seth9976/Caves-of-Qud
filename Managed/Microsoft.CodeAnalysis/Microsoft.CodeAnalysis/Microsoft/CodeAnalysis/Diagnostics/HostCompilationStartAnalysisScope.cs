using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000343 RID: 835
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class HostCompilationStartAnalysisScope : HostAnalysisScope
	{
		// Token: 0x060023E1 RID: 9185 RVA: 0x0007052F File Offset: 0x0006E72F
		public HostCompilationStartAnalysisScope(HostSessionStartAnalysisScope sessionScope)
		{
			this._sessionScope = sessionScope;
		}

		// Token: 0x060023E2 RID: 9186 RVA: 0x00070540 File Offset: 0x0006E740
		public override AnalyzerActions GetAnalyzerActions(DiagnosticAnalyzer analyzer)
		{
			AnalyzerActions analyzerActions = base.GetAnalyzerActions(analyzer);
			AnalyzerActions analyzerActions2 = this._sessionScope.GetAnalyzerActions(analyzer);
			if (analyzerActions2.IsEmpty)
			{
				return analyzerActions;
			}
			if (analyzerActions.IsEmpty)
			{
				return analyzerActions2;
			}
			return analyzerActions.Append(in analyzerActions2, true);
		}

		// Token: 0x04000F61 RID: 3937
		private readonly HostSessionStartAnalysisScope _sessionScope;
	}
}
