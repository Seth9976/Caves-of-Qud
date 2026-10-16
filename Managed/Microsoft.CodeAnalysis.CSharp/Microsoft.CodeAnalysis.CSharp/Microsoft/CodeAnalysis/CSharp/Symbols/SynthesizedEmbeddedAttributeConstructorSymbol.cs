using System;
using System.Collections.Immutable;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000586 RID: 1414
	internal sealed class SynthesizedEmbeddedAttributeConstructorSymbol : SynthesizedInstanceConstructor
	{
		// Token: 0x06007643 RID: 30275 RVA: 0x001B71E9 File Offset: 0x001B53E9
		internal SynthesizedEmbeddedAttributeConstructorSymbol(NamedTypeSymbol containingType, Func<MethodSymbol, ImmutableArray<ParameterSymbol>> getParameters)
			: base(containingType)
		{
			this._parameters = getParameters.Invoke(this);
		}

		// Token: 0x170020CA RID: 8394
		// (get) Token: 0x06007644 RID: 30276 RVA: 0x001B71FF File Offset: 0x001B53FF
		public override ImmutableArray<ParameterSymbol> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x06007645 RID: 30277 RVA: 0x001B7207 File Offset: 0x001B5407
		internal override void GenerateMethodBody(TypeCompilationState compilationState, BindingDiagnosticBag diagnostics)
		{
			base.GenerateMethodBodyCore(compilationState, diagnostics);
		}

		// Token: 0x04001CB6 RID: 7350
		private readonly ImmutableArray<ParameterSymbol> _parameters;
	}
}
