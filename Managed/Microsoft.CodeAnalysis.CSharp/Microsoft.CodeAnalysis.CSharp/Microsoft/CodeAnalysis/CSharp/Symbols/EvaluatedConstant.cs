using System;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004D2 RID: 1234
	internal sealed class EvaluatedConstant
	{
		// Token: 0x060068B0 RID: 26800 RVA: 0x00188950 File Offset: 0x00186B50
		public EvaluatedConstant(ConstantValue value, ImmutableBindingDiagnostic<AssemblySymbol> diagnostics)
		{
			this.Value = value;
			this.Diagnostics = diagnostics.NullToEmpty();
		}

		// Token: 0x04001A34 RID: 6708
		public readonly ConstantValue Value;

		// Token: 0x04001A35 RID: 6709
		public readonly ImmutableBindingDiagnostic<AssemblySymbol> Diagnostics;
	}
}
