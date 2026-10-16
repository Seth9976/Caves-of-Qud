using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000589 RID: 1417
	internal sealed class SynthesizedEmbeddedAttributeConstructorWithBodySymbol : SynthesizedInstanceConstructor
	{
		// Token: 0x0600765A RID: 30298 RVA: 0x001B753C File Offset: 0x001B573C
		internal SynthesizedEmbeddedAttributeConstructorWithBodySymbol(NamedTypeSymbol containingType, Func<MethodSymbol, ImmutableArray<ParameterSymbol>> getParameters, Action<SyntheticBoundNodeFactory, ArrayBuilder<BoundStatement>, ImmutableArray<ParameterSymbol>> getConstructorBody)
			: base(containingType)
		{
			this._parameters = getParameters.Invoke(this);
			this._getConstructorBody = getConstructorBody;
		}

		// Token: 0x170020D1 RID: 8401
		// (get) Token: 0x0600765B RID: 30299 RVA: 0x001B7559 File Offset: 0x001B5759
		public override ImmutableArray<ParameterSymbol> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x0600765C RID: 30300 RVA: 0x001B7561 File Offset: 0x001B5761
		internal override void GenerateMethodBody(TypeCompilationState compilationState, BindingDiagnosticBag diagnostics)
		{
			base.GenerateMethodBodyCore(compilationState, diagnostics);
		}

		// Token: 0x0600765D RID: 30301 RVA: 0x001B756B File Offset: 0x001B576B
		internal override void GenerateMethodBodyStatements(SyntheticBoundNodeFactory factory, ArrayBuilder<BoundStatement> statements, BindingDiagnosticBag diagnostics)
		{
			this._getConstructorBody.Invoke(factory, statements, this._parameters);
		}

		// Token: 0x04001CBF RID: 7359
		private readonly ImmutableArray<ParameterSymbol> _parameters;

		// Token: 0x04001CC0 RID: 7360
		private readonly Action<SyntheticBoundNodeFactory, ArrayBuilder<BoundStatement>, ImmutableArray<ParameterSymbol>> _getConstructorBody;
	}
}
