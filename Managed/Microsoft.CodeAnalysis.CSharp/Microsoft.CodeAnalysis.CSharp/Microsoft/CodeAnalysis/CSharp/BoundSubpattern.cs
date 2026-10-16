using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001DA RID: 474
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundSubpattern : BoundNode
	{
		// Token: 0x0600202B RID: 8235 RVA: 0x000A3CA2 File Offset: 0x000A1EA2
		public BoundSubpattern(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] Symbol symbol, BoundPattern pattern, bool hasErrors = false)
			: base(BoundKind.Subpattern, syntax, hasErrors || pattern.HasErrors())
		{
			this.Symbol = symbol;
			this.Pattern = pattern;
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x0600202C RID: 8236 RVA: 0x000A3CCB File Offset: 0x000A1ECB
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public Symbol Symbol
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x0600202D RID: 8237 RVA: 0x000A3CD3 File Offset: 0x000A1ED3
		public BoundPattern Pattern { get; }

		// Token: 0x0600202E RID: 8238 RVA: 0x000A3CDB File Offset: 0x000A1EDB
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitSubpattern(this);
		}

		// Token: 0x0600202F RID: 8239 RVA: 0x000A3CE4 File Offset: 0x000A1EE4
		public BoundSubpattern Update([global::System.Runtime.CompilerServices.Nullable(2)] Symbol symbol, BoundPattern pattern)
		{
			if (!SymbolEqualityComparer.ConsiderEverything.Equals(symbol, this.Symbol) || pattern != this.Pattern)
			{
				BoundSubpattern boundSubpattern = new BoundSubpattern(this.Syntax, symbol, pattern, base.HasErrors);
				boundSubpattern.CopyAttributes(this);
				return boundSubpattern;
			}
			return this;
		}
	}
}
