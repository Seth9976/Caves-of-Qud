using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x0200032A RID: 810
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class DiagnosticAnalyzer
	{
		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06002379 RID: 9081
		[Nullable(new byte[] { 0, 1 })]
		public abstract ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x0600237A RID: 9082
		public abstract void Initialize(AnalysisContext context);

		// Token: 0x0600237B RID: 9083 RVA: 0x0006FD73 File Offset: 0x0006DF73
		[NullableContext(2)]
		public sealed override bool Equals(object obj)
		{
			return this == obj;
		}

		// Token: 0x0600237C RID: 9084 RVA: 0x0006FD79 File Offset: 0x0006DF79
		public sealed override int GetHashCode()
		{
			return ReferenceEqualityComparer.GetHashCode(this);
		}

		// Token: 0x0600237D RID: 9085 RVA: 0x0006FD81 File Offset: 0x0006DF81
		public sealed override string ToString()
		{
			return base.GetType().ToString();
		}
	}
}
