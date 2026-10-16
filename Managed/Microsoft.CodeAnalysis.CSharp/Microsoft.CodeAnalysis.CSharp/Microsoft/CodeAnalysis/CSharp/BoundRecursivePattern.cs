using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001D8 RID: 472
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundRecursivePattern : BoundPattern
	{
		// Token: 0x0600201B RID: 8219 RVA: 0x000A3A54 File Offset: 0x000A1C54
		public BoundRecursivePattern([global::System.Runtime.CompilerServices.Nullable(1)] SyntaxNode syntax, BoundTypeExpression declaredType, MethodSymbol deconstructMethod, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundSubpattern> deconstruction, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundSubpattern> properties, Symbol variable, BoundExpression variableAccess, bool isExplicitNotNullTest, [global::System.Runtime.CompilerServices.Nullable(1)] TypeSymbol inputType, [global::System.Runtime.CompilerServices.Nullable(1)] TypeSymbol narrowedType, bool hasErrors = false)
			: base(BoundKind.RecursivePattern, syntax, inputType, narrowedType, hasErrors || declaredType.HasErrors() || deconstruction.HasErrors<BoundSubpattern>() || properties.HasErrors<BoundSubpattern>() || variableAccess.HasErrors())
		{
			this.DeclaredType = declaredType;
			this.DeconstructMethod = deconstructMethod;
			this.Deconstruction = deconstruction;
			this.Properties = properties;
			this.Variable = variable;
			this.VariableAccess = variableAccess;
			this.IsExplicitNotNullTest = isExplicitNotNullTest;
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x0600201C RID: 8220 RVA: 0x000A3ACF File Offset: 0x000A1CCF
		public BoundTypeExpression DeclaredType { get; }

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x0600201D RID: 8221 RVA: 0x000A3AD7 File Offset: 0x000A1CD7
		public MethodSymbol DeconstructMethod { get; }

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x0600201E RID: 8222 RVA: 0x000A3ADF File Offset: 0x000A1CDF
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<BoundSubpattern> Deconstruction
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x0600201F RID: 8223 RVA: 0x000A3AE7 File Offset: 0x000A1CE7
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<BoundSubpattern> Properties
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06002020 RID: 8224 RVA: 0x000A3AEF File Offset: 0x000A1CEF
		public Symbol Variable { get; }

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06002021 RID: 8225 RVA: 0x000A3AF7 File Offset: 0x000A1CF7
		public BoundExpression VariableAccess { get; }

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06002022 RID: 8226 RVA: 0x000A3AFF File Offset: 0x000A1CFF
		public bool IsExplicitNotNullTest { get; }

		// Token: 0x06002023 RID: 8227 RVA: 0x000A3B07 File Offset: 0x000A1D07
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitRecursivePattern(this);
		}

		// Token: 0x06002024 RID: 8228 RVA: 0x000A3B10 File Offset: 0x000A1D10
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public BoundRecursivePattern Update(BoundTypeExpression declaredType, MethodSymbol deconstructMethod, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundSubpattern> deconstruction, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundSubpattern> properties, Symbol variable, BoundExpression variableAccess, bool isExplicitNotNullTest, [global::System.Runtime.CompilerServices.Nullable(1)] TypeSymbol inputType, [global::System.Runtime.CompilerServices.Nullable(1)] TypeSymbol narrowedType)
		{
			if (declaredType != this.DeclaredType || !SymbolEqualityComparer.ConsiderEverything.Equals(deconstructMethod, this.DeconstructMethod) || deconstruction != this.Deconstruction || properties != this.Properties || !SymbolEqualityComparer.ConsiderEverything.Equals(variable, this.Variable) || variableAccess != this.VariableAccess || isExplicitNotNullTest != this.IsExplicitNotNullTest || !TypeSymbol.Equals(inputType, base.InputType, TypeCompareKind.ConsiderEverything) || !TypeSymbol.Equals(narrowedType, base.NarrowedType, TypeCompareKind.ConsiderEverything))
			{
				BoundRecursivePattern boundRecursivePattern = new BoundRecursivePattern(this.Syntax, declaredType, deconstructMethod, deconstruction, properties, variable, variableAccess, isExplicitNotNullTest, inputType, narrowedType, base.HasErrors);
				boundRecursivePattern.CopyAttributes(this);
				return boundRecursivePattern;
			}
			return this;
		}
	}
}
