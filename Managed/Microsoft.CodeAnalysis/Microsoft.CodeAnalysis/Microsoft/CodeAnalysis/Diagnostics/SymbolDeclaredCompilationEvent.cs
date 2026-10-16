using System;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x0200034F RID: 847
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class SymbolDeclaredCompilationEvent : CompilationEvent
	{
		// Token: 0x0600246E RID: 9326 RVA: 0x00071AC0 File Offset: 0x0006FCC0
		public SymbolDeclaredCompilationEvent(Compilation compilation, ISymbol symbol, [Nullable(2)] SemanticModel semanticModelWithCachedBoundNodes = null)
			: base(compilation)
		{
			this.Symbol = symbol;
			this.SemanticModelWithCachedBoundNodes = semanticModelWithCachedBoundNodes;
			this._lazyCachedDeclaringReferences = new Lazy<ImmutableArray<SyntaxReference>>(() => symbol.DeclaringSyntaxReferences);
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x0600246F RID: 9327 RVA: 0x00071B0B File Offset: 0x0006FD0B
		public ISymbol Symbol { get; }

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06002470 RID: 9328 RVA: 0x00071B13 File Offset: 0x0006FD13
		[Nullable(2)]
		public SemanticModel SemanticModelWithCachedBoundNodes
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06002471 RID: 9329 RVA: 0x00071B1B File Offset: 0x0006FD1B
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._lazyCachedDeclaringReferences.Value;
			}
		}

		// Token: 0x06002472 RID: 9330 RVA: 0x00071B28 File Offset: 0x0006FD28
		public override string ToString()
		{
			string text = this.Symbol.Name;
			if (text == "")
			{
				text = "<empty>";
			}
			string text2;
			if (this.DeclaringSyntaxReferences.Length == 0)
			{
				text2 = null;
			}
			else
			{
				text2 = " @ " + string.Join<FileLinePositionSpan>(", ", Enumerable.Select<SyntaxReference, FileLinePositionSpan>(this.DeclaringSyntaxReferences, (SyntaxReference r) => r.GetLocation().GetLineSpan()));
			}
			string text3 = text2;
			return string.Concat(new string[]
			{
				"SymbolDeclaredCompilationEvent(",
				text,
				" ",
				this.Symbol.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat),
				text3,
				")"
			});
		}

		// Token: 0x04000F8C RID: 3980
		[Nullable(new byte[] { 1, 0, 1 })]
		private readonly Lazy<ImmutableArray<SyntaxReference>> _lazyCachedDeclaringReferences;
	}
}
