using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200012E RID: 302
	internal abstract class ErrorLogger
	{
		// Token: 0x06000B52 RID: 2898
		[NullableContext(1)]
		public abstract void LogDiagnostic(Diagnostic diagnostic, [Nullable(2)] SuppressionInfo suppressionInfo);
	}
}
