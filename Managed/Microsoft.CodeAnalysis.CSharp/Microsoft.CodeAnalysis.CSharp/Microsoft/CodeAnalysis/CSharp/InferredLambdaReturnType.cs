using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000F8 RID: 248
	internal readonly struct InferredLambdaReturnType
	{
		// Token: 0x060012D5 RID: 4821 RVA: 0x00063CF0 File Offset: 0x00061EF0
		internal InferredLambdaReturnType(int numExpressions, bool hadExpressionlessReturn, RefKind refKind, TypeWithAnnotations typeWithAnnotations, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticInfo> useSiteDiagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<AssemblySymbol> dependencies)
		{
			this.NumExpressions = numExpressions;
			this.HadExpressionlessReturn = hadExpressionlessReturn;
			this.RefKind = refKind;
			this.TypeWithAnnotations = typeWithAnnotations;
			this.UseSiteDiagnostics = useSiteDiagnostics;
			this.Dependencies = dependencies;
		}

		// Token: 0x040005D3 RID: 1491
		internal readonly int NumExpressions;

		// Token: 0x040005D4 RID: 1492
		internal readonly bool HadExpressionlessReturn;

		// Token: 0x040005D5 RID: 1493
		internal readonly RefKind RefKind;

		// Token: 0x040005D6 RID: 1494
		internal readonly TypeWithAnnotations TypeWithAnnotations;

		// Token: 0x040005D7 RID: 1495
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal readonly ImmutableArray<DiagnosticInfo> UseSiteDiagnostics;

		// Token: 0x040005D8 RID: 1496
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal readonly ImmutableArray<AssemblySymbol> Dependencies;
	}
}
