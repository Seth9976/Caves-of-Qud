using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001D9 RID: 473
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundITuplePattern : BoundPattern
	{
		// Token: 0x06002025 RID: 8229 RVA: 0x000A3BC7 File Offset: 0x000A1DC7
		public BoundITuplePattern(SyntaxNode syntax, MethodSymbol getLengthMethod, MethodSymbol getItemMethod, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundSubpattern> subpatterns, TypeSymbol inputType, TypeSymbol narrowedType, bool hasErrors = false)
			: base(BoundKind.ITuplePattern, syntax, inputType, narrowedType, hasErrors || subpatterns.HasErrors<BoundSubpattern>())
		{
			this.GetLengthMethod = getLengthMethod;
			this.GetItemMethod = getItemMethod;
			this.Subpatterns = subpatterns;
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06002026 RID: 8230 RVA: 0x000A3BFD File Offset: 0x000A1DFD
		public MethodSymbol GetLengthMethod { get; }

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06002027 RID: 8231 RVA: 0x000A3C05 File Offset: 0x000A1E05
		public MethodSymbol GetItemMethod { get; }

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06002028 RID: 8232 RVA: 0x000A3C0D File Offset: 0x000A1E0D
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<BoundSubpattern> Subpatterns
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06002029 RID: 8233 RVA: 0x000A3C15 File Offset: 0x000A1E15
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitITuplePattern(this);
		}

		// Token: 0x0600202A RID: 8234 RVA: 0x000A3C20 File Offset: 0x000A1E20
		public BoundITuplePattern Update(MethodSymbol getLengthMethod, MethodSymbol getItemMethod, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundSubpattern> subpatterns, TypeSymbol inputType, TypeSymbol narrowedType)
		{
			if (!SymbolEqualityComparer.ConsiderEverything.Equals(getLengthMethod, this.GetLengthMethod) || !SymbolEqualityComparer.ConsiderEverything.Equals(getItemMethod, this.GetItemMethod) || subpatterns != this.Subpatterns || !TypeSymbol.Equals(inputType, base.InputType, TypeCompareKind.ConsiderEverything) || !TypeSymbol.Equals(narrowedType, base.NarrowedType, TypeCompareKind.ConsiderEverything))
			{
				BoundITuplePattern boundITuplePattern = new BoundITuplePattern(this.Syntax, getLengthMethod, getItemMethod, subpatterns, inputType, narrowedType, base.HasErrors);
				boundITuplePattern.CopyAttributes(this);
				return boundITuplePattern;
			}
			return this;
		}
	}
}
